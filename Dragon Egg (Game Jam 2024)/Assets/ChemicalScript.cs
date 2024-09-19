using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalScript : MonoBehaviour, IInteractable
{
    public CauldronMinigame manager;

    public bool isInteractable = true;

    [SerializeField] private Transform cauldron;
    private bool fall;
    private float verticalSpeed;
    private Vector3 directionToCauldron;

    private void Update()
    {
        if (fall)
        {
            transform.position += 2f * Time.deltaTime * directionToCauldron;
            transform.position -= new Vector3(0f, verticalSpeed, 0f);
            verticalSpeed += Time.deltaTime / 8f;
            transform.rotation = Quaternion.AngleAxis(180f * Time.deltaTime, Vector3.Cross(Vector3.up, directionToCauldron)) * transform.rotation;
        }
    }

    public void OnInteract()
    {
        if (!isInteractable)
        {
            return;
        }
        fall = true;
        directionToCauldron = new Vector3(cauldron.position.x, 0f, cauldron.position.z) - new Vector3(transform.position.x, 0f, transform.position.z);
        manager.PourContents(gameObject);
        StartCoroutine(BeginInteractionCooldown());
        
    }

    public string GetName() => gameObject.name;

    private IEnumerator BeginInteractionCooldown()
    {
        isInteractable = false;
        yield return new WaitForSeconds(3f);
        isInteractable = true;
    }
    
}
