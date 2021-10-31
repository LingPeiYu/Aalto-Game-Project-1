using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string targetSceneName;

    private Scene activeScene;

    private int nextSceneIndex;

    private void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        if (activeScene.buildIndex < SceneManager.sceneCountInBuildSettings - 1) nextSceneIndex = activeScene.buildIndex + 1;
        else nextSceneIndex = activeScene.buildIndex;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(activeScene.name);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
