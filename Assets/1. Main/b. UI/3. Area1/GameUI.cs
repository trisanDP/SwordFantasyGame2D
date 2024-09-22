using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using TMPro;


namespace OriginL {
    public class GameUI : MonoBehaviour {

        #region State
        public enum GameUIState {
            Pause, Play, Pause_Main, Pause_Setting, GameOver
        }

        public GameUIState ActiveState { get; private set; }
        #endregion

        #region Variable
        [Header("Pause/Over UI's")]
        [SerializeField] GameObject pauseUiGrp;
        [SerializeField] GameObject pauseBut_inSettingsGrp;
        [SerializeField] GameObject GameOverGrp;
        [SerializeField] GameObject OnScreenMessageGrp;
        [SerializeField] TextMeshProUGUI onScreenMessageTxt;


        internal bool isPaused = false;

        #endregion

        private string logFilePath;

        public static GameUI instance;


        private void Awake() {
            UiManager.Instance.activeState = UiManager.State.Game1;
        }
        private void Start() {
            ToggleAllUI(false);
            ActiveState = GameUIState.Play;
            if(instance == null) {
                instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(instance);
            }

            #region messageFilePath/ OnScreen Message
            logFilePath = Application.persistentDataPath + "/MessageLog.txt";

            // Optional: create a new log file or clear the existing one at the start of the game
            if(!File.Exists(logFilePath)) {
                File.WriteAllText(logFilePath, "Log Book Created: " + System.DateTime.Now + "\n\n");
            }

            //OnScreenMessageGrp Deactivate
            OnScreenMessageGrp.SetActive(false);
            #endregion
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


        #region message display
        public IEnumerator DisplayMessage(string message, int time) {
            OnScreenMessageGrp.SetActive(true);
            onScreenMessageTxt.enabled = true;
            onScreenMessageTxt.text = message;   // Display the message
            
            LogMessage(message);
            yield return new WaitForSeconds(time);  // Wait for the specified time
            onScreenMessageTxt.enabled = false;  // Hide the message
            OnScreenMessageGrp.SetActive(false) ;

        }

        private void LogMessage(string message) {
            string timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = timeStamp + ": " + message + "\n";

            // Append the log entry to the log file
            File.AppendAllText(logFilePath, logEntry);
        }
    

        #endregion

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
        private void OnEnable() {
            ScenesManager.Instance.OnSceneChange += RefreshRefrences;
            GameManager.Instance.OnGameOver += GameOverUI;
        }
        private void OnDestroy() {
            if(GameManager.Instance != null)
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

        void PauseSettingState() {
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
}
