using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    ItemClass item;
    public Image icon;
    public Button removeButton;

    public void AddItem(ItemClass newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
    #region Button
    public void OnRemovingItem()
    {
        InventoryManager.instance.RemoveInventoryItem(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
    #endregion
}
