using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;

public class IntersectionAgent : Agent
{
    GameObject OEB, OETLeft, OET, Saint_Jean, EOB, EOT, EOTRight, Fabrique;
    GameObject interGauche, interMid, interDroite;
    TopDownCarController carController;
    AIIntersectionControls controls;

    public float episodeTime = 60f;
    public float timeOfEpisode = 0f;
    public int totalCarStopped = 0;
    //float totalCarStopTimer = 0f;

    public float destroyedCarStopTimer = 0f;

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
        interGauche = GameObject.Find("OE_Intersection");
        interMid = GameObject.Find("Intersection_Milieu");
        interDroite = GameObject.Find("EO_Intersection");
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

                // positions des intersections
                sensor.AddObservation(interGauche.transform.localPosition);
                sensor.AddObservation(interMid.transform.localPosition);
                sensor.AddObservation(interDroite.transform.localPosition);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    public float lightDelay = 1.0f;
    private int prevOEIntersection = -1;
    private int prevIntersectionMid = -1;
    private int prevEOIntersection = -1;
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Get the action index for the all intersections
        int OEIntersection = actions.DiscreteActions[0];
        int IntersectionMid = actions.DiscreteActions[1];
        int EOIntersection = actions.DiscreteActions[2];

        // Look what index it is and change the lights accordingly
        if (OEIntersection == 0) { controls.TurnLeftOE(lightDelay); }
        if (OEIntersection == 1) { controls.Fabrique(lightDelay); }
        if (OEIntersection == 2) { controls.SaintJean(lightDelay); }
        if (OEIntersection == 3) { controls.OEAndOE(lightDelay); }

        if (IntersectionMid == 0) { controls.EOTurnLeft(); }
        if (IntersectionMid == 1) { controls.St_Henri(); }
        if (IntersectionMid == 2) { controls.A20Milieu(); }

        if (EOIntersection == 0) { controls.A20TourneGauche(); }
        if (EOIntersection == 1) { controls.St_HenriEO(); }
        if (EOIntersection == 2) { controls.A20EO(); }

        //******* Rewards ************

        // Ajouter une petite penalite a chaque changement de lumiere
        if (OEIntersection != prevOEIntersection)
            AddReward(-0.01f);
        if (IntersectionMid != prevIntersectionMid)
            AddReward(-0.01f);
        if (EOIntersection != prevEOIntersection)
            AddReward(-0.01f);

        prevOEIntersection = OEIntersection;
        prevIntersectionMid = IntersectionMid;
        prevEOIntersection = EOIntersection;

  
        GameObject[] AICars = GameObject.FindGameObjectsWithTag("AI");
        totalCarStopped = 0;

        foreach (GameObject car in AICars)
        {
            try
            {
                // trouver le nombre de voiture arreter a chaque step
                if (car.GetComponent<TopDownCarController>().maxSpeed < 1f)
                    totalCarStopped++;
                    //totalCarStopTimer += car.GetComponent<CarAIHandler>().timeStopped;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }

        //totalCarStopTimer += destroyedCarStopTimer;

        if (totalCarStopped != 0)
            AddReward(-0.05f * totalCarStopped);

        timeOfEpisode += Time.deltaTime;

        if (timeOfEpisode >= episodeTime)
        {
            //SetReward(1 / totalCarStopTimer);
            EndEpisode();
        }
    }

    public void SetCarStopTimer(float timer)
    {
        destroyedCarStopTimer += timer;
    }
}
