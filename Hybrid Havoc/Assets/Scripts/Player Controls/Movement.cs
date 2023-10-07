using System;
using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
public class Movement : MonoBehaviour, IInputReceiver<Vector2>, IServiceProvider
{
    [SerializeField] private Vector2Input movementInput;
    [SerializeField] private float moveSpeed;
    [Space]
    [SerializeField] private Service onHybridTransformation;
    
    private Rigidbody body;
    private Player player;
    private Vector2 rawInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        movementInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);
    }

    private void FixedUpdate()
    {
        SetVelocity();
    }

    private void OnEnable()
    {
        movementInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);
        onHybridTransformation.Event += ImplementService;
    }

    private void OnDisable()
    {
        movementInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
        onHybridTransformation.Event -= ImplementService;
    }

    public void ReceiveInput(Vector2 input)
    {
        rawInput = input;
    }
    
    private void SetVelocity()
    {
        var desiredDirection = new Vector3(rawInput.x, 0, rawInput.y);
        var velocity = desiredDirection * moveSpeed;
        body.velocity = velocity;
    }

    public void ImplementService(EventArgs args)
    {
        if (args is Hybrid.HybridCreationArgs inputArgs)
            ResetInputs(inputArgs);
    }

    public void ResetInputs(Hybrid.HybridCreationArgs args)
    {
        movementInput.UnsubscribeToInputAction(ReceiveInput, args.OldID);
        movementInput.SubscribeToInputAction(ReceiveInput, args.NewID);
    }
}
