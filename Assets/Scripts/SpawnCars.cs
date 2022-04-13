using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Retourne true s'il y a une voiture qui occupe l'emplacement
    // intSpawnPos == random (entre 0 et 8 inclusivement) correspondant a un vecteur dans le tableau tabSpawnWpPos
    // fPosition == position ou on veut verifier s'il y a une voiture
    // charOrientation == correspond a l'orientation de la nouvelle voiture h:horizontal v:vertical
    private bool isOccupied(float fPosition, char charOrientation)
    {
        GameObject[] AICars = GameObject.FindGameObjectsWithTag("AI");

        foreach (GameObject car in AICars)
        {
            float fPosCarCourant = -1000f; //Position de la voiture courante.

            // Si l'orientation du waypoint/de la voiture courante est horizontale, on recupere le x
            if (charOrientation == 'h')
                fPosCarCourant = car.transform.localPosition.x;
            else
                fPosCarCourant = car.transform.localPosition.y;

            // Si la position de l'avant de la voiture courante est plus grande que la position de l'arriere de la voiture qu'on veut placer
            // OU si la position de l'arriere de la voiture courante est plus petite que la position de l'avant de la voiture qu'on veut placer
            // Alors l'espace est occupe (return true)
            if (fPosCarCourant + 2 > fPosition - 2 || fPosCarCourant - 2 < fPosition + 2)
                return true;
        }

        return false;
    }

    private IEnumerator CarGenerator()
    {
        GameObject TrainingArea = GameObject.Find("TrainingArea");

        while (true)
        {

            int intNbCarsTotal = GameObject.FindGameObjectsWithTag("AI").Length;
            //Debug.Log("test " + intNbCars);

            if (intNbCarsTotal < 15)
            {
                // Decider le nombre de voitures spawn
                int intLowest = 1;
                int intHighest = 10;

                switch (intNbCarsTotal)
                {
                    case < 3:
                        intLowest = 5;
                        break;
                    case < 7:
                        intHighest = 5;
                        break;
                    default:
                        intHighest = 3;
                        break;
                }

                int intNbCars = Random.Range(intLowest, intHighest); //Nombre de voitures a spawn

                // Spawn les voitures
                int intNbCarsSpawned = 0; //Nombre de voitures qui ont spawn jusqu'a date.
                int randPosCar = Random.Range(0, tabSpawnPos.Length); // Position random du spawn correspondant au tableau 'tabSpawnPos'
                char charOrientation = 'v'; // Orientation du waypoint/de la voiture
                Vector2 spawnPoint = tabSpawnPos[randPosCar]; // Vecteur qui correspond au spawnpoint qui correspond a randPosCar
                float fPosSpawn = 0; // Position ou la voiture va spawn (x ou y dependant du charOrientation)

                // Si la position ou on veut spawn la voiture est OET, OEB, EOT ou EOB, alors l'orientation de la voiture est horizontale
                // La position ou la voiture va spawn est le x du spawn point
                if (randPosCar is 0 or 1 or 3 or 4)
                {
                    charOrientation = 'h';
                    fPosSpawn = tabSpawnPos[randPosCar].x;
                }
                else // Sinon l'orientation reste verticale et la position ou la voiture va spawn est le y du spawn point
                    fPosSpawn = tabSpawnPos[randPosCar].y;

                do
                {

                    // Verifier si une voiture est ou on veut en spawn une
                    if (isOccupied(fPosSpawn, charOrientation) == false)
                    {
                        // TESTS
                        Debug.Log("DEBUT du if occupied");
                        Debug.Log("intNbCarsSpawned : " + intNbCarsSpawned + " randPosCar : " + randPosCar + " charOrientation : " + charOrientation + " fPosSpawn : " + fPosSpawn);
                        // TESTS

                        //Spawn la voiture
                        if (charOrientation == 'h')
                            Instantiate(AICar, new Vector2(fPosSpawn, spawnPoint.y), Quaternion.Euler(0f, 0f, rotAngle[randPosCar]), TrainingArea.transform);
                        else
                            Instantiate(AICar, new Vector2(spawnPoint.x, fPosSpawn), Quaternion.Euler(0f, 0f, rotAngle[randPosCar]), TrainingArea.transform);

                        // Changer les valeurs pour la prochaine voiture
                        intNbCarsSpawned++;

                        randPosCar = Random.Range(0, tabSpawnPos.Length);

                        charOrientation = 'v';

                        if (randPosCar is 0 or 1 or 3 or 4)
                        {
                            charOrientation = 'h';
                            fPosSpawn = tabSpawnPos[randPosCar].x;
                        }
                        else
                            fPosSpawn = tabSpawnPos[randPosCar].y;

                        // TESTS 
                        Debug.Log("FIN du if occupied");
                        Debug.Log("intNbCarsSpawned : " + intNbCarsSpawned + " randPosCar : " + randPosCar + " charOrientation : " + charOrientation + " fPosSpawn : " + fPosSpawn);
                        // TESTS
                    }
                    else {

                        // Augmenter ou diminuer fPosSpawn selon ou on spawn la voiture
                        if (randPosCar is 0 or 1 or 5 or 8)
                            fPosSpawn -= 3;
                        else
                            fPosSpawn += 3;
                    }

                    intNbCarsSpawned++; // pour pas infinite loop

                    // TESTS
                    Debug.Log("Avant while " + intNbCarsSpawned + " sur " + intNbCars);
                    Debug.Log("Avant while , isoccupied " + isOccupied(fPosSpawn, charOrientation));
                    Debug.Log("intNbCarsSpawned : " + intNbCarsSpawned + " randPosCar : " + randPosCar + " charOrientation : " + charOrientation + " fPosSpawn : " + fPosSpawn);
                    // TESTS

                } while (intNbCarsSpawned < intNbCars);






                /*
                float fDecalage = 0;
                Vector2 posCar = tabSpawnPos[0];

                for (int i = 0; i < 4; i++)
                {
                    GameObject newCar = Instantiate(AICar, posCar, Quaternion.Euler(0f, 0f, rotAngle[0]), TrainingArea.transform);
                    newCar.GetComponent<CarAIHandler>().currentWaypoint = GameObject.Find("wpSpawnOET").GetComponent<WaypointNode>();
                    newCar.transform.Translate(new Vector2(0f, fDecalage));
                    fDecalage += 3.2f;
                }
                */

                // ------------------- UNTOUCHED
                int randPos1 = Random.Range(0, tabSpawnPos.Length);
                if (randPos1 is 2 or 5 or 8)
                {
                    randPos1 = Random.Range(0, tabSpawnPos.Length);

                    Vector2 randV1Pos = tabSpawnPos[randPos1];
                    Instantiate(AICar, randV1Pos, Quaternion.Euler(0f, 0f, rotAngle[randPos1]), TrainingArea.transform);
                    yield return new WaitForSeconds(Mathf.Lerp(0.5f, 1.5f, Random.value));
                }
                else
                {
                    Vector2 randV2Pos = tabSpawnPos[randPos1];
                    Instantiate(AICar, randV2Pos, Quaternion.Euler(0f, 0f, rotAngle[randPos1]), TrainingArea.transform);
                    yield return new WaitForSeconds(Mathf.Lerp(0.5f, 1.5f, Random.value));
                }
            }
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