using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour {

    #region Variables

    #region State

    public enum state {
        MainMenu, Game1
    }
    #endregion

    #region UI_Variables
    [Header("Inventory and Other UIs")]
    public InventoryUI_M InventoryUI;
    public ChestUI_Manager chestUI;
    public EquipmentUI_M equipmentUI;
    public IntractablesUI_Manager intractableUi;

    #endregion

    public List<GameObject> topUI;
    #endregion

    #region Singleton
    public static UiManager Instance;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
    }

    #endregion

    private void OnEnable() {
        GameManager.Instance.OnSceneChange += RefreshRefrences;
    }
    private void RefreshRefrences() {
        if(InventoryUI == null) {
            Debug.Log("InventoryUI was Empty");
            InventoryUI = FindObjectOfType<InventoryUI_M>();
        } else
            Debug.Log("InventoryFound");
        if(chestUI == null) {
            Debug.Log("ChestUI is Empty");
            chestUI = FindObjectOfType<ChestUI_Manager>();
        }
        if(equipmentUI == null) {
            Debug.Log("Equipmwe was Empty");
            equipmentUI = FindObjectOfType<EquipmentUI_M>();
        }
        if(intractableUi == null) {
            Debug.Log("IntractableUI was empty");
            intractableUi = FindObjectOfType<IntractablesUI_Manager>();
        }

    }

    public void ClearTopUis() {
        foreach(GameObject go in topUI) {
            go.SetActive(false);
        }
    }
}
