using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameUI : MonoBehaviour {

    #region INSTANCE
    public static GameUI Instance;

    #endregion

    #region State
    public enum GameUIState {
        Pause,Play,Pause_Main,Pause_Setting,GameOver
    }

    public GameUIState activeState { get; private set; }
    #endregion

    #region Variable
    [Header("Pause/Over UI's")]
    [SerializeField] GameObject pauseUiGrp;
    [SerializeField] GameObject pauseBut_MainGrp;
    [SerializeField] GameObject pauseBut_inSettingsGrp;
    [SerializeField] GameObject GameOverGrp;


    [Header("Inventory and Other UIs")]
    public GameObject InventoryUI;
    public GameObject chestUI;
    public GameObject EquipmentUI;

    internal bool isPaused = false;

    #endregion

    private void Awake() {
        #region Singleton
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;
        #endregion
        GameManager.Instance.OnGameOver += GameOverUI;
    }

    private void Start() {
        ToggleAllUI(false);
        activeState = GameUIState.Play;
        
    }

    void Update() {
        switch (activeState) {
            case GameUIState.Play:
                PlayState();
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    ChangeState(GameUIState.Pause);
                }
            break;

            case GameUIState.Pause:
                PauseState();

                if(Input.GetKeyDown(KeyCode.Escape)) {
                    ChangeState(GameUIState.Play);
                }
            break;

            case GameUIState.Pause_Main:
                pauseBut_MainGrp.SetActive(true);

                if(Input.GetKeyDown(KeyCode.Escape)) {
                    pauseBut_MainGrp.SetActive(false);  // Deactivate Owns UI
                    ChangeState(GameUIState.Pause);  //Change State
                }
            break;

            case GameUIState.Pause_Setting:
                PauseSettingState();

                if (Input.GetKeyDown(KeyCode.Escape)) {

                    pauseBut_inSettingsGrp.SetActive(false); 
                    ChangeState(GameUIState.Pause);
                }
            break;

            case GameUIState.GameOver:
            break;

        }

    }


    //...................................................
    #region DefaultFUnction
    private void OnDestroy() {
        if(GameManager.Instance != null)
            GameManager.Instance.OnGameOver -= GameOverUI;
    }

    private void OnEnable() {
        GameManager.Instance.onSceneChange += RefreshRefrences;
    }


    #endregion

    //...................................................
    #region State Function
    public void ChangeState(GameUIState newState) {
        activeState = newState;
    }
    void PlayState() {
        Time.timeScale = 1f;
        UiManager.Instance.activeUI = null;
        isPaused = false;
        pauseUiGrp.SetActive(false);
        pauseBut_MainGrp.SetActive(false);
    }
    void PauseState() {
        Time.timeScale = 0f;
        isPaused = true;
        UiManager.Instance.activeUI = pauseUiGrp;
        pauseUiGrp.SetActive(true);
        pauseBut_MainGrp.SetActive(true);
    }

    void PauseSettingState(){
        pauseBut_inSettingsGrp.SetActive(true);
       
    }
    #endregion

    //...................................................
    #region ToggleFunctions
    void ToggleAllUI(bool var) {
        GameOverGrp.SetActive(var);
        pauseUiGrp.SetActive(var);
        pauseBut_MainGrp.SetActive(var);
        pauseBut_inSettingsGrp.SetActive(var);
    }

    public void GameOverUI() {
        ChangeState(GameUIState.GameOver);
        GameOverGrp.SetActive(true);
    }

    private void RefreshRefrences() {
        
    }
    #endregion


    //...................................................
    #region Buttons
    public void RestartGame() {
        ChangeState(GameUIState.Play);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

    }

    public void GoToMainMenu() {
        //Load main menu scene
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnSettings() {
        activeState = GameUIState.Pause_Setting;
    }

    #endregion




}