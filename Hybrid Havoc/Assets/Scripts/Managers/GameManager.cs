using System.Collections.Generic;
using System.Linq;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [Range(2,4)]
    [SerializeField] private int numPlayers;
    [SerializeField] private List<Player> players;
    [Space]
    [SerializeField] private StateMachine.GameState initialState;

    public static GameManager instance;

    public StateMachine GameState { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(this);

        inputManager.Initialize();
    }

    private void Start()
    {
        RemoveExtraPlayers();
        InitializeStateMachine();
    }

    private void RemoveExtraPlayers()
    {

        switch (numPlayers)
        {
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
    
    public class StateMachine
    {
        public StateMachine(GameState initialState)
        {
            currentState = initialState;
            SetTransitions();
        }

        private GameState currentState;
        private Dictionary<GameState, GameState[]> stateTransitions = new Dictionary<GameState, GameState[]>();
        
        public enum GameState
        {
            MainMenu,
            HybridInactive,
            HybridActive,
            GameOver,
        }

        private void SetTransitions()
        {
            stateTransitions.Add(GameState.MainMenu, new GameState[]
            {
                GameState.HybridInactive,
            });
            stateTransitions.Add(GameState.HybridInactive, new GameState[]
            {
                GameState.HybridActive,
            });
            stateTransitions.Add(GameState.HybridActive, new GameState[]
            {
                GameState.HybridInactive,
                GameState.GameOver,
            });
            stateTransitions.Add(GameState.GameOver, new GameState[]
            {
                GameState.MainMenu,
            });
        }

        public void TransitionState(GameState newState)
        {
            if (stateTransitions[currentState].Contains(newState))
                currentState = newState;
        }
    }
}
