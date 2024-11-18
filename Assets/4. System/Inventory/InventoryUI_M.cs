using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace OriginL.Inventory {
    public class InventoryUI_M : MonoBehaviour {
        public Transform itemParent;
        public GameObject InventoryUI_Obj;
        private InventoryManager inventory;

        [SerializeField]
        private List<TextMeshProUGUI> storedResourceTxt;

        private InventorySlot[] slot;

        private void Awake() {
            inventory = InventoryManager.instance;
        }

        void Start() {
            slot = itemParent.GetComponentsInChildren<InventorySlot>();
            InventoryUI_Obj.SetActive(false);
            ResetResourceValue();
        }

        private void OnEnable() {
            inventory.itemChangeCallBack += UpdateItemSlotUi;
            inventory.OnResourceAddCallBack += UpdateStorageUI;
        }

        private void OnDestroy() {
            inventory.itemChangeCallBack -= UpdateItemSlotUi;
            inventory.OnResourceAddCallBack -= UpdateStorageUI;
        }

        // Update the inventory slots
        void UpdateItemSlotUi() {
            for(int i = 0; i < slot.Length; i++) {
                if(i < inventory.items.Count) {
                    slot[i].AddItem(inventory.items[i]);
                    Show();
                } else {
                    slot[i].ClearSlot();
                }
            }
        }

        // Update the storage UI to display resources and their amounts
        void UpdateStorageUI() {
            ResetResourceValue(); // Reset UI elements to default
            int index = 0;

            foreach(var storedResource in inventory.storage.storedResources) {
                if(index < storedResourceTxt.Count) {
                    var resourceType = storedResource.resourceType;  // Get the resource type
                    var amount = storedResource.amount;  // Get the amount of the resource

                    // Update the UI element with the resource's amount
                    storedResourceTxt[index].text = $"{amount}";  // Display resource name and amount

                    // Optionally, log the updated resource (for debugging)
                    // Debug.Log($"Updating resource {index}: {resourceType.resourceName} - {amount}");
                }
                index++;
            }
        }


        // Reset the resource display
        void ResetResourceValue() {
            foreach(var text in storedResourceTxt) {
                text.text = "0"; // Reset all UI text elements
            }
        }

        #region ShowHide

        public void Toggle() {
            InventoryUI_Obj.SetActive(!InventoryUI_Obj.activeSelf);
        }

        public void Show() {
            InventoryUI_Obj.SetActive(true);
        }

        public void Hide() {
            InventoryUI_Obj.SetActive(false);
        }

        #endregion
    }
}