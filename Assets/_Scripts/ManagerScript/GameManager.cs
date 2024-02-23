using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region Variables
    public enum State {
        MainMenu, InGame, Pause, GameOver
    }

    public static State ActiveState;
    #region Events
    public event Action OnGameOver;

    #endregion

    #region PrimitiveVariables
    public bool isGameOver;
    #endregion

    #region Components
    [Header("PlayerComponents")]
    public GameObject playerObj;
    public PlayerScript playerScript;
    #endregion

    #region Important
    public static GameManager Instance;
    #endregion

    #endregion
    private void Awake(){
        #region Singleton
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else{
            Destroy(gameObject);
        }

        #endregion
    }

    public void GameOver() {
        Time.timeScale = 0.2f;
        isGameOver = true;
        OnGameOver?.Invoke();
    }

    #region Links
    private void OnEnable() {
        if(ScenesManager.Instance == null) {
            Debug.Log("Testttt3333");
        }
        ScenesManager.Instance.OnSceneChange += SceneChange;
    }

    void SceneChange() {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Area1")) {  // can replace with ScenesManager.ActiveScene check;
            isGameOver = false;
            playerObj = GameObject.Find("Player");
            playerScript = playerObj.GetComponent<PlayerScript>();
        }
    }
    #endregion
}

