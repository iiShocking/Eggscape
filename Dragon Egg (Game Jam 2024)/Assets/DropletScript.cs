using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletScript : MonoBehaviour
{
    [SerializeField]
    private DripSpawnerScript ds;
    // Start is called before the first frame update
    void Start()
    {
        ds = FindObjectOfType<DripSpawnerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            ds.RespawnDroplet();
        }
    }
}
