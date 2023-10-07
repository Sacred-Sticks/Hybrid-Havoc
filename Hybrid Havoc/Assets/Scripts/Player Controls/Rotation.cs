using System;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector2Input rotationInput;
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
        rotationInput.EnableInput();
    }

    private void OnDisable()
    {
        rotationInput.DisableInput();
    }

    private void ReceiveInput(Vector2 input)
    {
        if (input == Vector2.zero)
            return;
        if (input.magnitude < deadzone)
            return;
        
        float angle = Mathf.Atan2(input.x , input.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
