using System.Collections;
using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Attacking : MonoBehaviour, IInputReceiver<float>
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

    private bool canAttack = true;
    private float attackCooldown;

    private void Awake()
    {
        player = GetComponent<Player>();
        attacker = GetComponent<IAttack>();
    }

    private void Start()
    {
        SubscribeToInputs();

        attackCooldown = 1 / fireRate;
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

    public void ReceiveInput(float input)
    {
        if (!gameObject.activeInHierarchy)
            return;
        if (input > deadzone)
        {
            if (canAttack)
                attack ??= StartCoroutine(AttackTimer());
            return;
        }
        if (attack == null)
            return;
        StopCoroutine(attack);
        attack = null;
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackTimer()
    {
        while (true)
        {
            attacker.Attack();
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void ResetInputs(Player.PlayerIdentifier oldID, Player.PlayerIdentifier newID)
    {
        shootingInput.UnsubscribeToInputAction(ReceiveInput, oldID);
        shootingInput.SubscribeToInputAction(ReceiveInput, newID);
    }

    public void SubscribeToInputs()
    {
        shootingInput.SubscribeToInputAction(ReceiveInput, player.PlayerID);
    }

    public void UnsubscribeToInputs()
    {
        shootingInput.UnsubscribeToInputAction(ReceiveInput, player.PlayerID);
    }
}
