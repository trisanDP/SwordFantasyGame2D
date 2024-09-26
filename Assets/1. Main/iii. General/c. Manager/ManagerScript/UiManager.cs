using UnityEngine;

namespace OriginL {
    public class UiManager : MonoBehaviour {

/*        #region Variables

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
            Debug.LogError("Here");
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

        }*/
    }
}