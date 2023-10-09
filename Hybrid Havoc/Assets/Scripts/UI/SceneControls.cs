using System;
using Kickstarter.Events;
using UnityEngine;
using Kickstarter.Stages;

public class SceneControls : MonoBehaviour
{
    [SerializeField] private string sceneName;
    
    public void LoadScene()
    {
        EventManager.Trigger("Scene.Load", new SceneController.SceneChangeEvent(sceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
