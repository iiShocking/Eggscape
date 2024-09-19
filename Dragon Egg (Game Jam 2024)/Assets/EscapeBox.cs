using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBox : MonoBehaviour
{
    public GameLoopManager gm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.hasEscaped = true;
        }
    }
}
