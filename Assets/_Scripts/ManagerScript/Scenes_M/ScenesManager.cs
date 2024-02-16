using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    #region Singleton
    public static ScenesManager Instance;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
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



}
