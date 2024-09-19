using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MonoBehaviour
{
    private EggMovement eggMovement;
    private Animator animator;
    private float nextFlapTime;

    private void Awake()
    {
        eggMovement = transform.parent.GetComponent<EggMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFlapTime)
        {
            animator.SetTrigger("Flap");
            eggMovement.Flap();
            nextFlapTime = Time.time + 0.8f;
        }
    }
}