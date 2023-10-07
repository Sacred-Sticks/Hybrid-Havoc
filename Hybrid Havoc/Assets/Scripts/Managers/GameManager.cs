using Kickstarter.Inputs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(this);
        
        inputManager.Initialize();
    }

    private class StateMachine
    {
        private enum GameState
        {
            MainMenu,
            Playing,
            HybridActive,
            Paused,
        }
    }
}
