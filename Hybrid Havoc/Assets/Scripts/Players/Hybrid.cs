using System;
using Kickstarter.Identification;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

public class Hybrid : MonoBehaviour, IServiceProvider
{
    private IInputReceiver[] inputs;
    private Player player;
    
    private void Awake()
    {
        inputs = GetComponents<IInputReceiver>();
        player = GetComponent<Player>();
    }

    public void ImplementService(EventArgs args)
    {
        if (args is HybridCreationArgs hybridCreationArgs)
            InitializeHybrid(hybridCreationArgs);
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

    private void InitializeHybrid(HybridCreationArgs args)
    {
        transform.position = args.PlayerObject.transform.position;
        transform.rotation = args.PlayerObject.transform.rotation;
    }
}
