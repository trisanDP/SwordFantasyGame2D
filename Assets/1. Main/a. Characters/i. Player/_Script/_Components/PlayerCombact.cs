using UnityEngine;

namespace OriginL.Player {

    public class PlayerCombact : Combact_Base {
        PlayerScript playerScrip;

        void Start() {
            playerScrip = GetComponent<PlayerScript>();
        }

        #region MainAttackFuntion

        public Attack Attack1 = new() { Rate = 1.0f, CanAttack = true, baseDamage = 10, knockBackF = 1 };
        public Attack Attack2 = new() { Rate = 1.0f, CanAttack = true, baseDamage = 20, knockBackF = 2 };

        internal void MeleeAttack1() {
            if(Attack1.CanAttack) {
                Attack1.CanAttack = false;
                playerScrip.p_animCont.PlayAttackAnim("Attack1");
                StartCoroutine(Attack_Co(Attack1));
                isAttacking = true;
            }
        }
        internal void MeleeAttack2() {

            if(Attack2.CanAttack) {
                Attack2.CanAttack = false;
                playerScrip.p_animCont.PlayAttackAnim("Attack2");
                StartCoroutine(Attack_Co(Attack2));
                isAttacking = true;
            }
        }
        #endregion




        #region Animation_Attack_Caller

        public void AttackCall() {  // Attack Function is called within animation frame     
            foreach(Collider2D hit in playerScrip.playerCollider.HitColRange()) {
                if(hit.TryGetComponent<IDamageable>(out var damageable)) {
                    /*playerScrip.target = hit.gameObject;*/
                    damageable.TakeDamage(Attack1.TPhysicalDamage(gameObject), Attack1.knockBackF, this.gameObject);
                }
            }
        }

        internal void CallAttack2() {
            foreach(Collider2D hit in playerScrip.playerCollider.HitColRange()) {
                if(hit.TryGetComponent<IDamageable>(out var damageable)) {
                    damageable.TakeDamage(Attack2.TMagicalDamage(), Attack2.knockBackF, this.gameObject);
                }
            }
        }
        #endregion




    }
}