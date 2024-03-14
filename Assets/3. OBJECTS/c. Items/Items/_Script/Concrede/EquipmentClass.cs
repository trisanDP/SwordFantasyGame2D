using UnityEngine;

namespace OriginL.Item {
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Item/Equipment")]
    public class Equipment : ItemClass {
        public EquipmentSlotType equipment_Slot;
        public int armorModifier;
        public int damageModifier;
        public int resistanceModifier;

        public override void Use() {
            base.Use();
            GameManager.Instance.playerObj.GetComponent<PlayerEquipmentManager>().Equip(this);
            RemoveFromInventory();

        }
    }

    public enum EquipmentSlotType {
        Head, Chest, Hand, Shield, Weapon, Leg, Foot
    };
}