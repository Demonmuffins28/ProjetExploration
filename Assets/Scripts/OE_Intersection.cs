using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OE_Intersection : MonoBehaviour
{
    public float waitTime = 5.0f;
    public float timer = 0.0f;
    public int cycle = 1;

    GameObject OEB, OETLeft, OET, Saint_Jean, EOB, EOT, EOTRight, Fabrique;

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

        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
        EOTRight.SetActive(false);
    }
    
    private void Update()
    {
        if (timer > 25)
        {
            cycle = 1;
            timer = 0;
        }

        timer += Time.deltaTime;

        if (timer >= waitTime * cycle)
        {
            switch (cycle)
            {
                case 1:
                    OETLeft.SetActive(false);
                    EOB.SetActive(true);
                    EOT.SetActive(true);
                    EOTRight.SetActive(true);
                    break;
                case 2:
                    OEB.SetActive(true);
                    OET.SetActive(true);
                    OETLeft.SetActive(true);
                    Fabrique.SetActive(false);
                    break;
                case 3:
                    Fabrique.SetActive(true);
                    Saint_Jean.SetActive(false);
                    break;
                case 4:
                    Saint_Jean.SetActive(true);
                    OEB.SetActive(false);
                    OET.SetActive(false);
                    EOB.SetActive(false);
                    EOT.SetActive(false);
                    EOTRight.SetActive(false);
                    break;
            }

            cycle++;
        }
    }
}
