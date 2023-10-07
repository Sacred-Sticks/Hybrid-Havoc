using System;
using System.Collections;
using Kickstarter.Events;
using Kickstarter.Identification;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

public class PlayerStatus : MonoBehaviour, IServiceProvider
{
    [SerializeField] private Service OnDeath;
    [SerializeField] private Service OnRespawn;
    [SerializeField] private float respawnTimer;

    public void ImplementService(EventArgs args)
    {
        if (args is not Health.DeathArgs deathArgs)
            return;
        StartCoroutine(Respawn(deathArgs));
    }

    private IEnumerator Respawn(Health.DeathArgs args)
    {
        yield return new WaitForSeconds(respawnTimer);
        OnRespawn.Trigger(new RespawnArgs(args.PlayerObject, args.PlayerID));
    }

    public class RespawnArgs : EventArgs
    {
        public RespawnArgs(GameObject playerGameObject, Player.PlayerIdentifier playerID)
        {
            PlayerGameObject = playerGameObject;
            PlayerID = playerID;
        }

        public GameObject PlayerGameObject { get; private set; }
        public Player.PlayerIdentifier PlayerID { get; private set; }
    }
}
