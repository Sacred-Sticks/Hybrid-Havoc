 using System;
 using Kickstarter.Events;
 using Kickstarter.Identification;
 using UnityEngine;
 
 [RequireComponent(typeof(Player))]
public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Service OnDeath;

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
                OnDeath.Trigger(new DeathArgs(gameObject, player.PlayerID));
        }
    }

    private void Awake()
    {
        player = GetComponent<Player>();
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
