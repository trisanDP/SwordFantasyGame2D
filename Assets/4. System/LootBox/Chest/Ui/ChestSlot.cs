using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using OriginL.Item;
using System;

namespace OriginL.ChestSpace {
    public class ChestSlot : MonoBehaviour {
        BaseItemSO item;                    // The item that this slot represents
        public Image icon;                   // The UI image for the item icon
        InventoryManager inventory;          // Reference to the InventoryManager
        public static event Action<BaseItemSO> OnItemAddedToInventory; // Event for adding item to inventory
        public UnityEvent<BaseItemSO> OnItemPickedUp; // Unity event when item is picked up

        public Button btn;

        private void Start() {
            inventory = InventoryManager.instance;
            btn.onClick.AddListener(AddToInventory);
        }

        public void AddItem(BaseItemSO newItem) // Will add the Item and item UI in chest
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
        }

        public void ClearSlot() // Will Clear the UI in chest
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        #region Button
        public void AddToInventory() // Linked to Button in ChestSlot prefab
        {            
            Debug.Log("Item Added to Inventory");
            OnItemPickedUp?.Invoke(item);
            inventory.AddItem(item);
            Chest.activeLootBox.RemoveChestItem(item);

        }
        #endregion
    }
}
