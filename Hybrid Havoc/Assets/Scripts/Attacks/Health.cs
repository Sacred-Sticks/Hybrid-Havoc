 using System;
 using Kickstarter.Events;
 using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Service OnDeath;

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
                OnDeath.Trigger(new DeathArgs());
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;
    }

    public class DeathArgs : EventArgs
    {
        
    }
}
