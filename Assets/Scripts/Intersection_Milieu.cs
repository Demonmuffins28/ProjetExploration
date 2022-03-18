using UnityEngine;

public class Intersection_Milieu : MonoBehaviour
{
    public float waitTime = 5.0f;
    public float timer = 0.0f;
    public int cycle = 1;

    GameObject OEB, OET, EOB, EOT, SH_Left, SH_Right, OEBRight, EOBLeft;

    private void Awake()
    {
        OEB = GameObject.Find("OEB_M");
        OET = GameObject.Find("OET_M");
        EOB = GameObject.Find("EOB_M");
        EOT = GameObject.Find("EOT_M");
        SH_Left = GameObject.Find("St-Henri_L_M");
        SH_Right = GameObject.Find("St-Henri_R_M");
        OEBRight = GameObject.Find("OEBRight_M");
        EOBLeft = GameObject.Find("EOBLeft_M");
    }

    private void Start()
    {
        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
        OEBRight.SetActive(false);
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
                // EO turn left
                case 1:
                    OEB.SetActive(true);
                    OET.SetActive(true);
                    OEBRight.SetActive(true);
                    Invoke("Case1", 1.0f);
                    break;
                // St-Henri go
                case 2:
                    EOB.SetActive(true);
                    EOT.SetActive(true);
                    EOBLeft.SetActive(true);
                    Invoke("Case2", 1.0f);
                    break;
                // A20 go
                case 3:
                    SH_Left.SetActive(true);
                    SH_Right.SetActive(true);
                    Invoke("Case3", 1.0f);
                    break;
            }

            cycle++;
        }
    }

    private void Case1()
    {
        EOBLeft.SetActive(false);
    }

    private void Case2()
    {
        SH_Left.SetActive(false);
        SH_Right.SetActive(false);
    }

    private void Case3()
    {
        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
        OEBRight.SetActive(false);
    }
}
