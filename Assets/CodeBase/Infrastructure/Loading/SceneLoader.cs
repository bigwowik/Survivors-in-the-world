using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Loading
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        
        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;


        public void Load(string levelName, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadLevel(levelName, onLoaded));

        private IEnumerator LoadLevel(string nextScene, Action onLoaded = null)
        {
            Debug.Log($"[SceneLoader] Start LoadEnemies Scene: {nextScene}");
            
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                Debug.Log($"[SceneLoader] Try to start Scene that is nextScene: {nextScene}");
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitLoadingScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitLoadingScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}