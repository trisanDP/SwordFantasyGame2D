using NPCSystem;
using OriginL;
using OriginL.ChestSpace;
using OriginL.Inventory;
using UnityEditor.VersionControl;
using UnityEngine;


public class UiManager : MonoBehaviour {

    #region Variables


    #region UI_Variables
    [Header("Inventory and Other UIs")]

    public InventoryUI_M InventoryUI;
    public ChestUI_Manager chestUI;
    public EquipmentUI_M equipmentUI;
    public IntractablesUI_Manager intractableUi;

    public HubUiManager hubUiManager;
    public NPCUiManager npcUiManager;
    public DeliveryUIManager deliveryUiManager;
    #endregion

    #endregion

    #region Singleton
    public static UiManager Instance;

    private void Awake() {

        RefreshRefrences1();
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
    }

    #endregion
    
    private void OnEnable() {
        ScenesManager.Instance.OnSceneChange += RefreshRefrences1;


    }

    private void OnDestroy() {
        ScenesManager.Instance.OnSceneChange -= RefreshRefrences1;

    }


/*    public void ShowNPCUi(NPC npc, NPCData npcData) {
        npcUiManager.GetNPCDetail(npc, npcData);
        npcUiManager.Show();
    }*/

    void LinkManagers() {
        if(InventoryUI == null) {
            GameManager.Instance.DebugMessage("InventoryUI_Manager Was Empty but now Assigned", GameManager.MessageField.UI);
            InventoryUI = FindFirstObjectByType<InventoryUI_M>(); ;
        }
        /*            Debug.Log("InventoryFound");*/
        if(chestUI == null) {
            GameManager.Instance.DebugMessage("ChestUI_Manager Was Empty but now Assigned", GameManager.MessageField.UI);
            chestUI = FindFirstObjectByType<ChestUI_Manager>();
        }
        if(equipmentUI == null) {
            GameManager.Instance.DebugMessage("Equipment Was Empty but now Assigned", GameManager.MessageField.UI);
            equipmentUI = FindFirstObjectByType<EquipmentUI_M>();
        }
        if(intractableUi == null) {
            GameManager.Instance.DebugMessage("Intractable Was Empty but now Assigned", GameManager.MessageField.UI);
            intractableUi = FindFirstObjectByType<IntractablesUI_Manager>();
        }
        if(deliveryUiManager == null) {
            GameManager.Instance.DebugMessage("DeliveryUI Was Empty but now Assigned", GameManager.MessageField.UI);
            deliveryUiManager = FindFirstObjectByType<DeliveryUIManager>();
        }        
        if(hubUiManager == null) {
            GameManager.Instance.DebugMessage("HubUiManager Was Empty but now Assigned", GameManager.MessageField.UI);
            hubUiManager = FindFirstObjectByType<HubUiManager>();
        }if(npcUiManager == null) {
            GameManager.Instance.DebugMessage("NPCUiManager Was Empty but now Assigned", GameManager.MessageField.UI);
            npcUiManager = FindFirstObjectByType<NPCUiManager>();
        }
    }

    private void RefreshRefrences1() {
        switch(GameManager.Instance.GetCurrentState()) {
            case GameState.MainMenu:
            break;
            case GameState.Start:
                LinkManagers();
            break;
        }
    }
}
