using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Variables;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

[RequireComponent(typeof(PlayerStatus))]
public class HybridTransformatiom : MonoBehaviour, IServiceProvider
{
    [SerializeField] private float timeToTransform;
    [SerializeField] private float timeToWin;
    [SerializeField] private Player hybrid;
    [Space]
    [SerializeField] private Service onRespawn;
    [SerializeField] private Service onDeath;
    [SerializeField] private Service onPlayerDestroyed;
    [SerializeField] private Service onHybridDeath;
    [Space]
    [SerializeField] private Service onHybridTransformation;
    [Space]
    [SerializeField] private StringVariable winner;

    private readonly Dictionary<Player.PlayerIdentifier, Coroutine> transformationRoutines = new Dictionary<Player.PlayerIdentifier, Coroutine>();
    private PlayerStatus statusTracker;
    private Coroutine hybridLifecycle;

    private void OnEnable()
    {
        onDeath.Event += ImplementService;
        onRespawn.Event += ImplementService;
        onPlayerDestroyed.Event += ImplementService;
        onHybridDeath.Event += ImplementService;
    }

    private void OnDisable()
    {
        onDeath.Event -= ImplementService;
        onRespawn.Event -= ImplementService;
        onPlayerDestroyed.Event -= ImplementService;
        onHybridDeath.Event -= ImplementService;
    }

    private void Awake()
    {
        statusTracker = GetComponent<PlayerStatus>();
    }

    private void Start()
    {
        StartAllTransformations();
    }

    private void MurderHybrid()
    {
        if (hybridLifecycle != null)
        {
            StopCoroutine(hybridLifecycle);
            hybridLifecycle = null;
        }
        StartAllTransformations();
    }

    private void StartAllTransformations()
    {
        var players = GameManager.instance.Players;
        foreach (var player in players.Where(p => p != null))
        {
            if (!GameManager.instance.playersActive.ContainsKey(player.PlayerID))
                continue;
            if (GameManager.instance.playersActive[player.PlayerID])
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
            case Hybrid.HybridDeathArgs:
                MurderHybrid();
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
        onHybridTransformation.Trigger(new Hybrid.HybridCreationArgs(playerObject, hybrid.PlayerID, playerID));
        GameManager.instance.SetGameState(GameManager.StateMachine.GameState.HybridActive);
        hybrid.PlayerID = playerID;
        hybridLifecycle = StartCoroutine(WinTimer());
    }

    private IEnumerator WinTimer()
    {
        yield return new WaitForSeconds(timeToWin);
        GameManager.instance.winnerID = hybrid.PlayerID;
        winner.Value = hybrid.PlayerID.ToString();
        GameManager.instance.SetGameState(GameManager.StateMachine.GameState.GameOver);
    }
}
