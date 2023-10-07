using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Rotation : MonoBehaviour, IInputReceiver<Vector2>
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
        SubscribeToInputs();
    }

    private void OnEnable()
    {
        SubscribeToInputs();
    }

    private void OnDisable()
    {
        UnsubscribeToInputs();
    }

    private void OnDestroy()
    {
        UnsubscribeToInputs();
    }

    public void SubscribeToInputs()
    {
        rotationInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);
    }

    public void UnsubscribeToInputs()
    {
        rotationInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
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

    public void ResetInputs(Player.PlayerIdentifier oldID, Player.PlayerIdentifier newID)
    {
        rotationInput.UnsubscribeToInputAction(ReceiveInput, oldID);
        rotationInput.SubscribeToInputAction(ReceiveInput, newID);
    }
}
