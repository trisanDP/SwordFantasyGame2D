using System.Collections;
using UnityEngine;
using System;

public class UI_MainMenu: MonoBehaviour {

    #region Variable

    [Header("ButtonGroup")]
    [SerializeField] GameObject MainUI;
    [SerializeField] GameObject inOptionUI;

    [Header("Animator")]
    [SerializeField] Animator anim;

    [Header("TimeDelay")]
    [SerializeField] float delayTime;
    
    [Header("Internal")]
    bool optIsActive = false;
    #endregion

    public enum ButtonAction {
        Start,
        Option,
        Exit
    }
    private void Awake() {
        Time.timeScale = 1;
    }
    private void Start() {
        if (MainUI == null) {
            Debug.LogWarning("Needs MainUI GameObject!!! Cheers!!!");
        } else if (inOptionUI == null) {
            Debug.LogWarning("Needs inOption GameObject!!! Cheers!!!");
        }
    }


    private IEnumerator UIAni_Start() {
        yield return new WaitForSeconds(delayTime);
        ScenesManager.Instance.LoadNewScene();
    }
    private IEnumerator UIAni_Exit() {
        yield return new WaitForSeconds(delayTime);
        Application.Quit();
    }

    #region Buttons

    #region Pannel1_MainMenu
    public void OnClickStart() {
        StartCoroutine(UIAni_Start());
    }
    public void OnClickOption() {
        StartCoroutine(UIAni_Option());
    }
    public void OnClickExit() {
        StartCoroutine(UIAni_Exit());

    }
    #endregion
    public void OnClickBack() {
        if(optIsActive == true) {
            optIsActive = false;
            MainUI.SetActive(true);
            inOptionUI.SetActive(false);
        }
    }

    public void OnButtonClick(ButtonAction action) {
        switch(action) {
            case ButtonAction.Start:
            StartCoroutine(UIAnimation(action, ScenesManager.Instance.LoadNewScene));
            break;
            case ButtonAction.Option:
            StartCoroutine(UIAnimation(action, null)); // Replace OptionMethod with your actual method
            break;
            case ButtonAction.Exit:
            StartCoroutine(UIAnimation(action, Application.Quit));
            break;
        }
    }


    private IEnumerator UIAnimation(ButtonAction action, Action method) {
        // Play your animation here based on the action
        // animator.Play("YourAnimationName");

        yield return new WaitForSeconds(delayTime);
        method.Invoke();
    }
    #endregion

    #region Extra:
    private IEnumerator UIAni_Option() {
        yield return new WaitForSeconds(delayTime);
        if(optIsActive == false) {
            optIsActive = true;
            MainUI.SetActive(false);
            inOptionUI.SetActive(true);
        }
    }
    #endregion
}
