using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GateAbility : MonoBehaviour
{
    [SerializeField] Ability ability;
    private Action _abilityMethod;
    [SerializeField] private GameObject teleportLink;
    [SerializeField] private bool onCooldown;
    [SerializeField] private float time;

    private void OnEnable()
    {
        while (AbilityManager.Instance == null)
        {
            
        }
        _abilityMethod = AbilityManager.Instance.AbilityMethods[ability];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ability == Ability.Teleport)
            {
                if (!onCooldown)
                {
                    StartCoroutine(Teleport());
                }
                return;
            }
            AbilityManager.Instance.time = time;
            _abilityMethod?.Invoke();
        }
    }

    public void SetAbility(Ability newAbility)
    {
        _abilityMethod = AbilityManager.Instance.AbilityMethods[newAbility];
        ability = newAbility;
    }

    private IEnumerator Teleport()
    {
        GameObject player = FindObjectOfType<EggMovement>().GameObject();
        onCooldown = true;
        teleportLink.GetComponent<GateAbility>().onCooldown = true;
        
        player.transform.position = teleportLink.transform.position;

        yield return new WaitForSeconds(3);

        onCooldown = false;
        teleportLink.GetComponent<GateAbility>().onCooldown = false;
    }
}
