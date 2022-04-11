using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIntersectionControls : MonoBehaviour
{
    // a20 oe
    GameObject OEB, OETLeft, OET, SJ, EOB, EOT, EOTRight, Fab;
    GameObject EOL, OEL, OETurnL, SJL, EOTurnL, FabL;

    // a20 milieu
    GameObject OEB_M, OET_M, EOB_M, EOT_M, SH_Left_M, SH_Right_M, OEBRight_M, EOBLeft_M;
    GameObject EOL_M, OEL_M, OETurnL_M, EOTurnL_M, SHL_M;

    // a20 eo
    GameObject OEB_EO, OET_EO, EOB_EO, EOT_EO, OETLeft_EO, EOBLeft_EO, St_Henri_EO;
    GameObject EOL_EO, OEL_EO, OETurnL_EO, EOTurnL_EO, SHL_EO;

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

        // Lumieres
        OEL = GameObject.Find("OEGreen");
        OETurnL = GameObject.Find("OETurnGreen");
        EOL = GameObject.Find("EOGreen");
        SJL = GameObject.Find("SJGreen");
        EOTurnL = GameObject.Find("EORightGreen");
        FabL = GameObject.Find("FabGreen");
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

        OETurnL.GetComponent<_Light>().couleurLumiere = 2;
        OEL.GetComponent<_Light>().couleurLumiere = 2;
        EOL.GetComponent<_Light>().couleurLumiere = 2;
        SJL.GetComponent<_Light>().couleurLumiere = 2;
        EOTurnL.GetComponent<_Light>().couleurLumiere = 2;
        FabL.GetComponent<_Light>().couleurLumiere = 2;
    }

    public void TourneJauneOE(int decisionID)
    {
        switch (decisionID)
        {
            case 0:
                OETurnL.GetComponent<_Light>().couleurLumiere = 3;
                OEL.GetComponent<_Light>().couleurLumiere = 3;
                break;
            case 1:
                FabL.GetComponent<_Light>().couleurLumiere = 3;
                break;
            case 2:
                SJL.GetComponent<_Light>().couleurLumiere = 3;
                break;
            case 3:
                OEL.GetComponent<_Light>().couleurLumiere = 3;
                EOTurnL.GetComponent<_Light>().couleurLumiere = 3;
                EOL.GetComponent<_Light>().couleurLumiere = 3;
                break;
            default:
                break;
        }

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
    public void TurnLeftOE()
    {
        TourneRougeOE();
        OETLeft.SetActive(false);
        OEB.SetActive(false);
        OET.SetActive(false);

        OETurnL.GetComponent<_Light>().couleurLumiere = 1;
        OEL.GetComponent<_Light>().couleurLumiere = 1;
    }

    // Action 2
    public void Fabrique()
    {
        TourneRougeOE();
        Fab.SetActive(false);

        FabL.GetComponent<_Light>().couleurLumiere = 1;
    }

    // Action 3
    public void SaintJean()
    {
        TourneRougeOE();
        SJ.SetActive(false);

        SJL.GetComponent<_Light>().couleurLumiere = 1;
    }

    // Action 4
    public void OEAndOE()
    {
        TourneRougeOE();
        OEB.SetActive(false);
        OET.SetActive(false);
        EOB.SetActive(false);
        EOT.SetActive(false);
        EOTRight.SetActive(false);

        OEL.GetComponent<_Light>().couleurLumiere = 1;
        EOTurnL.GetComponent<_Light>().couleurLumiere = 1;
        EOL.GetComponent<_Light>().couleurLumiere = 1;
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

    public void TourneJauneMilieu()
    {
        TourneRougeMilieu();
    }

    public void EOTurnLeft()
    {
        //TourneRougeMilieu();
        EOB_M.SetActive(false);
        EOT_M.SetActive(false);
        EOBLeft_M.SetActive(false);
    }

    public void St_Henri()
    {
        //TourneRougeMilieu();
        SH_Left_M.SetActive(false);
        SH_Right_M.SetActive(false);
    }

    public void A20Milieu()
    {
        //TourneRougeMilieu();
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

    public void TourneJauneEO()
    {
        TourneRougeEO();
    }

    public void A20TourneGauche()
    {
        //TourneRougeEO();
        OETLeft_EO.SetActive(false);
        EOBLeft_EO.SetActive(false);
    }

    public void St_HenriEO()
    {
        //TourneRougeEO();
        St_Henri_EO.SetActive(false);
    }

    public void A20EO()
    {
        //TourneRougeEO();
        OEB_EO.SetActive(false);
        OET_EO.SetActive(false);
        EOB_EO.SetActive(false);
        EOT_EO.SetActive(false);
    }
}
