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
        inventory = FindObjectOfType<InventoryManager>();
        slot = itemParent.GetComponentsInChildren<InventorySlot>();
        inventory.itemChangeCallBack += UpdateUI;
        InventoryUI.SetActive(false);

    }
    private void OnEnable() {
        
    }
    private void OnDestroy() {
        inventory.itemChangeCallBack -= UpdateUI;
    }

    void UpdateUI()
    {
        for(int i = 0; i < slot.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slot[i].AddItem(inventory.items[i]);
                Show();
            } else
            {
                slot[i].ClearSlot();
            }
        }
    }

    public void Show() {
        InventoryUI.SetActive(true);
    }
    public void Hide() {
        InventoryUI.SetActive(false);
    }
}
