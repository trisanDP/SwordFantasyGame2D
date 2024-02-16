using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_M : MonoBehaviour
{
    public Transform itemParent;
    public GameObject InventoryUI;
    InventoryManager inventory;
    

    InventorySlot[] slot;

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<InventoryManager>();
        slot = itemParent.GetComponentsInChildren<InventorySlot>();
        inventory.itemChangeCallBack += UpdateUI;
        InventoryUI.SetActive(false);

    }

    private void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.I)) {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
        }*/
    }

    void UpdateUI()
    {
        InventoryUI.SetActive(true);
        for(int i = 0; i < slot.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slot[i].AddItem(inventory.items[i]);
            } else
            {
                slot[i].ClearSlot();
            }
        }
    }
}
