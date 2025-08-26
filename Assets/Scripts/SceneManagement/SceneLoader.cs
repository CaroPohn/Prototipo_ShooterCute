using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;

    public static SceneLoader Instance => instance;

    [Tooltip("Name of the scene to load at startup.")]
    [SerializeField] private string startingScene;

    private string currentSceneName;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        ChangeScene(startingScene);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangingScene(sceneName));
    }

    private IEnumerator ChangingScene(string sceneName)
    {
        if (currentSceneName != null)
        {
            var unloadOperation = SceneManager.UnloadSceneAsync(currentSceneName);

            while (!unloadOperation.isDone)
            {
                yield return null;
            }
        }

        var loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            yield return null;
        }


        currentSceneName = sceneName;

        Scene currentScene = SceneManager.GetSceneByName(currentSceneName);
        SceneManager.SetActiveScene(currentScene);
    }
}
