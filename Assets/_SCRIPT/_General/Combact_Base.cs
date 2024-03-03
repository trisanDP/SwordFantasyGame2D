using System.Collections;
using UnityEngine;

public class Combact_Base : MonoBehaviour
{
    internal GameObject target;
    public bool isAttacking = false;

    [System.Serializable]
    public class Attack {
        // add animation type here and everything related to attack here so that only 1 function like MeleeAttack1 could control all attack
        public float Rate;
        public bool CanAttack;
        public int knockBackF;

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
    public IEnumerator Attack_Co(Attack attack) {
        attack.CanAttack = false;
        // Perform attack here
        yield return new WaitForSeconds(attack.Rate);
        attack.CanAttack = true;
        isAttacking = false;
    }

}
