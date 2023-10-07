using System;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
public class Movement : MonoBehaviour, IInputReceiver<Vector2>
{
    [SerializeField] private Vector2Input movementInput;
    [SerializeField] private float moveSpeed;
    
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
    }

    private void OnDisable()
    {
        movementInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
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

    public void ResetInputs(Player.PlayerIdentifier oldID, Player.PlayerIdentifier newId)
    {
        throw new NotImplementedException();
    }
}
