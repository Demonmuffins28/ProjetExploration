using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIntersectionControls : MonoBehaviour
{
    GameObject OEB, OETLeft, OET, SJ, EOB, EOT, EOTRight, Fab;

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
    }

    public void SetOEB(bool e)
    {
        OEB.SetActive(e);
    }

    public void SetOET(bool e)
    {
        OET.SetActive(e);
    }

    public void SetOETLeft(bool e)
    {
        OETLeft.SetActive(e);
    }

    public void SetEOB(bool e)
    {
        EOB.SetActive(e);
    }

    public void SetEOT(bool e)
    {
        EOT.SetActive(e);
    }

    public void SetEOTRight(bool e)
    {
        EOTRight.SetActive(e);
    }

    public void SetSJ(bool e)
    {
        SJ.SetActive(e);
    }

    public void SetFab(bool e)
    {
        Fab.SetActive(e);
    }
}
