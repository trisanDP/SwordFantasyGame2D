 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{

    ItemClass item;
    public Image icon;
    InventoryManager inventory;

    private void Start() {
        inventory = InventoryManager.instance;
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
        if (inventory != null) {
            inventory.AddItem(item);
            Chest.activeLootBox.RemoveChestItem(item);
        }
    }
    #endregion
}
