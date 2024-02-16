using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyStat : CharacterStat
{
    internal EnemyScript enemyScrip;

    [Header("Status")]
    [SerializeField] internal float stundDuration;

    [Header("Internal")]
    internal bool isStund;

    private void Start() {
        enemyScrip = GetComponent<EnemyScript>();
    }

   

    public override void TakeDamage(float damage, int knockBack, GameObject damageFrom) {
        base.TakeDamage(damage, knockBack, damageFrom);
        if(enemyScrip.Target == null) {
            enemyScrip.Target = damageFrom;
        }
    }




    #region Effect
    public override void KnockBack(int force, GameObject target) {
        base.KnockBack(force, target);
        StartCoroutine(Stund());
        enemyScrip.ActiveState = EnemyScript.State.Stund_State;  //Make a state manager as base
    }

    internal IEnumerator Stund() {
        enemyScrip.enemyController.activeSpeed = 0;  //make movement manager as base
        yield return new WaitForSeconds(stundDuration);
        enemyScrip.enemyController.activeSpeed = enemyScrip.enemyController.ChasingSpeed;
        enemyScrip.ActiveState = EnemyScript.State.Chasing_State;
        isStund = false;
    }

    #endregion

    #region basic
    public override void Die() {
        base.Die();
        enemyScrip.OnDeath();
    }
    #endregion
}
