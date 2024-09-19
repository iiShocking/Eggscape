using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody _playerBody;
    private EggMovement movementScript;
    [SerializeField] private PlayerFireballAbility playerFireballAbility;

    private static AbilityManager _instance;
    public float time;

    public static AbilityManager Instance
    {
        get { return _instance; }
    }

    public Dictionary<Ability, Action> AbilityMethods;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        Deploy();
    }

    private void Deploy()
    {
        _playerBody = player.GetComponent<Rigidbody>();
        movementScript = player.GetComponent<EggMovement>();
        AbilityMethods = new Dictionary<Ability, Action>
        {
            { Ability.ForwardLaunch, ForwardLaunch },
            { Ability.UpwardLaunch, UpwardLaunch },
            { Ability.Levitate, Levitate },
            { Ability.Slowness, Slowness },
            { Ability.Fireball, Fireball},
            {Ability.Teleport, Teleport},
            { Ability.None, EmptyAbility},
            { Ability.Bouncy, Bouncy },
            { Ability.Wings,  Wings}
        };
    }

    private void ForwardLaunch()
    {
        print("Forward");
        _playerBody.velocity *= 10;
        //_playerBody.AddForce(_playerBody.velocity * 100f, ForceMode.Impulse);
    }

    private void UpwardLaunch()
    {
        print("Up");
        //_playerBody.AddForce(new Vector3(0, 500, 0));
        _playerBody.AddForce(-Physics.gravity / 3f, ForceMode.Impulse);
        movementScript.ToggleDamageable();
    }

    private void Levitate()
    {
        print("Float");
        StartCoroutine(movementScript.Float(time));
    }

    private void Bouncy()
    {
        print("Bounce");
        //StartCoroutine(movementScript.Bouncy());
        movementScript.bounceTime = 10f;
    }

    private void Slowness()
    {
        movementScript.ChangeSpeed(3, 3);
    }

    private void Fireball()
    {
        playerFireballAbility.hasFireball = true;
    }

    private void Teleport()
    {
        //TP
        return;
    }
    private void EmptyAbility()
    {
        return;
    }

    private void Wings()
    {

    }
}