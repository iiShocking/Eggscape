using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWater : MonoBehaviour
{
    [SerializeField] private CauldronMinigame cm;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) cm.EmptyCauldron();
        
    }
}
