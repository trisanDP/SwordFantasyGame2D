using UnityEngine;
using OriginL.System;

namespace OriginL.UI {
    public class InventoryUI_M : MonoBehaviour {
        public Transform itemParent;
        public GameObject InventoryUI_Obj;
        InventoryManager inventory;


        InventorySlot[] slot;
        private void Awake() {
            inventory = FindObjectOfType<InventoryManager>();
            if(inventory == null ) 
                gameObject.SetActive(false);
            slot = itemParent.GetComponentsInChildren<InventorySlot>();
        }
        void Start() {
            InventoryUI_Obj.SetActive(false);
        }

        private void OnEnable() {
            inventory.itemChangeCallBack += UpdateUI;
        }
        private void OnDestroy() {
            inventory.itemChangeCallBack -= UpdateUI;
        }

        void UpdateUI() {
            for(int i = 0; i < slot.Length; i++) {
                if(i < inventory.items.Count) {
                    slot[i].AddItem(inventory.items[i]);
                    Show();
                } else {
                    slot[i].ClearSlot();
                }
            }
        }

        public void Show() {
            InventoryUI_Obj.SetActive(true);
            Debug.Log("hELLO");
        }
        public void Hide() {
            InventoryUI_Obj.SetActive(false);
        }
    }
}