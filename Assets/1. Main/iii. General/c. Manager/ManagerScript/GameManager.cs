using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    #region Variables

    #region GameState:
    public enum State {
        MainMenu, InGame, Pause, GameOver
    }
    [Header("GameState")]
    public static State ActiveState;
    #endregion

    #region Events
    public event Action OnGameOver;
    #endregion
    
    #region PrimitiveVariables
    public bool isGameOver;
    #endregion
    
    #region Components
    [Header("PlayerComponents")]
    public GameObject playerObj;
    #endregion


    #region Debug_Message
    public enum MessageField {
       None, Player, PlayerState, Enemy, Others
    }
    public MessageField setMessageField;
    #endregion

    #endregion

    #region Instance;
    public static GameManager Instance;
    #endregion

    private void Awake() {
        #region Singleton
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
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
        }
    }
    
    public void DebugMessage(string message, MessageField active) {
        if(active == setMessageField) {
            Debug.Log(message);
        }

    }

    public void onScreenMessage(string message) {
        
    }
    #endregion
}
