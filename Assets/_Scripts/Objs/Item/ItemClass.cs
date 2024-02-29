using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    [Header("Item Detail")]
    new public string name = "New Item";
    public Sprite itemIcon = null;
    public bool isDefault = false;


    public virtual void Use()
    {
/*        Debug.Log("Using " + name);*/
    }

    public void RemoveFromInventory()
    {
        InventoryManager.instance.RemoveInventoryItem(this);
    }
}
