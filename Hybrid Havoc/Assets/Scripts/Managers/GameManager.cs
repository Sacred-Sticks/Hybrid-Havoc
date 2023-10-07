using Kickstarter.Inputs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    private void Awake()
    {
        inputManager.Initialize();
    }

    private class StateMachine
    {
        private enum GameState
        {
            MainMenu,
            Playing,
            Paused,
        }
    }
}
