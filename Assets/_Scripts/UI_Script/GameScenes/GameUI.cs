using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameUI : MonoBehaviour {

    #region State
    public enum GameUIState {
        Pause,Play,Pause_Main,Pause_Setting,GameOver
    }

    public GameUIState ActiveState;
    #endregion

    #region Variable
    [Header("Pause/Over UI's")]
    [SerializeField] GameObject pauseUiGrp;
    [SerializeField] GameObject pauseBut_inSettingsGrp;
    [SerializeField] GameObject GameOverGrp;




    internal bool isPaused = false;

    #endregion
    private void Awake() {
        UiManager.Instance.activeState = UiManager.State.Game1;
    }
    private void Start() {
        ToggleAllUI(false);
        ActiveState = GameUIState.Play;
    }

    void Update() {
        switch(ActiveState) {  // onClick Listener // new Input system
            case GameUIState.Play:
            ToggleAllUI(false);
            PlayState();
            if(Input.GetKeyDown(KeyCode.Escape)) {
                ChangeState(GameUIState.Pause);
            }
            break;

            case GameUIState.Pause:
                ToggleAllUI(false);
                PauseState();
                if(Input.GetKeyDown(KeyCode.Escape)) {
                    ChangeState(GameUIState.Play);
                    Debug.Log("Test;");
            }
            break;

            case GameUIState.Pause_Setting:
                ToggleAllUI(false);
                PauseSettingState();

            if(Input.GetKeyDown(KeyCode.Escape)) {
                pauseBut_inSettingsGrp.SetActive(false);
                ChangeState(GameUIState.Pause);
            }
            break;

            case GameUIState.GameOver:
                
            break;
        }

    }
    /*

    void ActivateState(GameUIState activeState) {
        ToggleAllUI(false);
        switch(activeState) {
            case GameUIState.Play:
                PlayState();
                if(Input.GetKeyDown(KeyCode.Escape)) {
                    ActivateState(GameUIState.Pause);
                }   
            break;

            case GameUIState.Pause:
                PauseState();
                if(Input.GetKeyDown(KeyCode.Escape)) {
                    ActivateState(GameUIState.Play);
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

                if(Input.GetKeyDown(KeyCode.Escape)) {

                    pauseBut_inSettingsGrp.SetActive(false);
                    ChangeState(GameUIState.Pause);
                }
            break;

            case GameUIState.GameOver:
            break;

        }
    }*/

    //...................................................
    #region DefaultFUnction
    private void OnDestroy() {
        if(GameManager.Instance != null)
            GameManager.Instance.OnGameOver -= GameOverUI;
            
    }

    private void OnEnable() {
        ScenesManager.Instance.OnSceneChange += RefreshRefrences;
        GameManager.Instance.OnGameOver += GameOverUI;
    }

    private void OnDisable() {
        ScenesManager.Instance.OnSceneChange -= RefreshRefrences;
        GameManager.Instance.OnGameOver -= GameOverUI;
    }

    #endregion


    #region Functions
    private void RefreshRefrences() {
        
    
    
    }
    #endregion
    //...................................................

    #region State Function
    public void ChangeState(GameUIState newState) {
        ActiveState = newState;
    }
    void PlayState() {
        Time.timeScale = 1f;
        isPaused = false;
    }
    void PauseState() {
        Time.timeScale = 0f;
        isPaused = true;
        pauseUiGrp.SetActive(true);
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
        pauseBut_inSettingsGrp.SetActive(var);
    }

    public void GameOverUI() {
        ChangeState(GameUIState.GameOver);
        GameOverGrp.SetActive(true);
    }


    #endregion

    //...................................................
    #region Buttons
    public void OnButton_Restart() {
        ChangeState(GameUIState.Play);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

    }

    public void OnButton_MainMenu() {
        //Load main menu scene
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnButton_Settings() {
        ActiveState = GameUIState.Pause_Setting;
    }

    #endregion




}