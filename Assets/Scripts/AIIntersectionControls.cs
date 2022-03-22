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

    // Action 1
    public void TurnLeftOE(float delay)
    {
        EOB.SetActive(true);
        EOT.SetActive(true);
        EOTRight.SetActive(true);
        Invoke("Case1", delay);
    }

    // Action 2
    public void Fabrique(float delay)
    {
        OEB.SetActive(true);
        OET.SetActive(true);
        OETLeft.SetActive(true);
        Invoke("Case2", delay);
    }

    // Action 3
    public void SaintJean(float delay)
    {
        Fab.SetActive(true);
        Invoke("Case3", delay);
    }

    // Action 4
    public void OEAndOE(float delay)
    {
        SJ.SetActive(true);
        OETLeft.SetActive(true);
        Invoke("Case4", delay);
    }

    private void Case1()
    {
        OETLeft.SetActive(false);
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

}
