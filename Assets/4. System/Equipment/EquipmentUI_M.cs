
using OriginL.Item;
using OriginL.Player;
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
        public TextMeshProUGUI Energy;

        private PlayerStat stats;
        private void Awake() {
            slot = equipParent.GetComponentsInChildren<EquipmentSlot>();

        }

        void Start() {
            Initialize();
            equipmentUiObj.SetActive(false);
            if(stat != null) {
                UpdateEnergy();
            } else {
                Debug.LogError("PlayerStat component is missing or not assigned.");
                if(PlayerScript.Instance.playerStat != null) {
                    Debug.Log("NUll");
                }
            }
            stats = PlayerScript.Instance.playerStat;
            UpdateStats();
            UpdateHealth();
            
        }

        private void OnEnable() {
            PlayerEquipmentManager.onEquipmentChanged += UpdateValues;
            PlayerStat.OnHealthChange += UpdateHealth;
        }

        private void Initialize() {
            if(GameManager.Instance.isGameOver == false) {
                manager = PlayerEquipmentManager.Instance;/*PlayerScript.Instance.playerEquipmentM*/;
                stat = PlayerScript.Instance.playerStat;
            }
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

        public void UpdateStats() {
            if(!GameManager.Instance.isGameOver) {
                damageTxt.text = "" + stats.physicalDamage.GetValue();
                ArmorTxt.text = "" + stats.armor.GetValue();
                ResistanceTxt.text = "" + stats.resistance.GetValue();

            }
        }

        public void UpdateValues(BaseItemSO item, BaseItemSO item2) {
            UpdateStats();
            UpdateEnergy();
            UpdateHealth();
            
        }
        public void UpdateHealth() {
            this.health.text = "" + stats.CurrentHealth;
        }


/*        public void SetValues(int phyDmg, int armor, int resistance, int health) {
            PlayerStat stats = PlayerScript.Instance.playerStat;

            if(!GameManager.Instance.isGameOver) {
                damageTxt.text = "" + phyDmg;
                ArmorTxt.text = "" + armor;
                ResistanceTxt.text = "" + resistance;
                this.health.text = "" + health;
                SetEnergy();
            }
        }*/

        void UpdateEnergy() {
            Energy.text = stat.Energy.GetValue().ToString();
        }
    }
}