using System.Collections;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Attacking : MonoBehaviour
{
    [SerializeField] private FloatInput shootingInput;
    [Range(0, 1)]
    [SerializeField] private float deadzone;
    [Space]
    [SerializeField] private float fireRate;

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
    }

    private void OnDisable()
    {
        shootingInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
    }

    private void ReceiveInput(float input)
    {
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
}
