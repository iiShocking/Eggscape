using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsController : MonoBehaviour
{
    public static bool extend;
    public static GameObject gameobject;

    private void Start()
    {
        gameobject = gameObject;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (extend)
        {
            if (transform.localScale.x < 1)
            {
                transform.localScale += 3f * Time.deltaTime * Vector3.one;
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
        else
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale -= 3f * Time.deltaTime * Vector3.one;
            }
            else
            {
                transform.localScale = Vector3.zero;
                gameObject.SetActive(false);
            }
        }
    }
}
