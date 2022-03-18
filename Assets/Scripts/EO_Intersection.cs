using System.Collections;
using UnityEngine;

public class EO_Intersection : MonoBehaviour
{
    public float waitTime = 5.0f;
    public float timer = 0.0f;
    public int cycle = 1;

    GameObject OEB, OET, EOB, EOT, St_Henri, OETLeft, EOBLeft;

    private void Awake()
    {
        OEB = GameObject.Find("OEB_EO");
        OET = GameObject.Find("OET_EO");
        EOB = GameObject.Find("EOB_EO");
        EOT = GameObject.Find("EOT_EO");
        OETLeft = GameObject.Find("OETLeft_EO");
        EOBLeft = GameObject.Find("EOBLeft_EO");
        St_Henri = GameObject.Find("St-Henri_EO");
    }

    private void Start()
    {
        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
    }

    private void Update()
    {
        if (timer > 15)
        {
            cycle = 1;
            timer = 0;
        }

        timer += Time.deltaTime;

        if (timer >= waitTime * cycle)
        {
            switch (cycle)
            {
                // EO & OE turn left
                case 1:                    
                    OEB.SetActive(true);
                    OET.SetActive(true);
                    EOT.SetActive(true);
                    EOB.SetActive(true);
                    Invoke("Case1", 1.0f);
                    break;
                // St-Henri go
                case 2:
                    OETLeft.SetActive(true);
                    EOBLeft.SetActive(true);
                    Invoke("Case2", 1.0f);
                    break;
                // A20 go
                case 3:
                    St_Henri.SetActive(true);
                    Invoke("Case3", 1.0f);
                    break;
            }

            cycle++;
        }
    }

    private void Case1()
    {
        OETLeft.SetActive(false);
        EOBLeft.SetActive(false);
    }

    private void Case2()
    {
        St_Henri.SetActive(false);
    }

    private void Case3()
    {
        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
    }
}
