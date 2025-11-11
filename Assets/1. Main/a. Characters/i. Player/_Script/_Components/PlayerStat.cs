using OriginL.Item;
using System;
using System.Collections;
using UnityEngine;

namespace OriginL.Player {
    public class PlayerStat : CharacterStat {

        PlayerScript playerScrip;

        public float stundDuration;

        public Stat Energy;

        public static Action OnHealthChange;

        private void Start() {
            playerScrip = GetComponent<PlayerScript>();
            PlayerEquipmentManager.onEquipmentChanged += OnEquipmentChanged;
            Energy.SetBaseValue(10);
        }

        public override void TakeDamage(float damage, int knockBack, GameObject damageFrom) {
            base.TakeDamage(damage, knockBack, damageFrom);
            OnHealthChange?.Invoke();
        }

        public override void Die() {
            base.Die();
            playerScrip.isDead = true;
            gameObject.SetActive(false);
            GameManager.Instance.HandleGameOver();
        }

        private void OnEquipmentChanged(Equipment newItem, Equipment oldItem) {
            Debug.Log("Added?");
            if(newItem != null) {
                armor.AddModifier(newItem.armorModifier);
                physicalDamage.AddModifier(newItem.damageModifier);
                resistance.AddModifier(newItem.resistanceModifier);
            }
            if(oldItem != null) {
            Debug.Log("Added?2222");
                armor.RemoveModifier(oldItem.armorModifier);
                physicalDamage.RemoveModifier(oldItem.damageModifier);
                resistance.RemoveModifier(oldItem.resistanceModifier);
            }
        }



        #region Effect

        internal IEnumerator Stund() {
            playerScrip.playerController.canMove = false; //make movement manager as base
            yield return new WaitForSeconds(stundDuration);
            playerScrip.playerController.canMove = true;
        }
        #endregion
    }
}