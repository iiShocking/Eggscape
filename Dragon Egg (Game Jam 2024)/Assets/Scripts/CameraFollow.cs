using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform egg;
    [SerializeField] private float sensitivity;
    private Vector3 offset;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        offset = transform.position - egg.position;
    }

    private void Update()
    {
        transform.position = egg.position + offset;

        transform.RotateAround(egg.position, Vector3.up, Input.GetAxis("Mouse X") * sensitivity);
        transform.RotateAround(egg.position, transform.right, -Input.GetAxis("Mouse Y") * sensitivity);

        offset = transform.position - egg.position;
    }
}