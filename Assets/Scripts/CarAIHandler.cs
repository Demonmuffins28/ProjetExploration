using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CarAIHandler : MonoBehaviour
{
    [Header("AI settings")]
    float maxSpeed;
    [Range(0.0f, 1.0f)]
    float skillLevel = 1.0f;

    public WaypointNode currentWaypoint = null;

    [Header("Sensors")]
    float sensorLength = 6f;
    Vector2 frontSensorPos = new Vector2(0, 1.5f);

    [Header("Car LifeTime")]
    public float carLifetime = 0f;
    public float timeStopped = 0f;
    private bool carHasStopped = false;

    //Local variables
    Vector3 targetPosition = Vector3.zero;
    public float orignalMaximumSpeed = 0;

    float angleToTarget = 0;

    bool makeItStop = false;
    
    Vector2 inputVector;

    //Waypoints
    WaypointNode[] allWayPoints;

    //Components
    TopDownCarController topDownCarController;
    IntersectionAgent agent;
    //SpriteRenderer spriteRenderer;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        //spriteRenderer = GetComponent<SpriteRenderer>();

        allWayPoints = FindObjectsOfType<WaypointNode>();
        agent = FindObjectOfType<IntersectionAgent>();

        orignalMaximumSpeed = topDownCarController.maxSpeed;
        maxSpeed = orignalMaximumSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMaxSpeedBasedOnSkillLevel(maxSpeed);

        //Pick the cloesest waypoint if we don't have a waypoint set.
        if (currentWaypoint == null)
        {
            currentWaypoint = FindClosestWayPoint();
        }
    }

    // Update is called once per frame and is frame dependent
    void FixedUpdate()
    {
        inputVector = Vector2.zero;

        FollowWaypoints();

        Sensors();

        inputVector.x = TurnTowardTarget();
        inputVector.y = ApplyThrottleOrBrake(inputVector.x);

        if (!makeItStop)
        {
            //Send the input to the car controller.
            topDownCarController.SetInputVector(inputVector);
        } 
        else
        {
            //Send the input to the car controller.
            topDownCarController.SetInputVector(new Vector2(0,0));
            maxSpeed = 0;
        }

        carLifetime += Time.deltaTime;

        if (topDownCarController.maxSpeed < 1f || topDownCarController.accelerationInput <= 0.1f)
        {
            timeStopped += Time.deltaTime;
            carHasStopped = true;
        }   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (topDownCarController.GetVelocityMagnitude() > 5 
            && collision.gameObject.GetComponent<TopDownCarController>().accelerationInput == 0)
        {
            SetMakeItStop(true);
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            Debug.Log("Car hit");
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetMakeItStop(false);
    }

    //AI follows waypoints
    private void FollowWaypoints()
    {
        if (currentWaypoint == null)
        {
            Destroy(gameObject);
        }

        //Set the target on the waypoints position
        if (currentWaypoint != null)
        {
            if (currentWaypoint.nextWaypointNode.Length == 0)
            {
                Destroy(gameObject);
            }
                
            //Set the target position of for the AI. 
            targetPosition = currentWaypoint.transform.position;

            //Store how close we are to the target
            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            //Check if we are close enough to consider that we have reached the waypoint
            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                //If we are close enough then follow to the next waypoint, if there are multiple waypoints then pick one at random.
                currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
            }
        }
    }

    //Find the cloest Waypoint to the AI
    WaypointNode FindClosestWayPoint()
    {
        return allWayPoints
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    // Sensors for finding obstacles around the car
    private void Sensors()
    {

        Vector2 sensorStartPos = transform.position;
        sensorStartPos += (Vector2)transform.up * frontSensorPos.y;

        Vector2 sensorStartPosLeft = transform.position;
        sensorStartPosLeft += (Vector2)transform.up * frontSensorPos.y;
        sensorStartPosLeft += (Vector2)transform.right * 0.675f;

        Vector2 sensorStartPosRight = transform.position;
        sensorStartPosRight += (Vector2)transform.up * frontSensorPos.y;
        sensorStartPosRight -= (Vector2)transform.right * 0.675f;

        RaycastHit2D hit = Physics2D.Raycast(sensorStartPos, transform.up, sensorLength);
        RaycastHit2D hitLeft = Physics2D.Raycast(sensorStartPosLeft, transform.up, sensorLength);
        RaycastHit2D hitRight = Physics2D.Raycast(sensorStartPosRight, transform.up, sensorLength);

        // Center line raycast
        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("AI"))
            {
                Debug.DrawLine(sensorStartPos, hit.point, Color.red);
                if (hit.distance < 5f && hit.distance > 2f)
                    maxSpeed = Mathf.Abs(hit.transform.GetComponent<Rigidbody2D>().velocity.x);
                else if (hit.distance < 2f)
                {
                    topDownCarController.maxSpeed = maxSpeed < 2f ? 0 : maxSpeed * 0.6f;
                }
                else
                {
                    topDownCarController.maxSpeed = orignalMaximumSpeed;
                    topDownCarController.accelerationInput = 1f;
                }
            }
        }
        else
        {
            topDownCarController.maxSpeed = orignalMaximumSpeed;
            topDownCarController.accelerationInput = 1f;
        }
        
        // Left line raycast
        if (hitLeft.collider != null)
        {
            if (hitLeft.transform.CompareTag("AI"))
            {
                Debug.DrawLine(sensorStartPosLeft, hitLeft.point, Color.red);
                if (hitLeft.distance < 5f && hitLeft.distance > 2f)
                    maxSpeed = Mathf.Abs(hitLeft.transform.GetComponent<Rigidbody2D>().velocity.x);
                else if (hitLeft.distance < 2f)
                {
                    topDownCarController.maxSpeed = maxSpeed < 2f ? 0 : maxSpeed * 0.8f;
                }
                else
                {
                    topDownCarController.maxSpeed = orignalMaximumSpeed;
                    topDownCarController.accelerationInput = 1f;
                }
            }
        }
        else
        {
            topDownCarController.maxSpeed = orignalMaximumSpeed;
            topDownCarController.accelerationInput = 1f;
        }
        // Right line raycast
        if (hitRight.collider != null)
        {
            if (hitRight.transform.CompareTag("AI"))
            {
                Debug.DrawLine(sensorStartPosRight, hitRight.point, Color.red);
                if (hitRight.distance < 5f && hitRight.distance > 2f)
                    maxSpeed = Mathf.Abs(hitRight.transform.GetComponent<Rigidbody2D>().velocity.x);
                else if (hitRight.distance < 2f)
                {
                    topDownCarController.maxSpeed = maxSpeed < 2f ? 0 : maxSpeed * 0.8f;
                }
                else
                {
                    topDownCarController.maxSpeed = orignalMaximumSpeed;
                    topDownCarController.accelerationInput = 1f;
                }
            }
        }
        else
        {
            topDownCarController.maxSpeed = orignalMaximumSpeed;
            topDownCarController.accelerationInput = 1f;
        }
    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        //Calculate an angle towards the target 
        angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        //We want the car to turn as much as possible if the angle is greater than 45 degrees and we wan't it to smooth out so if the angle is small we want the AI to make smaller corrections. 
        float steerAmount = angleToTarget / 45.0f;

        //Clamp steering to between -1 and 1.
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }

    float ApplyThrottleOrBrake(float inputX)
    {
        //Apply throttle forward based on how much the car wants to turn. If it's a sharp turn this will cause the car to apply less speed forward. We store this as reduceSpeedDueToCornering so we can use it togehter with the skill level
        float reduceSpeedDueToCornering = Mathf.Abs(inputX) / 1.0f;

        //Apply throttle based on cornering and skill.
        float throttle = 1.05f - reduceSpeedDueToCornering; //* skillLevel;

        //Apply throttle based on cornering and skill.
        return throttle;
    }

    void SetMaxSpeedBasedOnSkillLevel(float newSpeed)
    {
        maxSpeed = Mathf.Clamp(newSpeed, 0, orignalMaximumSpeed);

        float skillbasedMaxiumSpeed = Mathf.Clamp(skillLevel, 0.3f, 1.0f);
        maxSpeed = maxSpeed * skillbasedMaxiumSpeed;
    }

    public void SetMakeItStop(bool stop)
    {
        makeItStop = stop;
    }

    private void OnDestroy()
    {
        agent.SetCarStopTimer(timeStopped);
        if (!carHasStopped)
            agent.AddReward(1);
    }

    public void SetWaypoint(WaypointNode wp)
    {
        currentWaypoint = wp;
    }
}