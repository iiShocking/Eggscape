using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFX : MonoBehaviour
{
    [SerializeField] private GameObject energy;

    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    // Update is called once per frame
    void Update()
    {
        energy.transform.Rotate(x, y, z);
    }
}
