using System;
using System.Linq;
using Kickstarter.Events;
using Kickstarter.Identification;
using MyUILibrary;
using UnityEngine;
using UnityEngine.UIElements;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

public class RadialSetter : MonoBehaviour, IServiceProvider
{
    [SerializeField] private Service OnHealthUpdate;
    [SerializeField] private Service OnPlayeredDestroyed;
    [SerializeField] private RadialProgressCircle[] radialProgressCircles;

    private void Awake()
    {
        foreach (var radialProgress in radialProgressCircles)
        {
            radialProgress.RadialProgress = GetComponent<UIDocument>().rootVisualElement.Q<RadialProgress>(radialProgress.Name);
        }
    }

    private void OnEnable()
    {
        OnHealthUpdate.Event += ImplementService;
        OnPlayeredDestroyed.Event += ImplementService;
    }

    private void OnDisable()
    {
        OnHealthUpdate.Event -= ImplementService;
        OnPlayeredDestroyed.Event -= ImplementService;
    }

    private void UpdateRadialProgress(Player.PlayerIdentifier playerID, float progress)
    {
        if (radialProgressCircles == null)
            return;
        radialProgressCircles.FirstOrDefault(r => r.PlayerID == playerID).RadialProgress.progress = progress;
    }

    private void DestroyUIElement(Player.PlayerIdentifier playerID)
    {
        radialProgressCircles.FirstOrDefault(r => r.PlayerID == playerID).RadialProgress.RemoveFromHierarchy();
    }

    public void ImplementService(EventArgs args)
    {
        switch (args)
        {
            case RadialProgressArgs radialProgressArgs:
                UpdateRadialProgress(radialProgressArgs.PlayerID, radialProgressArgs.Progress);
                break;
            case GameManager.PlayerDestroyedArgs playerDestroyedArgs:
                DestroyUIElement(playerDestroyedArgs.PlayerID);
                break;
        }
    }

    [Serializable]
    private class RadialProgressCircle
    {
        [SerializeField] private Player.PlayerIdentifier playerID;
        [SerializeField] private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }
        public Player.PlayerIdentifier PlayerID
        {
            get
            {
                return playerID;
            }
        }
        public RadialProgress RadialProgress { get; set; }
    }

    public class RadialProgressArgs : EventArgs
    {
        public RadialProgressArgs(Player.PlayerIdentifier playerID, float progress)
        {
            PlayerID = playerID;
            Progress = progress;
        }

        public Player.PlayerIdentifier PlayerID { get; }
        public float Progress { get; }
    }
}
