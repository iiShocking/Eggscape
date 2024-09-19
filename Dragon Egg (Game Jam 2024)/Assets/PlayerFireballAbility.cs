using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballAbility : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public bool hasFireball;

    [SerializeField] private GameObject crosshair;
    private void Update()
    {
        if (hasFireball && crosshair.activeSelf == false)
        {
            crosshair.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0) && hasFireball)
        {
            hasFireball = false;
            LaunchFireball();
            crosshair.SetActive(false);
        }
    }

    private void LaunchFireball()
    {
        GameObject fireballInstance = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        FireballScript fireball = fireballInstance.GetComponent<FireballScript>();

        // Calculate the direction from the player to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 direction = (hitInfo.point - fireballSpawnPoint.position).normalized;
            fireball.Launch(direction);
        }
        else
        {
            // If no hit, just launch forward
            fireball.Launch(fireballSpawnPoint.forward);
        }
    }
}
