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

    private int prevOEIntersection = -1;
    private int prevIntersectionMid = -1;
    private int prevEOIntersection = -1;
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Get the action index for the all intersections
        int OEIntersection = actions.DiscreteActions[0];
        int IntersectionMid = actions.DiscreteActions[1];
        int EOIntersection = actions.DiscreteActions[2];

        //Debug.Log("Action : " + OEIntersection + " ; PrevAction : " + prevOEIntersection);

        // Look what index it is and change the lights accordingly
        if (OEIntersection == 0 && prevOEIntersection != OEIntersection) { controls.ActionOE(OEIntersection, prevOEIntersection); }
        else if (OEIntersection == 1 && prevOEIntersection != OEIntersection) { controls.ActionOE(OEIntersection, prevOEIntersection); }
        else if (OEIntersection == 2 && prevOEIntersection != OEIntersection) { controls.ActionOE(OEIntersection, prevOEIntersection); }
        else if (OEIntersection == 3 && prevOEIntersection != OEIntersection) { controls.ActionOE(OEIntersection, prevOEIntersection); }

        if (IntersectionMid == 0 && prevIntersectionMid != IntersectionMid) { controls.ActionMid(IntersectionMid, prevIntersectionMid); }
        else if (IntersectionMid == 1 && prevIntersectionMid != IntersectionMid) { controls.ActionMid(IntersectionMid, prevIntersectionMid); }
        else if (IntersectionMid == 2 && prevIntersectionMid != IntersectionMid) { controls.ActionMid(IntersectionMid, prevIntersectionMid); }

        if (EOIntersection == 0 && prevEOIntersection != EOIntersection) { controls.ActionEO(EOIntersection, prevEOIntersection); }
        else if (EOIntersection == 1 && prevEOIntersection != EOIntersection) { controls.ActionEO(EOIntersection, prevEOIntersection); }
        else if (EOIntersection == 2 && prevEOIntersection != EOIntersection) { controls.ActionEO(EOIntersection, prevEOIntersection); }

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
            AddReward(-0.1f * totalCarStopped);

        timeOfEpisode += Time.deltaTime;

        if (timeOfEpisode >= episodeTime)
        {
            //SetReward(1 / totalCarStopTimer);
            EndEpisode();
        }
    }
}
