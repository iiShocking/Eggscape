using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private GameObject currentCheckpoint;

    [SerializeField] private GameObject player;

    private void Start()
    {
        currentCheckpoint.transform.position = player.transform.position;
    }

    //Assign a checkpoint to current assigned checkpoint
    public void AssignCheckpoint(GameObject checkpoint) => currentCheckpoint = checkpoint;
    
    //Move player to checkpoint position
    public void RespawnAtCheckpoint()
    {
        player.transform.position = currentCheckpoint.transform.position;
        player.transform.rotation = Quaternion.Euler(-90, 0 , 0);
        player.GetComponent<EggMovement>().inStand = currentCheckpoint.name == "SpawnPosition";
    }
}