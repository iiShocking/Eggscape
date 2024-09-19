using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrystalBallManager : MonoBehaviour
{
    [SerializeField] private GameObject potionPool;
    [SerializeField] private GameObject abilityGate;

    [SerializeField] private GameObject orb;
    [SerializeField] private GameObject brokenOrb;
    [SerializeField] private GameObject energy;

    private void Start()
    {
        orb.SetActive(true);
        energy.SetActive(true);
    }

    public void OnSmashed()
    {
        //Change ball model
        orb.SetActive(false);
        energy.SetActive(false);
        brokenOrb.SetActive(true);
        //Enable Potion visuals
        potionPool.gameObject.SetActive(true);
        //Enable potion effects
        abilityGate.gameObject.SetActive(true);
    }
}
