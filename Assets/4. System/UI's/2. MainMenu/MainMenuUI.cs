using UnityEngine;

public class UI_MainMenu : MonoBehaviour {

    #region Variable

    [Header("UI_Pannel_Group")]
    [SerializeField] GameObject Pannel1_MainMenu;
    [SerializeField] GameObject pannel2_Option;

    [Header("Animator")]
    [SerializeField] Animator anim;

    [Header("TimeDelay")]
    [SerializeField] float delayTime;

    #endregion

    public enum State {
        MainMenu, Setting,
    }

    private void Awake() {
        Time.timeScale = 1;
        SetActiveState(State.MainMenu);
    }

    private void Start() {
        if(Pannel1_MainMenu == null) {
            Debug.LogWarning("Needs MainUI GameObject!!! Cheers!!!");
        } else if(pannel2_Option == null) {
            Debug.LogWarning("Needs inOption GameObject!!! Cheers!!!");
        }
    }

    public void SetActiveState(State activeState) {
        switch(activeState) {
            case State.MainMenu:
            ToggleAllPannel(false);
            Pannel1_MainMenu.SetActive(true);
            break;

            case State.Setting:
            ToggleAllPannel(false);
            pannel2_Option.SetActive(true);
            break;
        }
    }

    #region Buttons

    #region Pannel1_MainMenu
    public void OnButtonNewGame() {
        ScenesManager.Instance.LoadNewScene();
    }
    public void OnButtonSettings() {
        SetActiveState(State.Setting);
        
    }
    public void OnButtonExit() {
        Application.Quit();

    }
    #endregion

    #region Pannel2_Option
    public void OnButtonBack() {
        SetActiveState(State.MainMenu);
    }
    public void OnButtonGraphic() {
        Debug.Log("In Construction");
    }
    #endregion

    #endregion

    #region Extra:

    public void ToggleAllPannel(bool var) {
        Pannel1_MainMenu.SetActive(var);
        pannel2_Option.SetActive(var);
    }
    #endregion

    
}
