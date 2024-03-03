using UnityEngine;

public class ItemPickup : Intractable
{

    public ItemClass item;
    private SpriteRenderer spriteRenderer;

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (item != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = item.itemIcon;
        }
        spriteRenderer.sortingLayerName = "ForGround";
    }

    private void Start()
    {
/*        if(inventory == null)
        {
            inventory = InventoryManager.instance;
        }*/

    }

    public override void OnEntract()
    {

        bool wasPickedUp = inventory.AddItem(item);
        if (wasPickedUp)
        {
/*            Debug.Log("Picking Up " + item.name);*/
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {

    }


}
