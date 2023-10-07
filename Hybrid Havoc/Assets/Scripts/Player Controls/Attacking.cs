using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Attacking : MonoBehaviour
{
    [SerializeField] private FloatInput shootingInput;

    private Player player;
    private IAttack attacker;

    private void Awake()
    {
        player = GetComponent<Player>();
        attacker = GetComponent<IAttack>();
    }

    private void Start()
    {
        shootingInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);
    }

    private void OnEnable()
    {
        shootingInput.EnableInput();
    }

    private void OnDisable()
    {
        shootingInput.DisableInput();
    }

    private void ReceiveInput(float input)
    {
        attacker.Attack();
    }
}
