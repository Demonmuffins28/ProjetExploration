using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    //Components
    TopDownCarController topDownCarController;
    CarAIHandler carAIHandler;

    public new Collider2D collider;

    private void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        carAIHandler = GetComponent<CarAIHandler>();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "OETLeft")
        {
            // Check if car goes forward or turns
            if (carAIHandler.currentWaypoint.name == "OET_TurnLeft" ||
                carAIHandler.currentWaypoint.name == "OET_TurnLeft_1" ||
                carAIHandler.currentWaypoint.name == "OET_TurnLeft_2")
            {
                carAIHandler.SetMakeItStop(true);
                collider = collision;
            }
            else if (carAIHandler.currentWaypoint.name == "WaypointNode(7)")
            {
                GameObject OET = GameObject.Find("OET");
                if (OET.activeSelf)
                {
                    carAIHandler.SetMakeItStop(true);
                    collider = OET.GetComponent<BoxCollider2D>();
                }
                else
                {
                    carAIHandler.SetMakeItStop(false);
                    collider = OET.GetComponent<BoxCollider2D>();
                }

            }
        }
        else if (collision.name == "EOTRight")
        {
            if (carAIHandler.currentWaypoint.name is "WaypointNode(5)" or "WaypointNode(6)")
            {
                carAIHandler.SetMakeItStop(true);
                collider = collision;
            }
            else if (carAIHandler.currentWaypoint.name == "WaypointNode(2)")
            {
                GameObject EOT = GameObject.Find("EOT");
                if (EOT.activeSelf)
                {
                    carAIHandler.SetMakeItStop(true);
                    collider = EOT.GetComponent<BoxCollider2D>();
                }
                else
                {
                    carAIHandler.SetMakeItStop(false);
                    collider = EOT.GetComponent<BoxCollider2D>();
                }

            }
        }
        else
        {
            carAIHandler.SetMakeItStop(true);
            collider = collision;
        }
    }

    private void Update()
    {
        if (collider.isActiveAndEnabled == false)
        {
            carAIHandler.SetMakeItStop(false);
            topDownCarController.accelerationInput = 1f;
            topDownCarController.SetAccelerationFactor(30.0f);
        }            
    }
}
