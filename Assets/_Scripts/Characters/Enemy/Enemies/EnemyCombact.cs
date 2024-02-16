using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Combact_Base;

public class EnemyCombact : Combact_Base
{
    EnemyScript enemyScrip;
    private void Start() {
        enemyScrip = GetComponent<EnemyScript>();
    }

    internal void MeleeAttack1() {
        if (Attack1.CanAttack) {
            isAttacking = true;
            Attack1.CanAttack = false;
            enemyScrip.enemyAnimCont.PlayAttackAnim("Attack1");
            StartCoroutine(Attack_Co(Attack1));
        }
    }
    internal void MeleeAttack2() {
        if (Attack2.CanAttack) {
            Attack2.CanAttack = false;
            enemyScrip.enemyAnimCont.PlayAttackAnim("Attack2");
            StartCoroutine(Attack_Co(Attack2));
        }
    }


    public void CallAttack1() {  // Attack Function is called within animation frame     
        foreach (Collider2D hit in enemyScrip.enemyCollider.ColInRange()) {
            if (hit.TryGetComponent<IDamageable>(out var damageable)) {
                /*playerScrip.target = hit.gameObject;*/
                damageable.TakeDamage(Attack1.TPhysicalDamage(this.gameObject), Attack1.knockBackF,this.gameObject);
            }
        }
    }

    internal void CallAttack2() {
        foreach (Collider2D hit in enemyScrip.enemyCollider.ColInRange()) {
            if (hit.TryGetComponent<IDamageable>(out var damageable)) {
                damageable.TakeDamage(Attack2.TMagicalDamage(), Attack2.knockBackF,this.gameObject);
            }
        }
    }

}
