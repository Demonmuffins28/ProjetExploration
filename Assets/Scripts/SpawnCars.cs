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

    private IEnumerator CarGenerator()
    {
        GameObject TrainingArea = GameObject.Find("TrainingArea");

        while (true)
        {
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
