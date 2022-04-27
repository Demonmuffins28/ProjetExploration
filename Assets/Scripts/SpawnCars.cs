using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnCars : MonoBehaviour
{
    public GameObject AICar;

    private float destroyedCarStopTimer = 0f;

    Vector2[] tabSpawnPos = {
        new Vector2(-150,16.3f), // OET
        new Vector2(-150,12f), // OEB
        new Vector2(-80.5f,70f), // Frabrique
        new Vector2(129f,27.7f), // EOT
        new Vector2(129f,24f),   // EOB
        new Vector2(-77.9f, -18.6f), // Saint-Jean
        new Vector2(38.2f, 82.7f), // St-Henri N
        new Vector2(35.9f, 85.5f), // St-Henri N
        //new Vector2(1.2f, 84.1f), // St-Henri N (pas 340)
        new Vector2(58f, -27.4f) // St-Henri S
    };

    Vector2[] tabSpawnWpPos = {
        new Vector2(-175f, 15.4f),
        new Vector2(-175f, 11.4f),
        new Vector2(-80.7f, 97.3f),
        new Vector2(170f, 27.9f),
        new Vector2(170f, 24.5f),
        new Vector2(-77.9f, -46.2f),
        new Vector2(54.2f, 122.7f),
        new Vector2(57.6f, 121.2f),
        new Vector2(57.8f, -60.1f)
    };

    // Tableau de waypoints pour le spawn des voitures
    // Dans le meme ordre que tabSpawnPos
    /* Erreur dans unity si laisse ici.
    WaypointNode[] tabWaypoints = {
        GameObject.Find("wpSpawnOET").GetComponent<WaypointNode>(),
        GameObject.Find("wpSpawnOEB").GetComponent<WaypointNode>(),
        GameObject.Find("wpSpawnFabrique").GetComponent<WaypointNode>(),
        GameObject.Find("wpSpawnEOT").GetComponent<WaypointNode>(),
        GameObject.Find("wpSpawnEOB").GetComponent<WaypointNode>(),
        GameObject.Find("wpSpawnSJB").GetComponent<WaypointNode>(),
        GameObject.Find("wpSpawnSH").GetComponent<WaypointNode>(),
        GameObject.Find("wpSpawnSHN1").GetComponent<WaypointNode>(),
        GameObject.Find("wpSpawnSHN2").GetComponent<WaypointNode>()
    };*/

    int[] rotAngle = { -90, -90, 180, 90, 90, 0, 120, 120, /*180,*/ 0 };

    public float delay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        /*
        int i = 0;
        foreach (var pos in tabSpawnPos)
        {
            Instantiate(AICar, pos, Quaternion.Euler(0f, 0f, rotAngle[i]));
            i++;
        }
        */

        Invoke("StartGenerator", delay);
        //StartGenerator();
    }

    private void translateCar(GameObject newCar)
    {
        GameObject[] AICars = GameObject.FindGameObjectsWithTag("AI");

        foreach (GameObject car in AICars)
        {
            
            if (car != newCar)
            {
                float xCourant = car.transform.localPosition.x;
                float yCourant = car.transform.localPosition.y;
                float xNew = newCar.transform.localPosition.x;
                float yNew = newCar.transform.localPosition.y;

                //Debug.Log("xCourant : " + xCourant + " yCourant : " + yCourant + " xNew : " + xNew + " yNew " + yNew);
                //Debug.Log((xCourant + 2 > xNew - 2 && xCourant - 2 < xNew + 2) && (yCourant + 2 > yNew - 2 && yCourant - 2 < yNew + 2));

                if ((xCourant + 2 > xNew - 2 && xCourant - 2 < xNew + 2) && (yCourant + 2 > yNew - 2 && yCourant - 2 < yNew + 2))
                {
                    newCar.transform.Translate(new Vector2(0, -3f));
                    translateCar(newCar); // Continuer de vérifier avec la même voiture jusqu'à temps que l'espace ne soit plus occupé.
                }
            }
            
        }
    }

    private IEnumerator CarGenerator()
    {
        GameObject TrainingArea = GameObject.Find("TrainingArea");

        while (true)
        {

            int intNbCarsTotal = GameObject.FindGameObjectsWithTag("AI").Length;
            //Debug.Log("test " + intNbCars);

            if (intNbCarsTotal < 50)
            {
                // Decider le nombre de voitures spawn
                int intLowest = 1;
                int intHighest = 10;

                switch (intNbCarsTotal)
                {
                    case < 10:
                        intLowest = 5;
                        break;
                    case < 20:
                        intHighest = 6;
                        break;
                    default:
                        intHighest = 3;
                        break;
                }

                int intNbCars = Random.Range(intLowest, intHighest); //Nombre de voitures a spawn

                // Spawn les voitures
                int randPosCar = 0; // Position random du spawn correspondant au tableau 'tabSpawnPos'

                for (int i = 0; i < intNbCars; i++)
                {
                    randPosCar = Random.Range(0, tabSpawnPos.Length);

                    // Si la position est Fabrique, Saint-Jean-Baptiste ou saint-henri sud, on re-randomize la position de la voiture
                    if(randPosCar is 2 or 5 or 8)
                        randPosCar = Random.Range(0, tabSpawnPos.Length);

                    Vector2 posCar = tabSpawnPos[randPosCar];

                    GameObject newCar = Instantiate(AICar, posCar, Quaternion.Euler(0f, 0f, rotAngle[randPosCar]), TrainingArea.transform);

                    translateCar(newCar);
                }

            }
            
             yield return new WaitForSeconds(Mathf.Lerp(0.5f, 1.5f, Random.value));
        }
    }

    public void StartGenerator()
    {
        StartCoroutine(CarGenerator());
    }

    public void StopGenerator()
    {
        StopAllCoroutines();
    }
    public void SetCarStopTimer(float timer)
    {
        destroyedCarStopTimer += timer;
    }

    public float GetCarStopTimer()
    {
        return destroyedCarStopTimer;
    }

}
