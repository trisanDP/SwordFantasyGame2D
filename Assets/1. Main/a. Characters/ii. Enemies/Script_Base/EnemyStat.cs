using System.Collections;
using UnityEngine;

namespace OriginL.EnemySpace {
    public class EnemyStat : CharacterStat {
        internal Enemy enemy;
        #region Variables

        [Header("Status")]
        [SerializeField] internal float stundDuration;

        [Header("Drops")]
        [SerializeField] GameObject dropBox;
/*        [SerializeField] List<BaseItemSO> drops;*/

        #endregion
        private void Start() {
            enemy = GetComponent<Enemy>();
        }

        public override void TakeDamage(float damage, int knockBack, GameObject damageFrom) {
            base.TakeDamage(damage, knockBack, damageFrom);
            enemy.enemyAnimCont.DamageTaken();
            if(!enemy.hasAggroed) {   // Modify Enemy.Combact.SetAggro(Target);
                enemy.AggroTo(damageFrom);
            }
        }

        public override void Die() {
            base.Die();
            enemy.enemyAnimCont.PlayDeathAnim();
        }

        #region Temp
      /*  public void DropItems() {  // Called in EnemyAnimation Script
            dropBox = Instantiate(dropBox, transform.position, Quaternion.identity);
            if(dropBox != null) {
                foreach(BaseItemSO item in drops) { // Loop through all items in Drops
                    LootDrop itemDropsScript = dropBox.GetComponent<LootDrop>();
                    if(itemDropsScript != null) {
                        itemDropsScript.ItemsRewards.Add(item);
                    } else
                        Debug.Log("LootDrop Component in Dropbox in enemy Is Empty");
                }
            } else
                Debug.Log("DropBox Prefab Empty");
        }
*/
        #endregion

        #region Effect

        #region 1. KnockBack

        public override void KnockBack(int force, GameObject target) {
            base.KnockBack(force, target);
        }

        // In Base

        #endregion

        #region 2.Stund
        // Stund
        public void StartStundEffect() {
            StartCoroutine(Stund());
        }

        internal IEnumerator Stund() {
            enemy.enemyController.StopEnemyMovement();  //make movement manager as base
            yield return new WaitForSeconds(stundDuration);
            enemy.enemyController.activeSpeed = enemy.chasingSpeed;
        }


        #endregion

        #endregion
    }
}