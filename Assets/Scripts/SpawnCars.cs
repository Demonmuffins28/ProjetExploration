using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    public GameObject AICar;

    Vector2[] tabSpawnPos = {
        new Vector2(-100,16.3f),
        new Vector2(-100,12.6f),
        new Vector2(-80.5f,70f),
        new Vector2(-5.3f,28.7f),
        new Vector2(-5.3f,25f)
    };
    int[] rotAngle = { -90, -90, 180, 90, 90 };

    public float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (var pos in tabSpawnPos)
        {
            Instantiate(AICar, pos, Quaternion.Euler(0f, 0f, rotAngle[i]));
            i++;
        }

        Invoke("StartGenerator", delay);
        //StartGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CarGenerator()
    {
        while (true)
        {
            int randPos = Random.Range(0, 4);
            Vector2 randV2Pos = tabSpawnPos[randPos];
            Instantiate(AICar, randV2Pos, Quaternion.Euler(0f, 0f, rotAngle[randPos]));
            yield return new WaitForSeconds(Mathf.Lerp(1.0f, 2.0f, Random.value));
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
}
