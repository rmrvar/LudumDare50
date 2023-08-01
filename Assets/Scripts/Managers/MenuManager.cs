using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string _startMenuScene = default;
    [SerializeField] private string _gameScene = default;
    [SerializeField] private string _gameOverMenuScene = default;

    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Attempted to create multiple instances of MenuManager!");

            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void LoadStartMenu(float delay = 0)
    {
        Debug.Log("Loading Start Screen!");

        LoadSceneWithDelay(_startMenuScene, delay);
    }

    public void LoadGameOverMenu(float delay = 0)
    {
        Debug.Log("Loading Game Over Screen!");

        LoadSceneWithDelay(_gameOverMenuScene, delay);
    }

    public void LoadGame(float delay = 0)
    {
        Debug.Log("Loading Game!");

        LoadSceneWithDelay(_gameScene, delay);
    }

    private void LoadSceneWithDelay(string scenePath, float delay = 0)
    {
        StartCoroutine(IE_LoadSceneWithDelay(scenePath, delay));
    }

    private IEnumerator IE_LoadSceneWithDelay(string scenePath, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(scenePath);
    }
}
