using UnityEngine;


namespace OriginL.EnemySpace {
    public class EnemyController {

        internal Enemy enemyScrip;

        #region Variables
        [Header("Values")]
        [SerializeField] internal float activeSpeed;

        public EnemyController(Enemy enemyScrip,float activeSpeed) {
            this.enemyScrip = enemyScrip;
            this.activeSpeed = activeSpeed;
        }


        #endregion

        #region Petrol Movement
/*        internal virtual void StartPetrolMovement() {
            // Petrol Movement
            if(enemyScrip.enemyAnimCont._facingRight) {
                enemyScrip.rb.AddForce(Vector2.right * activeSpeed, ForceMode2D.Force);
            } else {
                enemyScrip.rb.AddForce(Vector2.left * activeSpeed, ForceMode2D.Force);
            }

        }*/
        #endregion

        internal void MoveToObj(GameObject obj, float activeSpeed) {
            enemyScrip.enemyAI.LookAt(obj);
            enemyScrip.rb.AddForce(((obj.transform.position) - enemyScrip.transform.position).normalized * activeSpeed, ForceMode2D.Force);
        }

        internal void EnemyMoveDirectionCont(Vector2 direction, float activeSpeed) {
            enemyScrip.rb.AddForce(direction * activeSpeed, ForceMode2D.Force);

        }

        internal void StopEnemyMovement() {
            if(enemyScrip.rb.linearVelocity.x != 0) {
                enemyScrip.rb.linearVelocity = new Vector2(0, enemyScrip.rb.linearVelocity.y);
                return;
            }
        }

    }
}