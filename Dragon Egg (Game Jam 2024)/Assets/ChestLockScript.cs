using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLockScript : MonoBehaviour, IInteractable
{
    [SerializeField] private GameLoopManager gameLoopManager;

    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject barrier;
    public string GetName() => "padlock";
    

    public void OnInteract()
    {
        if (gameLoopManager.hasKey)
        {
            //Open Chest (Animation + Change hitboxes)
            _animator.SetTrigger("open");
            gameObject.SetActive(false);
            gameLoopManager.hasWings = true;
            _animator.SetTrigger("send");
            barrier.GetComponent<BoxCollider>().enabled = false;
        }
        
        //Send wings into player
    }
    
}
