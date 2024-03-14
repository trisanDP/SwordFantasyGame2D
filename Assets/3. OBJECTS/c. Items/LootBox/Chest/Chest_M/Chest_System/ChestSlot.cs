using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using OriginL.Item;

namespace OriginL.ChestSpace{
    public class ChestSlot : MonoBehaviour {
        ItemClass item;
        public Image icon;
        /*    InventoryManager inventory;*/
        /*    public static event Action<ItemClass> OnItemAddedToInventory;*/
        public UnityEvent<ItemClass> OnItemPickedUp;

        private void Start() {
            /*        inventory = InventoryManager.instance;*/
        }

        public void AddItem(ItemClass newItem) { // Will add the Item and item UI in chest
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
        }

        public void ClearSlot() {  // Will Clear the Ui in chest
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        #region Button
        public void AddToInventory() {  // Linked to Button in ChestSlot prefab
            OnItemPickedUp?.Invoke(item);
            Chest.activeLootBox.RemoveChestItem(item);
        }
        #endregion
    }
}