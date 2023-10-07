using System;
using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

[RequireComponent(typeof(Player))]
public class Rotation : MonoBehaviour, IInputReceiver<Vector2>, IServiceProvider
{
    [SerializeField] private Vector2Input rotationInput;
    [SerializeField] private Service onHybridTransformation;
    [Range(0, 1)]
    [SerializeField] private float deadzone;
    
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        rotationInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);
    }

    private void OnEnable()
    {
        rotationInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);
        onHybridTransformation.Event += ImplementService;
    }

    private void OnDisable()
    {
        rotationInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
        onHybridTransformation.Event -= ImplementService;
    }

    private void OnDestroy()
    {
        rotationInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
        onHybridTransformation.Event -= ImplementService;
    }

    public void ReceiveInput(Vector2 input)
    {
        if (input == Vector2.zero)
            return;
        if (input.magnitude < deadzone)
            return;
        
        float angle = Mathf.Atan2(input.x , input.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    
    public void ImplementService(EventArgs args)
    {
        if (args is Hybrid.HybridCreationArgs inputArgs)
            ResetInputs(inputArgs);
    }

    public void ResetInputs(Hybrid.HybridCreationArgs args)
    {
        rotationInput.UnsubscribeToInputAction(ReceiveInput, args.OldID);
        rotationInput.SubscribeToInputAction(ReceiveInput, args.NewID);
    }
}
