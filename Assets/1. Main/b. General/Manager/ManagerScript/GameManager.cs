using BrokenLands;
using NPCSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    #region Variables

    #region PrimitiveVariables
    public bool isGameOver;
    #endregion
    
    #region Components
/*    [Header("PlayerComponents")]
    public GameObject playerObj;*/
    #endregion

    #region Events
    public event Action OnGameOver;
    #endregion

    #region ScriptLinkVariables
    [Header("Script Reference")]
    public Hub hub;
    public List<NPC> activeNPCList;
    public List<NPCData> npcDataList;
    #endregion

    #region Debug_Message
    public enum MessageField {
       None, Player, PlayerState, Enemy,UI, Others
    }
    public MessageField setMessageField;
    #endregion

    public TimeManager timeManager;

    public bool isPaused;
    // Current game state
    [SerializeField] private GameState currentState;
    #endregion
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if(_instance == null) {
                //Debug.LogError("GameManager instance is null! Make sure it exists in the scene.");

            }
            return _instance;
        }
    }

    

    void Awake() {
        // Check if there's already an instance
        if(_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        } else if(_instance != this) {
            Destroy(gameObject);  // Destroy duplicate instance
        }
    }

    #endregion

    #region UnityRuntimeFunction
    private void Start() {
        if(timeManager == null)
            timeManager = FindFirstObjectByType<TimeManager>();
    }

    #endregion
    

    #region GameState
    public void SetGameState(GameState newState) {
        currentState = newState;

        switch(currentState) {
            case GameState.Start:
            InitializeGame();
            break;
            case GameState.Play:
            ResumeGame();
            break;

            case GameState.Pause:
            PauseGame();
            break;

            case GameState.MainMenu:
            /*EnterMenu();*/
            break;

            case GameState.GameOver:
            HandleGameOver();
            break;
        }
        Debug.Log("Game State changed to: " + currentState);
    }

    void InitializeGame() {
        activeNPCList = Resources.Load<NPCDatabase>("NPCDatabase").ActiveNPCLists;
        npcDataList = Resources.Load<NPCDatabase>("NPCDatabase").npcDataList;
        
    }

    #region GameCommand
    public void HandleGameOver() {
        timeManager.PauseTime();
        Time.timeScale = 0.2f;
        isGameOver = true;
        OnGameOver?.Invoke();
    }

    private void PauseGame() {
        timeManager.PauseTime();
        // Additional code for pausing the game (e.g., showing pause menu UI)
    }

    private void ResumeGame() {
        timeManager.ResumeTime();
        // Additional code for resuming the game (e.g., hiding pause menu UI)
    }
    private void EnterMenu() {
        timeManager.PauseTime();
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
        ScenesManager.Instance.OnSceneChange += SceneChange;
    }
    private void OnDestroy() {
        ScenesManager.Instance.OnSceneChange -= SceneChange;
    }
    void SceneChange() {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Area1")) {  // can replace with ScenesManager.ActiveScene check;
            isGameOver = false;
        }
    }


    #endregion

    #region QualityOfLify
    public void DebugMessage(string message, MessageField active) {
        if(active == setMessageField) {
            Debug.Log(message);
        }
    }
    #endregion

    #region GetFunctions
    public List<NPC> GetActiveNPCList() {
        return FindObjectsByType<NPC>(FindObjectsSortMode.None).ToList();
    }
    //Use This Below one if needed
    public List<NPC> GetActiveNPCList1() {
        return Resources.Load<NPCDatabase>("NPCDatabase").ActiveNPCLists;
    }
    #endregion
}
public enum GameState {
    Start,
    MainMenu,
    Play,
    Pause,
    GameOver
}