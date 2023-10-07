using System;
using System.Collections;
using System.Xml.Schema;
using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

[RequireComponent(typeof(Player))]
public class Attacking : MonoBehaviour, IInputReceiver<float>, IServiceProvider
{
    [SerializeField] private FloatInput shootingInput;
    [Range(0, 1)]
    [SerializeField] private float deadzone;
    [SerializeField] private float fireRate;
    [Space]
    [SerializeField] private Service onHybridTransformation;

    private Player player;
    private IAttack attacker;
    private Coroutine attack;

    private float attackCooldown;

    private void Awake()
    {
        player = GetComponent<Player>();
        attacker = GetComponent<IAttack>();
    }

    private void Start()
    {
        shootingInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);

        attackCooldown = 1 / fireRate;
    }

    private void OnEnable()
    {
        shootingInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);
        onHybridTransformation.Event += ImplementService;
    }

    private void OnDisable()
    {
        shootingInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
        onHybridTransformation.Event -= ImplementService;
    }

    private void OnDestroy()
    {
        shootingInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
        onHybridTransformation.Event -= ImplementService;
    }

    public void ReceiveInput(float input)
    {
        if (!gameObject.activeInHierarchy)
            return;
        if (input > deadzone)
        {
            attack ??= StartCoroutine(AttackTimer());
            return;
        }
        if (attack == null)
            return;
        StopCoroutine(attack);
        attack = null;
    }

    private IEnumerator AttackTimer()
    {
        while (true)
        {
            attacker.Attack();
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    public void ImplementService(EventArgs args)
    {
        if (args is Hybrid.HybridCreationArgs inputArgs)
            ResetInputs(inputArgs);
    }

    public void ResetInputs(Hybrid.HybridCreationArgs args)
    {
        shootingInput.UnsubscribeToInputAction(ReceiveInput, args.OldID);
        shootingInput.SubscribeToInputAction(ReceiveInput, args.NewID);
    }
}
