using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColors : MonoBehaviour
{
    GameObject OEB, OETLeft, Saint_Jean, EOT, EOTRight, Fabrique, lCol;
    GameObject OEB_M, EOB_M, St_Henri, OETurn_M, EOTurn_M, lCol_M;
    GameObject OEB_EO, EOB_EO, St_Henri_EO, OETurn_EO, EOTurn_EO, lCol_EO;

    private void Awake()
    {
        OEB = GameObject.Find("OEB");
        OETLeft = GameObject.Find("OETLeft");
        Saint_Jean = GameObject.Find("Saint-Jean");
        EOT = GameObject.Find("EOT");
        EOTRight = GameObject.Find("EOTRight");
        Fabrique = GameObject.Find("Fabrique");
        lCol = GameObject.Find("LightsColor");

        OEB_M = GameObject.Find("OEB_M");
        EOB_M = GameObject.Find("EOB_M");
        St_Henri = GameObject.Find("St-Henri_L_M");
        OETurn_M = GameObject.Find("OEBRight_M");
        EOTurn_M = GameObject.Find("EOBLeft_M");
        lCol_M = GameObject.Find("LightsColor_M");

        OEB_EO = GameObject.Find("OEB_EO");
        EOB_EO = GameObject.Find("EOB_EO");
        St_Henri_EO = GameObject.Find("St-Henri_EO");
        OETurn_EO = GameObject.Find("OETLeft_EO");
        EOTurn_EO = GameObject.Find("EOBLeft_EO");
        lCol_EO = GameObject.Find("LightsColor_EO");
    }

    // Update is called once per frame
    void Update()
    {
/*        // OE goes straight
        if (OEB.activeSelf)
        {
            lCol.transform.Find("OEGreen").gameObject.SetActive(false);
            lCol.transform.Find("OERed").gameObject.SetActive(true);
        }
        else
        {
            lCol.transform.Find("OEGreen").gameObject.SetActive(true);
            lCol.transform.Find("OERed").gameObject.SetActive(false);
        }

        // OE turn left
        if (OETLeft.activeSelf)
        {
            lCol.transform.Find("OETurnGreen").gameObject.SetActive(false);
            lCol.transform.Find("OETurnRed").gameObject.SetActive(true);
        }
        else
        {
            lCol.transform.Find("OETurnGreen").gameObject.SetActive(true);
            lCol.transform.Find("OETurnRed").gameObject.SetActive(false);
        }*/

        // Saint jean
        /*if (Saint_Jean.activeSelf)
        {
            lCol.transform.Find("SJGreen").gameObject.SetActive(false);
            lCol.transform.Find("SJRed").gameObject.SetActive(true);
        }
        else
        {
            lCol.transform.Find("SJGreen").gameObject.SetActive(true);
            lCol.transform.Find("SJRed").gameObject.SetActive(false);
        }

        // EO go straight
*//*        if (EOT.activeSelf)
        {
            lCol.transform.Find("EOGreen").gameObject.SetActive(false);
            lCol.transform.Find("EORed").gameObject.SetActive(true);
        }
        else
        {
            lCol.transform.Find("EOGreen").gameObject.SetActive(true);
            lCol.transform.Find("EORed").gameObject.SetActive(false);
        }*//*
        
        // test
        if (EOT.activeSelf)
            lCol.transform.Find("EORed").GetComponent<_Light>().couleurLumiere = 2;
        else
            lCol.transform.Find("EORed").GetComponent<_Light>().couleurLumiere = 1;

        // EO turn right
        if (EOTRight.activeSelf)
        {
            lCol.transform.Find("EORightGreen").gameObject.SetActive(false);
            lCol.transform.Find("EORightRed").gameObject.SetActive(true);
        }
        else
        {
            lCol.transform.Find("EORightGreen").gameObject.SetActive(true);
            lCol.transform.Find("EORightRed").gameObject.SetActive(false);
        }

        // fabrique
        if (Fabrique.activeSelf)
        {
            lCol.transform.Find("FabGreen").gameObject.SetActive(false);
            lCol.transform.Find("FabRed").gameObject.SetActive(true);
        }
        else
        {
            lCol.transform.Find("FabGreen").gameObject.SetActive(true);
            lCol.transform.Find("FabRed").gameObject.SetActive(false);
        }*/

        // **************** Middle intersection *****************************

        // OE goes straight
        if (OEB_M.activeSelf)
        {
            lCol_M.transform.Find("OEGreen_M").gameObject.SetActive(false);
            lCol_M.transform.Find("OERed_M").gameObject.SetActive(true);
        }
        else
        {
            lCol_M.transform.Find("OEGreen_M").gameObject.SetActive(true);
            lCol_M.transform.Find("OERed_M").gameObject.SetActive(false);
        }

        // EO goes straight
        if (EOB_M.activeSelf)
        {
            lCol_M.transform.Find("EOGreen_M").gameObject.SetActive(false);
            lCol_M.transform.Find("EORed_M").gameObject.SetActive(true);
        }
        else
        {
            lCol_M.transform.Find("EOGreen_M").gameObject.SetActive(true);
            lCol_M.transform.Find("EORed_M").gameObject.SetActive(false);
        }

        // OE turn right
        if (OETurn_M.activeSelf)
        {
            lCol_M.transform.Find("OETurnGreen_M").gameObject.SetActive(false);
            lCol_M.transform.Find("OETurnRed_M").gameObject.SetActive(true);
        }
        else
        {
            lCol_M.transform.Find("OETurnGreen_M").gameObject.SetActive(true);
            lCol_M.transform.Find("OETurnRed_M").gameObject.SetActive(false);
        }

        // EO turn left
        if (EOTurn_M.activeSelf)
        {
            lCol_M.transform.Find("EOTurnGreen_M").gameObject.SetActive(false);
            lCol_M.transform.Find("EOTurnRed_M").gameObject.SetActive(true);
        }
        else
        {
            lCol_M.transform.Find("EOTurnGreen_M").gameObject.SetActive(true);
            lCol_M.transform.Find("EOTurnRed_M").gameObject.SetActive(false);
        }

        // Saint-Henri
        if (St_Henri.activeSelf)
        {
            lCol_M.transform.Find("SHGreen_M").gameObject.SetActive(false);
            lCol_M.transform.Find("SHRed_M").gameObject.SetActive(true);
        }
        else
        {
            lCol_M.transform.Find("SHGreen_M").gameObject.SetActive(true);
            lCol_M.transform.Find("SHRed_M").gameObject.SetActive(false);
        }

        // **************** EO intersection *****************************

        // OE goes straight
        if (OEB_EO.activeSelf)
        {
            lCol_EO.transform.Find("OEGreen_EO").gameObject.SetActive(false);
            lCol_EO.transform.Find("OERed_EO").gameObject.SetActive(true);
        }
        else
        {
            lCol_EO.transform.Find("OEGreen_EO").gameObject.SetActive(true);
            lCol_EO.transform.Find("OERed_EO").gameObject.SetActive(false);
        }

        // EO goes straight
        if (EOB_EO.activeSelf)
        {
            lCol_EO.transform.Find("EOGreen_EO").gameObject.SetActive(false);
            lCol_EO.transform.Find("EORed_EO").gameObject.SetActive(true);
        }
        else
        {
            lCol_EO.transform.Find("EOGreen_EO").gameObject.SetActive(true);
            lCol_EO.transform.Find("EORed_EO").gameObject.SetActive(false);
        }

        // OE turn right
        if (OETurn_EO.activeSelf)
        {
            lCol_EO.transform.Find("OETurnGreen_EO").gameObject.SetActive(false);
            lCol_EO.transform.Find("OETurnRed_EO").gameObject.SetActive(true);
        }
        else
        {
            lCol_EO.transform.Find("OETurnGreen_EO").gameObject.SetActive(true);
            lCol_EO.transform.Find("OETurnRed_EO").gameObject.SetActive(false);
        }

        // EO turn left
        if (EOTurn_EO.activeSelf)
        {
            lCol_EO.transform.Find("EOTurnGreen_EO").gameObject.SetActive(false) ;
            lCol_EO.transform.Find("EOTurnRed_EO").gameObject.SetActive(true);
        }
        else
        {
            lCol_EO.transform.Find("EOTurnGreen_EO").gameObject.SetActive(true);
            lCol_EO.transform.Find("EOTurnRed_EO").gameObject.SetActive(false);
        }

        // Saint-Henri
        if (St_Henri_EO.activeSelf)
        {
            lCol_EO.transform.Find("SHGreen_EO").gameObject.SetActive(false);
            lCol_EO.transform.Find("SHRed_EO").gameObject.SetActive(true);
        }
        else
        {
            lCol_EO.transform.Find("SHGreen_EO").gameObject.SetActive(true);
            lCol_EO.transform.Find("SHRed_EO").gameObject.SetActive(false);
        }
    }
}
