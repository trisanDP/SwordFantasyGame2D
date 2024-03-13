using OriginL.System;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IIntractable
{
    InventoryManager inventory;
    public ItemClass item;
    private SpriteRenderer spriteRenderer;


    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (item != null && spriteRenderer != null){
            spriteRenderer.sprite = item.itemIcon;
        }
        spriteRenderer.sortingLayerName = "ForGround";
    }

    private void Start()
    {
        if(inventory == null) {
            inventory = InventoryManager.instance;
        }

    }

    public void OnIntract(){

        bool wasPickedUp = inventory.AddItem(item);
        if (wasPickedUp)
        {
/*            Debug.Log("Picking Up " + item.name);*/
            Destroy(gameObject);
        }
    }

    public string Message() {
        return item.message;
    }

    public GameObject GetGameObject() {
        return gameObject;
    }
}
