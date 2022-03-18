using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
    float accelerationFactor = 20.0f;
    float turnFactor = 2.5f;
    public float maxSpeed = 7.5f;
    float slowDownFactor = 10f;

    //Local variables
    public float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

    //Components
    Rigidbody2D carRigidbody2D;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rotationAngle = transform.rotation.eulerAngles.z;
    }

    //Frame-rate independent for physics calculations.
    void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        //Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, slowDownFactor, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

        //Caculate how much "forward" we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, slowDownFactor, Time.fixedDeltaTime * 3);
            return;
        }

        if (maxSpeed == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, slowDownFactor, Time.fixedDeltaTime * 3);
        }

        //Limit so we cannot go faster in any direction while accelerating
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //Limit the cars ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 2);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);

        carRigidbody2D.velocity = forwardVelocity;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        if (maxSpeed > 0)
            accelerationInput = inputVector.y;
        else
            accelerationInput = 0;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }

    public void SetAccelerationFactor(float factor)
    {
        accelerationFactor = factor;
    }
}