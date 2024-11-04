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
    void Start() {
        UpdateSceneState(SceneType.MainMenu);
    }

    void UpdateSceneState(SceneType scene) {
        if(scene == SceneType.MainMenu) {
            GameManager.Instance.SetGameState(GameState.MainMenu);
        }
        if(scene == SceneType.Area1) {
            GameManager.Instance.SetGameState(GameState.Start);
        }
    }
 
    public void LoadScene(SceneType scene) {
        SceneManager.LoadScene(scene.ToString());
        UpdateSceneState(scene);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += SceneChanged;
    }

    void SceneChanged(Scene a, LoadSceneMode b) {
        OnSceneChange?.Invoke();
    }


}
