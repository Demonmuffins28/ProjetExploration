using UnityEngine;

public class CarDetection : MonoBehaviour
{

    GameObject OEB, OETLeft, OET, Saint_Jean, EOB, EOT, EOTRight, Fabrique;
    CarAIHandler carHandler;

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

        carHandler = GetComponent<CarAIHandler>();

        /*
        OEB.SetActive(true);
        OET.SetActive(true);
        EOB.SetActive(true);
        EOT.SetActive(true);
        EOTRight.SetActive(true);
        OETLeft.SetActive(true);
        Saint_Jean.SetActive(true);
        Fabrique.SetActive(true);
        */
    }

    private void FixedUpdate()
    {
        GameObject[] AICars = GameObject.FindGameObjectsWithTag("AI");

        int countOE = 0;
        foreach (GameObject car in AICars)
        {
            carHandler = car.GetComponent<CarAIHandler>();

            try
            {
                if (carHandler != null && carHandler.currentWaypoint.name is "OET_TurnLeft" or "OET_TurnLeft_1" or "OET_TurnLeft_2")
                {
                    countOE++;
                }
            }
            catch (System.Exception)
            {
         
            }
        }

        /*
        if (countOE > 0)
        {
            OEB.SetActive(false);
            OET.SetActive(false);
            OETLeft.SetActive(false);
        }
        */
    }
}
