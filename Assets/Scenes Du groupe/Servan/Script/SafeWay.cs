﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeWay : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> salles;
    private List<GameObject> pointsInteret;
    [SerializeField]
    private List<GameObject> ptsIntSalle0;
    [SerializeField]
    private List<GameObject> ptsIntSalle1;
    [SerializeField]
    private List<GameObject> ptsIntSalle2;
    [SerializeField]
    private List<GameObject> ptsIntSalle3;
    [SerializeField]
    private List<GameObject> ptsIntSalle4;
    [SerializeField]
    private List<GameObject> listAgent;

    private GameObject currentAgent;
    private IaPnj currentIaPnj;
    private GameObject nextDestinationSalle;
    private GameObject nextDestinationPointInt;
    private Vector3 nextObj;


    [SerializeField]
    private Animator currentAnimator;

    private int rng;
    private int startRng;
    private int rngSalle;
    [SerializeField]
    private int tempsAttente;

    void Start()
    {
        rngSalle = 0;
        if(tempsAttente == 0)
        {
            tempsAttente = 5;
        }
    }

    void Update()
    {
        ParcourirAgent();
    }

    void ParcourirAgent()
    {
        foreach (GameObject agent in listAgent)
        {
            currentAgent = agent;
            currentIaPnj = currentAgent.GetComponent<IaPnj>();
            currentAnimator = currentAgent.GetComponent<Animator>();
            if(!currentIaPnj.Agent.hasPath && currentIaPnj.canWait == true)
            {
                currentAnimator.SetBool("IsWalking", false);
                currentIaPnj.startWaitTime = Time.time;
                currentIaPnj.canWait = false;
            }
            if (!currentIaPnj.Agent.hasPath && Time.time > currentIaPnj.startWaitTime + tempsAttente && currentIaPnj.canWait == false)
            {
                currentAnimator.SetBool("IsWalking", true);
                currentIaPnj.canWait = true;
                VerificationEtatAgent();
                SetDestinationAgent();
                nextDestinationSalle = null;
                nextDestinationPointInt = null;
            }
        }
    }

    void VerificationEtatAgent()
    {
        switch (currentIaPnj.etat)
        {
            case IaPnj.Etat.Attente:
                break;
            case IaPnj.Etat.PointInteretSalle:
                SelectPointInt();
                break;
            //case IaPnj.Etat.SelectSalle:
                //SelectSalle();
                //break;
            default:
                break;
        }
    }

    void SelectSalle()
    {
        nextObj = salles[rngSalle].transform.position;
        currentIaPnj.currentSalle = salles[rngSalle];
        rngSalle += 1;
        if (rngSalle > salles.Count - 1)
        {
            rngSalle = 0;
        }
        currentIaPnj.etat = IaPnj.Etat.PointInteretSalle;
    }

    void SelectPointInt()
    {
        DetectionSalle();
        rng = Random.Range(0, pointsInteret.Count);
        startRng = rng;
        nextDestinationPointInt = pointsInteret[rng];
        nextObj = nextDestinationPointInt.transform.position;
        currentIaPnj.ptsInts = nextDestinationPointInt;
        rng = Random.Range(0, 10);
        if(rng >= 8)
        {
            //currentIaPnj.etat = IaPnj.Etat.SelectSalle;
        }
    }

    void SetDestinationAgent()
    {
        currentIaPnj.Agent.destination = nextObj;
    }

    void DetectionSalle()
    {
        if (currentIaPnj.currentSalle == salles[0])
        {
            pointsInteret = ptsIntSalle0;
        }
        if (currentIaPnj.currentSalle == salles[1])
        {
            pointsInteret = ptsIntSalle1;
        }
        if (currentIaPnj.currentSalle == salles[2])
        {
            pointsInteret = ptsIntSalle2;
        }
        if (currentIaPnj.currentSalle == salles[3])
        {
            pointsInteret = ptsIntSalle3;
        }
        if (currentIaPnj.currentSalle == salles[4])
        {
            pointsInteret = ptsIntSalle4;
        }
    }
}
