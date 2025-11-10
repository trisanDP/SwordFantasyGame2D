using UnityEngine;

namespace OriginL.ChestSpace {
    public class ChestUI_Manager : MonoBehaviour {
        public Transform itemParent;            // The parent transform to hold the slot UI elements
        public GameObject ChestUi;              // The Chest UI GameObject to show/hide
        public GameObject itemSlotPrefab;       // The prefab to instantiate for each item slot

        private ChestSlot[] slot;               // Array to hold the instantiated slots

        void Start() {
            if(itemParent != null) {
                // Ensure that the itemParent starts empty
                foreach(Transform child in itemParent) {
                    Destroy(child.gameObject);
                }
            } else {
                Debug.Log("Item Parent is empty");
            }

            ChestUi.SetActive(false);
        }

        public void Show() {
            ChestUi.SetActive(true);
        }

        public void Hide() {
            ChestUi.SetActive(false);
        }

        public void UpdateUI(LootBox_Base chest) {
            // First, clear any existing slots
            foreach(Transform child in itemParent) {
                Destroy(child.gameObject);
            }

            // Instantiate a new slot for each item in the chest
            for(int i = 0; i < chest.ItemsRewards.Count; i++) {
                GameObject newSlot = Instantiate(itemSlotPrefab, itemParent);
                ChestSlot slotComponent = newSlot.GetComponent<ChestSlot>();

                // Add the item to the newly instantiated slot
                slotComponent.AddItem(chest.ItemsRewards[i]);
            }
        }
    }
}
