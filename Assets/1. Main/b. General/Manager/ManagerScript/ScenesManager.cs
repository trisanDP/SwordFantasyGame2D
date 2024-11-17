using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // To handle UI elements like loading bar

public class ScenesManager : MonoBehaviour {
    #region Singleton
    public static ScenesManager Instance;
    public event Action OnSceneChange;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Enums
    public enum SceneType {
        MainMenu,
        Area1,
        Area2,
    }
    #endregion

    #region Fields
    private SceneType targetScene;
    [SerializeField] private GameObject loadingScreenUI; // Reference to the loading screen UI
    [SerializeField] private Slider loadingBar; // Reference to the loading bar
    #endregion

    #region Public Methods
    public void LoadScene(SceneType scene) {
        targetScene = scene;
        ShowLoadingScreen(true);  // Show the loading UI
        StartCoroutine(LoadTargetScene());
    }

    public SceneType GetCurrentSceneType() {
        if(TryGetSceneType(SceneManager.GetActiveScene().name, out SceneType sceneType)) {
            return sceneType;
        }
        return default;
    }
    #endregion

    #region Private Loading Methods
    private IEnumerator LoadTargetScene() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene.ToString());

        while(!asyncLoad.isDone) {
            // Update loading bar based on progress
            if(loadingBar != null) {
                loadingBar.value = asyncLoad.progress;
            }
            yield return null; // Wait for the next frame
        }

        OnSceneChange?.Invoke(); // Notify listeners after the scene has loaded
        ShowLoadingScreen(false);  // Hide the loading UI after the scene is loaded
    }
    #endregion

    #region Utility Methods
    private void ShowLoadingScreen(bool show) {
        // Activate or deactivate the loading screen UI
        if(loadingScreenUI != null) {
            loadingScreenUI.SetActive(show);
        }
    }

    private bool TryGetSceneType(string sceneName, out SceneType sceneType) {
        return Enum.TryParse(sceneName, out sceneType);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        OnSceneChange?.Invoke(); // Notify listeners when a new scene is loaded
    }
    #endregion
}
