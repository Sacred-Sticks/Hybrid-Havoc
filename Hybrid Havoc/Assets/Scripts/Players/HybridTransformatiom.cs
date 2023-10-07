using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Service OnPlayerDestroyed;
    [Space]
    [SerializeField] private Service OnHybridTransformation;

    private readonly Dictionary<Player.PlayerIdentifier, Coroutine> transformationRoutines = new Dictionary<Player.PlayerIdentifier, Coroutine>();
    private PlayerStatus statusTracker;
    
    private void OnEnable()
    {
        OnDeath.Event += ImplementService;
        OnRespawn.Event += ImplementService;
        OnPlayerDestroyed.Event += ImplementService;
    }

    private void OnDisable()
    {
        OnDeath.Event -= ImplementService;
        OnRespawn.Event -= ImplementService;
        OnPlayerDestroyed.Event -= ImplementService;
    }

    private void Awake()
    {
        statusTracker = GetComponent<PlayerStatus>();
    }

    private void Start()
    {
        var players = GameManager.instance.Players;
        foreach (var player in players.Where(p => p != null))
        {
            StartTransformation(new PlayerStatus.RespawnArgs(player.gameObject, player.PlayerID));
        }
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
            case GameManager.PlayerDestroyedArgs playerDestroyedArgs:
                CancelTransformation(playerDestroyedArgs);
                break;
        }
    }

    private void CancelTransformation(GameManager.PlayerDestroyedArgs args)
    {
        StopTransformation(new Health.DeathArgs(args.PlayerObject, args.PlayerID));
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

        if (GameManager.instance.GameState.ActiveState == GameManager.StateMachine.GameState.HybridActive)
            return;
        transformationRoutines[args.PlayerID] = StartCoroutine(TransformationTimer(args.PlayerObject, args.PlayerID));
    }
    
    private IEnumerator TransformationTimer(GameObject playerObject, Player.PlayerIdentifier playerID)
    {
        yield return new WaitForSeconds(timeToTransform);
        if (GameManager.instance.GameState.ActiveState == GameManager.StateMachine.GameState.HybridActive)
            yield break;
        if (playerObject == null)
            yield break;
        hybrid.gameObject.SetActive(true);
        playerObject.SetActive(false);
        OnHybridTransformation.Trigger(new Hybrid.HybridCreationArgs(playerObject, hybrid.PlayerID, playerID));
        GameManager.instance.SetGameState(GameManager.StateMachine.GameState.HybridActive);
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
