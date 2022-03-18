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
    }

    private void Start()
    {
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
                // turn left OE
                case 1:
                    EOB.SetActive(true);
                    EOT.SetActive(true);
                    EOTRight.SetActive(true);
                    Invoke("Case3", 1.0f);
                    break;
                // Frabrique go
                case 2:
                    OEB.SetActive(true);
                    OET.SetActive(true);
                    OETLeft.SetActive(true);
                    Invoke("Case3", 1.0f);
                    break;
                // SaintJean go
                case 3:
                    Fabrique.SetActive(true);
                    Invoke("Case3", 1.0f);
                    break;
                // OE & EO go
                case 4:
                    Saint_Jean.SetActive(true);
                    OETLeft.SetActive(true);
                    Invoke("Case3", 1.0f);
                    break;
            }

            cycle++;
        }
    }

    private void Case1()
    {
        OETLeft.SetActive(false);
    }

    private void Case2()
    {
        Fabrique.SetActive(false);
    }

    private void Case3()
    {
        Saint_Jean.SetActive(false);
    }

    private void Case4()
    {
        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
        EOTRight.SetActive(false);
    }
}
