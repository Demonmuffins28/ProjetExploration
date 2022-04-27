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

    public float delay = 2.0f;

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

        OEL_M = GameObject.Find("OEGreen_M");
        EOL_M = GameObject.Find("EOGreen_M");
        OETurnL_M = GameObject.Find("OETurnGreen_M");
        EOTurnL_M = GameObject.Find("EOTurnGreen_M");
        SHL_M = GameObject.Find("SHGreen_M");

        OEL_EO = GameObject.Find("OEGreen_EO");
        EOL_EO = GameObject.Find("EOGreen_EO");
        OETurnL_EO = GameObject.Find("OETurnGreen_EO");
        EOTurnL_EO = GameObject.Find("EOTurnGreen_EO");
        SHL_EO = GameObject.Find("SHGreen_EO");
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

    public void ActionOE(int numAction, int prevAction)
    {
        TourneRougeOE();

        //Debug.Log("ActionOE called. Action number = " + numAction + " ; Previous Action = " + prevAction);

        // switch pour la lumiere jaune. 
        switch (prevAction)
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

        // switch pour le delais sur la lumiere verte
        switch (numAction)
        {
            case 0:
                Invoke("tourneGaucheOE", delay);
                break;
            case 1:
                Invoke("FabA2", delay);
                break;
            case 2:
                Invoke("SJOE", delay);
                break;
            case 3:
                Invoke("A20OE", delay);
                break;
            default:
                break;
        }
    }

    private void tourneGaucheOE()
    {
        TourneRougeOE();

        OETurnL.GetComponent<_Light>().couleurLumiere = 1;
        OEL.GetComponent<_Light>().couleurLumiere = 1;

        OETLeft.SetActive(false);
        OEB.SetActive(false);
        OET.SetActive(false);
    }

    private void FabA2()
    {
        TourneRougeOE();

        FabL.GetComponent<_Light>().couleurLumiere = 1;

        Fab.SetActive(false);
    }

    private void SJOE()
    {
        TourneRougeOE();

        SJL.GetComponent<_Light>().couleurLumiere = 1;

        SJ.SetActive(false);
    }

    private void A20OE()
    {
        TourneRougeOE();

        OEL.GetComponent<_Light>().couleurLumiere = 1;
        EOTurnL.GetComponent<_Light>().couleurLumiere = 1;
        EOL.GetComponent<_Light>().couleurLumiere = 1;

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

        OEL_M.GetComponent<_Light>().couleurLumiere = 2;
        EOL_M.GetComponent<_Light>().couleurLumiere = 2;
        OETurnL_M.GetComponent<_Light>().couleurLumiere = 2;
        EOTurnL_M.GetComponent<_Light>().couleurLumiere = 2;
        SHL_M.GetComponent<_Light>().couleurLumiere = 2;
    }

    public void ActionMid(int numAction, int prevAction)
    {
        TourneRougeMilieu();

        // switch pour la lumiere jaune. 
        switch (prevAction)
        {
            case 0:
                EOTurnL_M.GetComponent<_Light>().couleurLumiere = 3;
                EOL_M.GetComponent<_Light>().couleurLumiere = 3;
                break;
            case 1:
                SHL_M.GetComponent<_Light>().couleurLumiere = 3;
                break;
            case 2:
                EOL_M.GetComponent<_Light>().couleurLumiere = 3;
                OEL_M.GetComponent<_Light>().couleurLumiere = 3;
                OETurnL_M.GetComponent<_Light>().couleurLumiere = 3;
                break;
            default:
                break;
        }

        // switch pour le delais sur la lumiere verte
        switch (numAction)
        {
            case 0:
                Invoke("EOTurnLeft", delay);
                break;
            case 1:
                Invoke("St_Henri", delay);
                break;
            case 2:
                Invoke("A20Milieu", delay);
                break;
            default:
                break;
        }
    }


    private void EOTurnLeft()
    {
        TourneRougeMilieu();

        EOTurnL_M.GetComponent<_Light>().couleurLumiere = 1;
        EOL_M.GetComponent<_Light>().couleurLumiere = 1;

        EOB_M.SetActive(false);
        EOT_M.SetActive(false);
        EOBLeft_M.SetActive(false);
    }

    private void St_Henri()
    {
        TourneRougeMilieu();

        SHL_M.GetComponent<_Light>().couleurLumiere = 1;

        SH_Left_M.SetActive(false);
        SH_Right_M.SetActive(false);
    }

    private void A20Milieu()
    {
        TourneRougeMilieu();

        EOL_M.GetComponent<_Light>().couleurLumiere = 1;
        OEL_M.GetComponent<_Light>().couleurLumiere = 1;
        OETurnL_M.GetComponent<_Light>().couleurLumiere = 1;

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

        OEL_EO.GetComponent<_Light>().couleurLumiere = 2;
        EOL_EO.GetComponent<_Light>().couleurLumiere = 2;
        OETurnL_EO.GetComponent<_Light>().couleurLumiere = 2;
        EOTurnL_EO.GetComponent<_Light>().couleurLumiere = 2;
        SHL_EO.GetComponent<_Light>().couleurLumiere = 2;
    }

    public void ActionEO(int numAction, int prevAction)
    {
        TourneRougeEO();

        // switch pour la lumiere jaune. 
        switch (prevAction)
        {
            case 0:
                EOTurnL_EO.GetComponent<_Light>().couleurLumiere = 3;
                OETurnL_EO.GetComponent<_Light>().couleurLumiere = 3;
                break;
            case 1:
                SHL_EO.GetComponent<_Light>().couleurLumiere = 3;
                break;
            case 2:
                EOL_EO.GetComponent<_Light>().couleurLumiere = 3;
                OEL_EO.GetComponent<_Light>().couleurLumiere = 3;
                break;
            default:
                break;
        }

        // switch pour le delais sur la lumiere verte
        switch (numAction)
        {
            case 0:
                Invoke("A20TourneGauche", delay);
                break;
            case 1:
                Invoke("St_HenriEO", delay);
                break;
            case 2:
                Invoke("A20EO", delay);
                break;
            default:
                break;
        }
    }

    private void A20TourneGauche()
    {
        TourneRougeEO();

        EOTurnL_EO.GetComponent<_Light>().couleurLumiere = 1;
        OETurnL_EO.GetComponent<_Light>().couleurLumiere = 1;

        OETLeft_EO.SetActive(false);
        EOBLeft_EO.SetActive(false);
    }

    private void St_HenriEO()
    {
        TourneRougeEO();

        SHL_EO.GetComponent<_Light>().couleurLumiere = 1;

        St_Henri_EO.SetActive(false);
    }

    private void A20EO()
    {
        TourneRougeEO();

        EOL_EO.GetComponent<_Light>().couleurLumiere = 1;
        OEL_EO.GetComponent<_Light>().couleurLumiere = 1;

        OEB_EO.SetActive(false);
        OET_EO.SetActive(false);
        EOB_EO.SetActive(false);
        EOT_EO.SetActive(false);
    }
}
