using System;
using System.Collections;
using System.Collections.Generic;
using Kickstarter.Events;
using Kickstarter.Identification;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

[RequireComponent(typeof(PlayerStatus))]
public class HybridTransformatiom : MonoBehaviour, IServiceProvider
{
    [SerializeField] private float timeToTransform;
    [SerializeField] private Player hybrid;
    [Space]
    [SerializeField] private Service OnRespawn;
    [SerializeField] private Service OnDeath;
    [Space]
    [SerializeField] private Service OnHybridTransformation;

    private readonly Dictionary<Player.PlayerIdentifier, Coroutine> transformationRoutines = new Dictionary<Player.PlayerIdentifier, Coroutine>();
    private PlayerStatus statusTracker;
    
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

    private void Awake()
    {
        statusTracker = GetComponent<PlayerStatus>();
    }

    private void Start()
    {
        var players = statusTracker.Players;
        foreach (var player in players)
            StartTransformation(new PlayerStatus.RespawnArgs(player.gameObject, player.PlayerID));
    }

    public void ImplementService(EventArgs args)
    {
        switch (args)
        {
            case Health.DeathArgs deathArgs:
                StopTransformation(deathArgs);
                break;
            case PlayerStatus.RespawnArgs respawnArgs:
                StartTransformation(respawnArgs);
                break;
        }
    }

    private void StopTransformation(Health.DeathArgs args)
    {
        if (!transformationRoutines.ContainsKey(args.PlayerID))
            transformationRoutines.Add(args.PlayerID, null);
        if (transformationRoutines[args.PlayerID] == null)
            return;
        
        StopCoroutine(transformationRoutines[args.PlayerID]);
        transformationRoutines[args.PlayerID] = null;
    }

    private void StartTransformation(PlayerStatus.RespawnArgs args)
    {
        if (!transformationRoutines.ContainsKey(args.PlayerID))
            transformationRoutines.Add(args.PlayerID, null);

        transformationRoutines[args.PlayerID] = StartCoroutine(TransformationTimer(args.PlayerObject, args.PlayerID));
    }
    
    private IEnumerator TransformationTimer(GameObject playerObject, Player.PlayerIdentifier playerID)
    {
        yield return new WaitForSeconds(timeToTransform);
        hybrid.gameObject.SetActive(true);
        playerObject.SetActive(false);
        OnHybridTransformation.Trigger(new Hybrid.HybridCreationArgs(playerObject, hybrid.PlayerID, playerID));
        hybrid.PlayerID = playerID;
    }

    [Serializable]
    private struct HybridType
    {
        [SerializeField] private Player.PlayerIdentifier playerID;
        [SerializeField] private GameObject prefab;
        
        public Player.PlayerIdentifier PlayerID
        {
            get
            {
                return playerID;
            }
        }

        public GameObject Prefab
        {
            get
            {
                return prefab;
            }
        }
    }
}
