using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIntersectionControls : MonoBehaviour
{
    GameObject OEB, OETLeft, OET, SJ, EOB, EOT, EOTRight, Fab;

    GameObject OEB_M, OET_M, EOB_M, EOT_M, SH_Left_M, SH_Right_M, OEBRight_M, EOBLeft_M;

    GameObject OEB_EO, OET_EO, EOB_EO, EOT_EO, OETLeft_EO, EOBLeft_EO, St_Henri_EO;

    private void Awake()
    {
        OEB = GameObject.Find("OEB");
        OETLeft = GameObject.Find("OETLeft");
        OET = GameObject.Find("OET");
        SJ = GameObject.Find("Saint-Jean");
        EOB = GameObject.Find("EOB");
        EOT = GameObject.Find("EOT");
        EOTRight = GameObject.Find("EOTRight");
        Fab = GameObject.Find("Fabrique");

        OEB_M = GameObject.Find("OEB_M");
        OET_M = GameObject.Find("OET_M");
        EOB_M = GameObject.Find("EOB_M");
        EOT_M = GameObject.Find("EOT_M");
        SH_Left_M = GameObject.Find("St-Henri_L_M");
        SH_Right_M = GameObject.Find("St-Henri_R_M");
        OEBRight_M = GameObject.Find("OEBRight_M");
        EOBLeft_M = GameObject.Find("EOBLeft_M");

        OEB_EO = GameObject.Find("OEB_EO");
        OET_EO = GameObject.Find("OET_EO");
        EOB_EO = GameObject.Find("EOB_EO");
        EOT_EO = GameObject.Find("EOT_EO");
        OETLeft_EO = GameObject.Find("OETLeft_EO");
        EOBLeft_EO = GameObject.Find("EOBLeft_EO");
        St_Henri_EO = GameObject.Find("St-Henri_EO");
    }

    /***************************** Intersection de gauche OE ****************************************/
    public void TourneRougeOE()
    {
        OEB.SetActive(true);
        OETLeft.SetActive(true);
        OET.SetActive(true);
        SJ.SetActive(true);
        EOB.SetActive(true);
        EOT.SetActive(true);
        EOTRight.SetActive(true);
        Fab.SetActive(true);
    }

    // Action 1
    public void TurnLeftOE(float delay)
    {
        /*      EOB.SetActive(true);
                EOT.SetActive(true);
                EOTRight.SetActive(true);*/
        TourneRougeOE();
        //Invoke("Case1", delay);
        OETLeft.SetActive(false);
        OEB.SetActive(false);
        OET.SetActive(false);
    }

    // Action 2
    public void Fabrique(float delay)
    {
        /*      OEB.SetActive(true);
                OET.SetActive(true);
                OETLeft.SetActive(true);*/
        TourneRougeOE();
        //Invoke("Case2", delay);
        Fab.SetActive(false);
    }

    // Action 3
    public void SaintJean(float delay)
    {
        //Fab.SetActive(true);
        TourneRougeOE();
        //Invoke("Case3", delay);
        SJ.SetActive(false);
    }

    // Action 4
    public void OEAndOE(float delay)
    {
        /*      SJ.SetActive(true);
                OETLeft.SetActive(true);*/
        TourneRougeOE();
        //Invoke("Case4", delay);
        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
        EOTRight.SetActive(false);
    }

    private void Case1()
    {
        OETLeft.SetActive(false);
        OEB.SetActive(false);
        OET.SetActive(false);
    }

    private void Case2()
    {
        Fab.SetActive(false);
    }

    private void Case3()
    {
        SJ.SetActive(false);
    }

    private void Case4()
    {
        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
        EOTRight.SetActive(false);
    }

    /***************************** Intersection du milieu ****************************************/
    private void TourneRougeMilieu()
    {
        OEB_M.SetActive(true);
        OET_M.SetActive(true);
        EOB_M.SetActive(true);
        EOT_M.SetActive(true);
        SH_Left_M.SetActive(true);
        SH_Right_M.SetActive(true);
        OEBRight_M.SetActive(true);
        EOBLeft_M.SetActive(true);
    }

    public void EOTurnLeft()
    {
        TourneRougeMilieu();
        EOB_M.SetActive(false);
        EOT_M.SetActive(false);
        EOBLeft_M.SetActive(false);
    }

    public void St_Henri()
    {
        TourneRougeMilieu();
        SH_Left_M.SetActive(false);
        SH_Right_M.SetActive(false);
    }

    public void A20Milieu()
    {
        TourneRougeMilieu();
        OEB_M.SetActive(false);
        OET_M.SetActive(false);
        OEBRight_M.SetActive(false);
        EOB_M.SetActive(false);
        EOT_M.SetActive(false);
    }

    /***************************** Intersection de droite EO ****************************************/
    private void TourneRougeEO()
    {
        OEB_EO.SetActive(true);
        OET_EO.SetActive(true);
        EOB_EO.SetActive(true);
        EOT_EO.SetActive(true);
        OETLeft_EO.SetActive(true);
        EOBLeft_EO.SetActive(true);
        St_Henri_EO.SetActive(true);
    }

    public void A20TourneGauche()
    {
        TourneRougeEO();
        OETLeft_EO.SetActive(false);
        EOBLeft_EO.SetActive(false);
    }

    public void St_HenriEO()
    {
        TourneRougeEO();
        St_Henri_EO.SetActive(false);
    }

    public void A20EO()
    {
        TourneRougeEO();
        OEB_EO.SetActive(false);
        OET_EO.SetActive(false);
        EOB_EO.SetActive(false);
        EOT_EO.SetActive(false);
    }
}
