using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    #region Singleton
    public static ScenesManager Instance;
    public event Action OnSceneChange;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);

        OnSceneChange?.Invoke();
    }
    #endregion

    public enum SceneType {
        MainMenu, Area1
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadScene(SceneType scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewScene() {
        SceneManager.LoadScene("Area1");
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += SceneChanged;
    }
    void SceneChanged(Scene a, LoadSceneMode b) {
        OnSceneChange?.Invoke();
    }


}
