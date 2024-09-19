using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the fireball after 'lifetime' seconds
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with other objects
        Debug.Log("Fireball hit " + collision.gameObject.name);

        // Destroy the fireball on impact
        Destroy(gameObject);
    }

    public void Launch(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
    }
}
