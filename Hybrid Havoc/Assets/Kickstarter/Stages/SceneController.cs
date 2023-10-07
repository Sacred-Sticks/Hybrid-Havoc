using System.Collections.Generic;
using Kickstarter.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kickstarter.Stages
{
    public class SceneController : MonoBehaviour
    {
        private const string prefix = "Scene";
        private const string load = "Load";

        private readonly Dictionary<string, int> sceneIndices = new Dictionary<string, int>();

        private void Start()
        {
            SetSceneIndices();
        }

        private void OnEnable()
        {
            EventManager.AddListener<SceneChangeEvent>($"{prefix}.{load}", LoadScene);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<SceneChangeEvent>($"{prefix}.{load}", LoadScene);
        }

        private void SetSceneIndices()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;

            for (int i = 0; i < sceneCount; i++)
            {
                string sceneName = GetSceneNameFromBuildIndex(i);
                sceneIndices.Add(sceneName, i);
            }
        }
        
        private string GetSceneNameFromBuildIndex(int index) {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
 
            return sceneName;
        }

        private void LoadScene(SceneChangeEvent parameters)
        {
            string sceneName = parameters.SceneName;
            if (!sceneIndices.ContainsKey(sceneName))
                return;
            int sceneIndex = sceneIndices[sceneName];
            SceneManager.LoadScene(sceneIndex);
        }

        public class SceneChangeEvent
        {
            public string SceneName { get; }

            public SceneChangeEvent(string sceneName)
            {
                SceneName = sceneName;
            }
        }
    }
}
