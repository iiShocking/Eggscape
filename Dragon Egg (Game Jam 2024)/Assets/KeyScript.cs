using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private GameLoopManager gameLoopManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            rb.useGravity = true;
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            gameLoopManager.ObtainedKey();
            Destroy(gameObject);
        }
    }
}
