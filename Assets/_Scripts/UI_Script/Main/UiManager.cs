using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public GameObject gameUI;
    public GameObject menuUI;
    public GameObject activeUI;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
            Destroy(gameObject);
    }

    public void OnUIBack() {
        activeUI.SetActive(false);
    }
}
