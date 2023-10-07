using System;
using Kickstarter.Events;
using Kickstarter.Identification;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

public class Hybrid : MonoBehaviour, IServiceProvider
{
    [SerializeField] private Service OnHybridCreation;
    [Space]
    [SerializeField] private Service OnHybridDeath;

    private IInputReceiver[] inputs;
    private Player player;

    private void Awake()
    {
        inputs = GetComponents<IInputReceiver>();
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        OnHybridCreation.Event += ImplementService;
    }

    private void OnDisable()
    {
        OnHybridCreation.Event -= ImplementService;
        GameManager.instance.SetGameState(GameManager.StateMachine.GameState.HybridInactive);
        OnHybridDeath.Trigger(new HybridDeathArgs());
    }

    public void ImplementService(EventArgs args)
    {
        if (args is HybridCreationArgs hybridCreationArgs)
            InitializeHybrid(hybridCreationArgs);
    }

    private void InitializeHybrid(HybridCreationArgs args)
    {
        transform.position = args.PlayerObject.transform.position;
        transform.rotation = args.PlayerObject.transform.rotation;

        foreach (var inputReceiver in inputs)
        {
            inputReceiver.ResetInputs(args.OldID, args.NewID);
            Debug.Log($"{inputReceiver}: inputs set to {args.NewID}");
        }
    }

    public class HybridCreationArgs : EventArgs
    {
        public HybridCreationArgs(GameObject playerObject, Player.PlayerIdentifier oldID, Player.PlayerIdentifier newID)
        {
            PlayerObject = playerObject;
            OldID = oldID;
            NewID = newID;
        }

        public GameObject PlayerObject { get; private set; }
        public Player.PlayerIdentifier OldID { get; private set; }
        public Player.PlayerIdentifier NewID { get; private set; }

    }

    public class HybridDeathArgs : EventArgs
    {

    }
}
