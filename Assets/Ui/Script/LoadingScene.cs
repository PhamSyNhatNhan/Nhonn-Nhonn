using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreenUI;
    
    private void Start()
    {
        Time.timeScale = 0;
        
        StartCoroutine(LoadSceneAsync());
    }
    
    private IEnumerator LoadSceneAsync()
    {
        loadingScreenUI.SetActive(true);
        
        yield return null;
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentSceneIndex);
        
        asyncOperation.allowSceneActivation = false;
        
        while (!asyncOperation.isDone && asyncOperation.progress < 0.9f)
        {
            yield return null;
        }
        
        loadingScreenUI.SetActive(false);
        
        Time.timeScale = 1;
        
        asyncOperation.allowSceneActivation = true;
    }
}
