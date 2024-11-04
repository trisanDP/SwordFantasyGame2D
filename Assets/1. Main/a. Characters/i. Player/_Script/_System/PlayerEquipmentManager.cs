using System;
using UnityEngine;
using OriginL.Item;

namespace OriginL {
    public class PlayerEquipmentManager : MonoBehaviour {
        InventoryManager inventoryManager;

        public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
        public OnEquipmentChanged onEquipmentChanged;

        public Equipment[] currentEquipment;

        public EquipmentUI_M equipmentUI;
        public static PlayerEquipmentManager Instance;

        private void Awake() {
            if(Instance != null) {
                Destroy(Instance);
            } else {
                Instance = this;
            }
        }

        void Start() {
            inventoryManager = GetComponent<InventoryManager>();
            if(UiManager.Instance != null) {
                equipmentUI = UiManager.Instance.equipmentUI;
            } else
                Debug.LogWarning("UIManager is Missing");

            int numSlots = System.Enum.GetNames(typeof(EquipmentSlotType)).Length;

            currentEquipment = new Equipment[numSlots];
        }

        #region Basic Equipment Function

        public void Equip(Equipment newItem) {
            int slotIndex = (int)newItem.equipment_Slot;
            Equipment oldItem = null;
            if(currentEquipment[slotIndex] != null) {
                oldItem = currentEquipment[slotIndex];
                inventoryManager.AddItem(oldItem);
            }

            currentEquipment[slotIndex] = newItem;
            onEquipmentChanged?.Invoke(newItem, oldItem);
            equipmentUI.UpdateUI(slotIndex);

        }
        public void UnEquip(Equipment Item) {  // For Button in  Equipment Slot
            int slotIndex = Array.IndexOf(currentEquipment, Item);

            if(slotIndex >= 0) {
                Equipment oldItem = currentEquipment[slotIndex];
                inventoryManager.AddItem(oldItem);

                currentEquipment[slotIndex] = null;
                onEquipmentChanged?.Invoke(null, oldItem);
                equipmentUI.UpdateUI2(slotIndex);
            }
        }



        public void UnEquipAll() {
            // Create a copy of the currentEquipment array to iterate over
            Equipment[] currentEquipmentCopy = (Equipment[])currentEquipment.Clone();

            for(int i = 0; i < currentEquipmentCopy.Length; i++) {
                if(currentEquipmentCopy[i] != null) {
                    UnEquip(currentEquipmentCopy[i]);
                }
            }
        }
        #endregion




    }
}