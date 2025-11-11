using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using OriginL.Item;
using System;

namespace OriginL.ChestSpace {
    public class ChestSlot : MonoBehaviour {
        BaseItemSO item;                 
        public Image icon;                  
        InventoryManager inventory;          
        //public static event Action<BaseItemSO> OnItemAddedToInventory; 
       // public UnityEvent<BaseItemSO> OnItemPickedUp; 

        public Button btn;

        private void Start() {
            inventory = InventoryManager.instance;
            //btn.onClick.AddListener(AddToInventory);
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
            //OnItemPickedUp?.Invoke(item);
            inventory.AddItem(item);
            Chest.activeLootBox.RemoveChestItem(item);

        }
        #endregion
    }
}
