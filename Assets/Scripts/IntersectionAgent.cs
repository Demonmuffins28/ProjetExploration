using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class IntersectionAgent : Agent
{
    GameObject OEB, OETLeft, OET, Saint_Jean, EOB, EOT, EOTRight, Fabrique;
    TopDownCarController carController;
    AIIntersectionControls controls;

    public float episodeTime = 60f;
    float timeOfEpisode = 0f;

    private void Awake()
    {
        OEB = GameObject.Find("OEB");
        OETLeft = GameObject.Find("OETLeft");
        OET = GameObject.Find("OET");
        Saint_Jean = GameObject.Find("Saint-Jean");
        EOB = GameObject.Find("EOB");
        EOT = GameObject.Find("EOT");
        EOTRight = GameObject.Find("EOTRight");
        Fabrique = GameObject.Find("Fabrique");

        carController = GetComponent<TopDownCarController>();
        controls = GetComponent<AIIntersectionControls>();
    }

    public override void OnEpisodeBegin()
    {
        // detruire tous les Autos
        GameObject[] AICars = GameObject.FindGameObjectsWithTag("AI");
        foreach (GameObject ai in AICars)
        {
            Destroy(ai);
        }

        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
        EOTRight.SetActive(false);

        timeOfEpisode = 0f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        GameObject[] AICars = GameObject.FindGameObjectsWithTag("AI");

        foreach (GameObject car in AICars)
        {
            carController = car.GetComponent<TopDownCarController>();

            try
            {
                // car position
                sensor.AddObservation(car.transform.localPosition.x);
                sensor.AddObservation(car.transform.localPosition.y);

                // car velocity
                sensor.AddObservation(carController.GetComponent<Rigidbody2D>().velocity.x);
                sensor.AddObservation(carController.GetComponent<Rigidbody2D>().velocity.y);

                // agent position (not sure if needed)
                sensor.AddObservation(this.transform.localPosition);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    public float lightDelay = 1.0f;
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Get the action index for the OE intersection
        int OEIntersection = actions.DiscreteActions[0];
        //int IntersctionMid = actions.DiscreteActions[1];
        //int EOIntersection = actions.DiscreteActions[2];

        // Look what index it is and change the lights accordingly
        if (OEIntersection == 0) { controls.TurnLeftOE(lightDelay); }
        if (OEIntersection == 1) { controls.Fabrique(lightDelay); }
        if (OEIntersection == 2) { controls.SaintJean(lightDelay); }
        if (OEIntersection == 3) { controls.OEAndOE(lightDelay); }

        Debug.Log(OEIntersection);

        // Rewards
        GameObject[] AICars = GameObject.FindGameObjectsWithTag("AI");
        float totalCarTimer = 0;

        foreach (GameObject car in AICars)
        {
            try
            {
                totalCarTimer += car.GetComponent<CarAIHandler>().carLifetime;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }

        timeOfEpisode += Time.deltaTime;
        if (timeOfEpisode >= episodeTime)
        {
            SetReward(1 / totalCarTimer);
            EndEpisode();
        }
    }
}
