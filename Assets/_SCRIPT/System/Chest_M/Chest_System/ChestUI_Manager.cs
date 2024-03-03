using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI_Manager : MonoBehaviour
{
    public Transform itemParent;
    public GameObject ChestUi;

    ChestSlot[] slot;
    

    void Start() {
        if(itemParent != null)
            slot = itemParent.GetComponentsInChildren<ChestSlot>();
        else
            Debug.Log("Item Parent is empty");
        ChestUi.SetActive(false);
    }

    public void Show() {
        ChestUi.SetActive(true);
    }
    public void Hide() {
        ChestUi.SetActive(false);
    }

    public void UpdateUI(LootBox_Base chest) {
        for (int i = 0; i < slot.Length; i++) {
            if (i < chest.ItemsRewards.Count) {
                slot[i].AddItem(chest.ItemsRewards[i]);
            } else {
                slot[i].ClearSlot();
            }
        }
    }

   
}
