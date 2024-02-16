using UnityEngine;
using UnityEngine.UI;
using static PlayerEquipmentManager;

public class EquipmentSlot : MonoBehaviour
{
    Equipment item;
    public Image icon;
    PlayerEquipmentManager manager;

    private void Start() {
        manager = GameManager.Instance.playerObj.GetComponent<PlayerEquipmentManager>();
    }

    public void AddItem(Equipment newItem) {
        item = newItem;
        if (item == null)
            Debug.Log("1");
        icon.sprite = item.itemIcon;
        icon.enabled = true;
    }

    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    #region Button
    public void OnButtonUnequip() {
        manager.UnEquip(item);

    }

    #endregion
}
