using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBallScript : MonoBehaviour
{
    [SerializeField] private CrystalBallManager crystalBallManager;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            crystalBallManager.OnSmashed();
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
