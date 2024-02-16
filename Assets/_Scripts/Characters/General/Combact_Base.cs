using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Combact_Base;

public class Combact_Base : MonoBehaviour
{
    internal GameObject target;
    public bool isAttacking = false;

    [System.Serializable]
    public class Attack {
        public float Rate;
        public bool CanAttack;
        public int knockBackF;
        public string[] statusEffect;

        #region DamageRelated
        [Header("Damages")]
        public int baseDamage;
        public float physicalD_Mod;
        public float magicD_Mod;
        private float finalDamage;
        #endregion

        public float TPhysicalDamage(GameObject obj) {
            physicalD_Mod = obj.GetComponent<CharacterStat>().physicalDamage.GetValue();
            finalDamage =  baseDamage  + physicalD_Mod;
            return finalDamage;
        }

        public float TMagicalDamage() {
            return finalDamage = baseDamage + magicD_Mod;
        }

    }

    public Attack Attack1 = new() { Rate = 1.0f, CanAttack = true, baseDamage = 10 , knockBackF = 1, statusEffect = new string[] { "Bleed" } };
    public Attack Attack2 = new() { Rate = 1.0f, CanAttack = true, baseDamage = 20, knockBackF = 2, statusEffect = new string[] { "Poison", "Bleed", "Fear" } };

    public IEnumerator Attack_Co(Attack attack) {
        attack.CanAttack = false;
        // Perform attack here
        yield return new WaitForSeconds(attack.Rate);
        attack.CanAttack = true;
        isAttacking = false;
    }

}
