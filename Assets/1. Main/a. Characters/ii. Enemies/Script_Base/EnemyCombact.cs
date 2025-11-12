using UnityEngine;

namespace OriginL.EnemySpace {
    public class EnemyCombact : Combat_Base {
        Enemy enemyScrip;
        private void Start() {
            enemyScrip = GetComponent<Enemy>();
        }
        public Attack Attack1 = new() { Rate = 1.0f, CanAttack = true, baseDamage = 10, knockBackF = 1 };
        public Attack Attack2 = new() { Rate = 1.0f, CanAttack = true, baseDamage = 20, knockBackF = 2 };




        internal void MeleeAttack1() {
            if(Attack1.CanAttack && isAttacking == false) {
                isAttacking = true;
                Attack1.CanAttack = false;
                enemyScrip.enemyAnimCont.PlayAttackAnim("Attack1");
                StartCoroutine(Attack_Co(Attack1));
            }
        }
        internal void MeleeAttack2() {
            if(Attack2.CanAttack && !isAttacking) {
                Attack2.CanAttack = false;
                enemyScrip.enemyAnimCont.PlayAttackAnim("Attack2");
                StartCoroutine(Attack_Co(Attack2));
            }
        }


        public void CallAttack1() {  // Attack Function is called within animation frame     
            foreach(Collider2D hit in enemyScrip.enemyCollider.HitRange()) {
                if(hit.TryGetComponent<IDamageable>(out var damageable)) {
                    /*playerScrip.target = hit.gameObject;*/
                    damageable.TakeDamage(Attack1.TPhysicalDamage(this.gameObject), Attack1.knockBackF, this.gameObject);
                }
            }
        }

        internal void CallAttack2() {
            foreach(Collider2D hit in enemyScrip.enemyCollider.HitRange()) {
                if(hit.TryGetComponent<IDamageable>(out var damageable)) {
                    damageable.TakeDamage(Attack2.TMagicalDamage(), Attack2.knockBackF, this.gameObject);
                }
            }
        }

    }
}