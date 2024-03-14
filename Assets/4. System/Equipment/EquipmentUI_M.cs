using TMPro;
using UnityEngine;

namespace OriginL {
    public class EquipmentUI_M : MonoBehaviour {
        public Transform equipParent;
        public GameObject equipmentUiObj;
        OriginL.Player.PlayerStat stat;

        private PlayerEquipmentManager manager;

        EquipmentSlot[] slot;

        public TextMeshProUGUI damageTxt;
        public TextMeshProUGUI ArmorTxt;
        public TextMeshProUGUI ResistanceTxt;
        public TextMeshProUGUI health;

        private void Awake() {
            slot = equipParent.GetComponentsInChildren<EquipmentSlot>();
            if(GameManager.Instance.playerObj != null && GameManager.Instance.isGameOver == false) {
                manager = GameManager.Instance.playerObj.GetComponent<PlayerEquipmentManager>();
                stat = GameManager.Instance.playerObj.GetComponent<OriginL.Player.PlayerStat>();
            }
        }

        void Start() {
            equipmentUiObj.SetActive(false);
        }

        public void ToggleUi() {
            equipmentUiObj.SetActive(!equipmentUiObj.activeSelf);
        }

        public void UpdateUI(int index) {
            equipmentUiObj.SetActive(true);
            for(int i = 0; i < slot.Length; i++) {
                if(i < slot.Length) {
                    slot[index].AddItem(manager.currentEquipment[index]);
                } else {
                    slot[i].ClearSlot();
                }
            }
        }

        public void UpdateUI2(int index) {
            slot[index].ClearSlot();
        }

        public void SetValues(int phyDmg, int armor, int resistance, int health) {
            if(!GameManager.Instance.isGameOver) {
                damageTxt.text = "" + phyDmg;
                ArmorTxt.text = "" + armor;
                ResistanceTxt.text = "" + resistance;
                this.health.text = "" + health;
            }
        }
        /*
            public void Update() {
                if(!GameManager.Instance.isGameOver) {
                    damageTxt.text = "" + stat.physicalDamage.GetValue();
                    ArmorTxt.text = "" + stat.armor.GetValue();
                    ResistanceTxt.text = "" + stat.resistance.GetValue();
                    health.text = "" + stat.CurrentHealth;
                }
            }*/
    }
}