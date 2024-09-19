using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class DripSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private GameObject dropPosition;

    public Material dropMat;
    
    [SerializeField]
    private GameObject drop;

    private void Start()
    {
        drop = Instantiate(dropPrefab, dropPosition.transform.position, Quaternion.identity);
    }

    public void RespawnDroplet()
    {
        Destroy(drop);
        Spawn();
    }

    private void Spawn()
    {
        drop = Instantiate(dropPrefab, dropPosition.transform.position, Quaternion.identity);
        drop.GetComponent<MeshRenderer>().material = dropMat;
    }
}
