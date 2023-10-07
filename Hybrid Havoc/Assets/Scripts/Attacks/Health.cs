 using System;
 using Kickstarter.Events;
 using Kickstarter.Identification;
 using UnityEngine;
 using IServiceProvider = Kickstarter.Events.IServiceProvider;

 [RequireComponent(typeof(Player))]
public class Health : MonoBehaviour , IServiceProvider
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Service OnDeath;
    [SerializeField] private Service OnRespawn;

    private Player player;
    private float currentHealth;
    
    private float health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            if (health <= 0)
            {
                OnDeath.Trigger(new DeathArgs(gameObject, player.PlayerID));
            }
        }
    }

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void OnEnable()
    {
        OnRespawn.Event += ImplementService;
    }

    private void OnDisable()
    {
        OnRespawn.Event -= ImplementService;
    }

    public void ImplementService(EventArgs args)
    {
        if (args is PlayerStatus.RespawnArgs)
            health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
    }

    public class DeathArgs : EventArgs
    {
        public DeathArgs(GameObject playerObject, Player.PlayerIdentifier playerID)
        {
            PlayerObject = playerObject;
            PlayerID = playerID;
        }

        public GameObject PlayerObject { get; private set; }
        public Player.PlayerIdentifier PlayerID { get; private set; }
    }
}
