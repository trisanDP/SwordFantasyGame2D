using BrokenLands;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    #region Variables

    #region PrimitiveVariables
    public bool isGameOver;
    #endregion
    
    #region Components
    [Header("PlayerComponents")]
    public GameObject playerObj;
    #endregion


    #region GameState:
    public enum GameState {
        Play,
        Pause,
        Menu,
        GameOver
    }

    // Current game state
    private GameState currentState;
    #endregion

    #region Events
    public event Action OnGameOver;
    #endregion

    #region TimeVariables
    [Header("Time Settings")]
    public float realMinPerGameMin = 1f; // Default: 1 real minute = 30 in-game minutes
    public int day = 1;
    private float timeOfDay; // This tracks the time of day.
    private float realTimePassed = 0f; // Time tracker for real time.
    private bool isNight;
    public bool isPaused;
    public bool use24HourFormat;

    //Event
    public event Action OnNightStarted;
    public event Action OnDayStarted;

    #endregion

    #region Debug_Message
    public enum MessageField {
       None, Player, PlayerState, Enemy,UI, Others
    }
    public MessageField setMessageField;
    #endregion

    private TimeManager timeManager;

    #endregion


    public UiManager uiManager;
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if(_instance == null) {
                Debug.LogError("GameManager instance is null! Make sure it exists in the scene.");
            }
            return _instance;
        }
    }

    private void Awake() {
        // Check if there's already an instance
        if(_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        } else if(_instance != this) {
            Destroy(gameObject);  // Destroy duplicate instance
        }
        if(timeManager == null) 
            timeManager = FindFirstObjectByType<TimeManager>();
        uiManager = UiManager.Instance;
        
    }
    #endregion

    #region UnityRuntimeFunction
    void Start() {
        SetGameState(GameState.Play);
    }

    #endregion


    #region GameState
    public void SetGameState(GameState newState) {
        currentState = newState;

        switch(currentState) {
            case GameState.Play:
            ResumeGame();
            break;

            case GameState.Pause:
            PauseGame();
            break;

            case GameState.Menu:
            EnterMenu();
            break;

            case GameState.GameOver:
            HandleGameOver();
            break;
        }

        Debug.Log("Game State changed to: " + currentState);
    }
    #region GameCommand
    public void HandleGameOver() {
        timeManager?.PauseTime();
        Time.timeScale = 0.2f;
        isGameOver = true;
        OnGameOver?.Invoke();
    }

    private void PauseGame() {
        timeManager?.PauseTime();
        // Additional code for pausing the game (e.g., showing pause menu UI)
    }

    private void ResumeGame() {
        timeManager?.ResumeTime();
        // Additional code for resuming the game (e.g., hiding pause menu UI)
    }
    private void EnterMenu() {
        timeManager?.PauseTime();
        // Additional code for menu behavior
    }

    // Getter for current game state
    public GameState GetCurrentState() {
        return currentState;
    }
    #endregion
    #endregion



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


    #endregion

    public void StartHubManagementGame() {

    }

    #region QualityOfLify
    public void DebugMessage(string message, MessageField active) {
        if(active == setMessageField) {
            Debug.Log(message);
        }
    }
    #endregion
}
