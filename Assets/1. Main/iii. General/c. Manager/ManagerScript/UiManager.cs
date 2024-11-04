using OriginL;
using OriginL.ChestSpace;
using OriginL.Inventory;
using UnityEditor.VersionControl;
using UnityEngine;


public class UiManager : MonoBehaviour {

    #region Variables

    #region State

    public enum State {
        MainMenu, Game1
    }
    public State activeState;
    #endregion

    #region UI_Variables
    [Header("Inventory and Other UIs")]

    public InventoryUI_M InventoryUI;
    public ChestUI_Manager chestUI;
    public EquipmentUI_M equipmentUI;
    public IntractablesUI_Manager intractableUi;


    #endregion

    #endregion
    #region Singleton
    public static UiManager Instance;

    private void Awake() {
        RefreshRefrences1();
        if(Instance == null) {
            Instance = this;
        } else
            Destroy(gameObject);
        if(InventoryUI == null) {
            Debug.LogError("InventoryUI is missing!");
        }
        if(chestUI == null) {
            Debug.LogError("ChestUI is missing!");
        }
        if(equipmentUI == null) {
            Debug.LogError("EquipmentUI is missing!");
        }
        if(intractableUi == null) {
            Debug.LogError("IntractablesUI is missing!");
        }
    }

    #endregion

    private void OnEnable() {
        switch(activeState) {
            case State.Game1:
            ScenesManager.Instance.OnSceneChange += RefreshRefrences1;
            break;
            case State.MainMenu:
            break;
        }

    }
    private void RefreshRefrences1() {
        if(InventoryUI == null) {
            GameManager.Instance.DebugMessage("InventoryUI_Manager Was Empty but now Assigned",GameManager.MessageField.UI);
            InventoryUI = FindFirstObjectByType<InventoryUI_M>(); ;
        } else
            Debug.Log("InventoryFound");
        if(chestUI == null) {
            GameManager.Instance.DebugMessage("ChestUI_Manager Was Empty but now Assigned", GameManager.MessageField.UI);
            chestUI = FindFirstObjectByType<ChestUI_Manager>();
        }
        if(equipmentUI == null) {
            Debug.Log("Equipmwe was Empty");
            equipmentUI = FindFirstObjectByType<EquipmentUI_M>();
        }
        if(intractableUi == null) {
            Debug.Log("IntractableUI was empty");
            intractableUi = FindFirstObjectByType<IntractablesUI_Manager>();
        }

    }
}
