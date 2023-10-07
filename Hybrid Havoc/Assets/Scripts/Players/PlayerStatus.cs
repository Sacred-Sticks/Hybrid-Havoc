using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kickstarter.Events;
using Kickstarter.Identification;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

public class PlayerStatus : MonoBehaviour, IServiceProvider
{
    [SerializeField] private Service OnDeath;
    [SerializeField] private Service OnRespawn;
    [SerializeField] private float respawnTimer;
    [SerializeField] private Player[] players;

    public IEnumerable<Player> Players
    {
        get
        {
            return players;
        }
    }
    
    private void OnEnable()
    {
        OnDeath.Event += ImplementService;
        OnRespawn.Event += ImplementService;
    }

    private void OnDisable()
    {
        OnDeath.Event -= ImplementService;
        OnRespawn.Event -= ImplementService;
    }

    public void ImplementService(EventArgs args)
    {
        switch (args)
        {
            case Health.DeathArgs deathArgs:
                Death(deathArgs);
                break;
            case RespawnArgs respawnArgs:
                Respawn(respawnArgs);
                break;
        }
    }

    private IEnumerator Respawn(Health.DeathArgs args)
    {
        yield return new WaitForSeconds(respawnTimer);
        OnRespawn.Trigger(new RespawnArgs(args.PlayerObject, args.PlayerID));
        var healthComponent = args.PlayerObject.GetComponent<Health>();
        healthComponent.health = healthComponent.MaxHealth;
    }

    public class RespawnArgs : EventArgs
    {
        public RespawnArgs(GameObject playerObject, Player.PlayerIdentifier playerID)
        {
            PlayerObject = playerObject;
            PlayerID = playerID;
        }

        public GameObject PlayerObject { get; private set; }
        public Player.PlayerIdentifier PlayerID { get; private set; }
    }

    private void Death(Health.DeathArgs args)
    {
        args.PlayerObject.SetActive(false);
        StartCoroutine(Respawn(args));
    }

    private void Respawn(RespawnArgs args)
    {
        var player = players.FirstOrDefault(p => p.PlayerID == args.PlayerID);
        player.gameObject.SetActive(true);
    }
}
