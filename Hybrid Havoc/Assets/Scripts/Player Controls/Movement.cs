using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
public class Movement : MonoBehaviour, IInputReceiver<Vector2>
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
        SubscribeToInputs();
    }

    private void FixedUpdate()
    {
        SetVelocity();
    }

    private void OnEnable()
    {
        SubscribeToInputs();
    }

    private void OnDisable()
    {
        UnsubscribeToInputs();
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

    public void ResetInputs(Player.PlayerIdentifier oldID, Player.PlayerIdentifier newID)
    {
        movementInput.UnsubscribeToInputAction(ReceiveInput, oldID);
        movementInput.SubscribeToInputAction(ReceiveInput, newID);
    }

    public void SubscribeToInputs()
    {
        movementInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);
    }

    public void UnsubscribeToInputs()
    {
        movementInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
    }
}
