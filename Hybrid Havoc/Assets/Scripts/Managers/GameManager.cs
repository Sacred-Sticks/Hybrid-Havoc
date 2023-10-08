using System;
using System.Collections.Generic;
using System.Linq;
using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using Kickstarter.Stages;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

public class GameManager : MonoBehaviour, IServiceProvider
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private List<Player> players;
    [Space]
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string gameplaySceneName;
    [SerializeField] private string gameoverSceneName;
    [Space]
    [SerializeField] private string uiWinnerElement;
    [Space]
    [SerializeField] private StateMachine.GameState initialState;
    [Space]
    [SerializeField] private Service onDeath;
    [SerializeField] private Service onRespawn;
    [Space]
    [SerializeField] private Service onPlayerDestroyed;

    public static GameManager instance;
    
    public Player.PlayerIdentifier winnerID { private get; set; }
    public StateMachine GameState { get; private set; }
    public IEnumerable<Player> Players
    {
        get
        {
            return players;
        }
    }
    public readonly Dictionary<Player.PlayerIdentifier, bool> playersActive = new Dictionary<Player.PlayerIdentifier, bool>();

    private int numPlayers;

    private void Awake()
    {
        instance = this;
        inputManager.Initialize(out int playerCount);
        numPlayers = playerCount - 1; // Remove one because we aren't using Keyboard & Mouse
        
        InitializeStateMachine();
    }

    private void Start()
    {
        RemoveExtraPlayers();
        foreach (var player in players.Where(p => p != null).ToArray())
            TogglePlayerStatus(player.PlayerID);
    }

    private void OnEnable()
    {
        onDeath.Event += ImplementService;
        onRespawn.Event += ImplementService;
    }

    private void OnDisable()
    {
        onDeath.Event -= ImplementService;
        onRespawn.Event -= ImplementService;
    }

    private void RemoveExtraPlayers()
    {
        switch (numPlayers)
        {
            case 0:
                RemovePlayer(1);
                RemovePlayer(2);
                RemovePlayer(3);
                RemovePlayer(4);
                break;
            case 1:
                RemovePlayer(1);
                RemovePlayer(2);
                RemovePlayer(3);
                break;
            case 2:
                RemovePlayer(1);
                RemovePlayer(2);
                break;
            case 3:
                RemovePlayer(1);
                break;
            case 4:
                break;
        }
        void RemovePlayer(int indexFromEnd)
        {
            var obj = players[^indexFromEnd];
            players[^1] = null;
            onPlayerDestroyed.Trigger(new PlayerDestroyedArgs(obj.gameObject, obj.PlayerID));
            Destroy(obj.gameObject);
        }
    }

    private void InitializeStateMachine()
    {
        GameState = new StateMachine(initialState);
    }

    public void SetGameState(StateMachine.GameState newState)
    {
        GameState.TransitionState(newState);
    }

    private void TogglePlayerStatus(Player.PlayerIdentifier playerID)
    {
        if (!playersActive.ContainsKey(playerID))
            playersActive.Add(playerID, false);
        playersActive[playerID] = !playersActive[playerID];
    }

    public void ImplementService(EventArgs args)
    {
        switch (args)
        {
            case Health.DeathArgs deathArgs:
                TogglePlayerStatus(deathArgs.PlayerID);
                break;
            case PlayerStatus.RespawnArgs respawnArgs:
                TogglePlayerStatus(respawnArgs.PlayerID);
                break;
        }
    }

    public class StateMachine
    {
        public StateMachine(GameState initialState)
        {
            ActiveState = initialState;
            SetTransitions();
        }

        public GameState ActiveState { get; set; }
        private readonly Dictionary<GameState, GameState[]> stateTransitions = new Dictionary<GameState, GameState[]>();

        public enum GameState
        {
            MainMenu,
            HybridInactive,
            HybridActive,
            GameOver,
        }

        private void SetTransitions()
        {
            stateTransitions.Add(GameState.MainMenu, new[]
            {
                GameState.HybridInactive,
            });
            stateTransitions.Add(GameState.HybridInactive, new[]
            {
                GameState.HybridActive,
            });
            stateTransitions.Add(GameState.HybridActive, new[]
            {
                GameState.HybridInactive,
                GameState.GameOver,
            });
            stateTransitions.Add(GameState.GameOver, new[]
            {
                GameState.MainMenu,
            });
        }

        public void TransitionState(GameState newState)
        {
            if (!stateTransitions[ActiveState].Contains(newState))
                return;

            ActiveState = newState;
            switch (ActiveState)
            {
                case GameState.MainMenu:
                    EventManager.Trigger("Scene.Load", new SceneController.SceneChangeEvent(instance.mainMenuSceneName));
                    break;
                case GameState.HybridInactive:
                    break;
                case GameState.HybridActive:
                    break;
                case GameState.GameOver:
                    EventManager.Trigger("Scene.Load", new SceneController.SceneChangeEvent(instance.gameoverSceneName));
                    break;
            }
        }
    }

    public class PlayerDestroyedArgs : EventArgs
    {
        public PlayerDestroyedArgs(GameObject playerObject, Player.PlayerIdentifier playerID)
        {
            PlayerObject = playerObject;
            PlayerID = playerID;
        }

        public GameObject PlayerObject { get; }
        public Player.PlayerIdentifier PlayerID { get; }
    }
}
