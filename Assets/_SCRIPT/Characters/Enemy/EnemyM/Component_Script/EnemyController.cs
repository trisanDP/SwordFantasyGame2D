using UnityEngine;


namespace OriginL.EnemySpace {
    public class EnemyController : MonoBehaviour {

        internal Enemy enemyScrip;

        #region Variables
        [Header("Values")]
        [SerializeField] internal float ChasingSpeed;
        [SerializeField] internal float activeSpeed;
        [SerializeField] internal float petrolSpeed;


        #endregion

        private void Start() {
            enemyScrip = GetComponent<Enemy>();
            petrolSpeed = Random.Range(petrolSpeed * 1f, petrolSpeed);
            ChasingSpeed = Random.Range(ChasingSpeed * 0.85f, ChasingSpeed);
            activeSpeed = petrolSpeed;
        }

        internal virtual void StartPetrolMovement() {
            activeSpeed = petrolSpeed;
            // Petrol Movement
            if(enemyScrip.enemyAnimCont._facingRight) {
                enemyScrip.rb.AddForce(Vector2.right * activeSpeed, ForceMode2D.Force);
            } else {
                enemyScrip.rb.AddForce(Vector2.left * activeSpeed, ForceMode2D.Force);
            }

        }
        internal void MoveToObj(GameObject obj, float activeSpeed) {
            enemyScrip.enemyAI.LookAt(obj);
            enemyScrip.rb.AddForce(((obj.transform.position) - enemyScrip.transform.position).normalized * activeSpeed, ForceMode2D.Force);

        }

        internal void EnemyMoveDirectionCont(Vector2 direction, float activeSpeed) {
            enemyScrip.rb.AddForce(direction * activeSpeed, ForceMode2D.Force);

        }

        internal void StopEnemyMovement() {
            if(enemyScrip.rb.velocity.x != 0) {
                enemyScrip.rb.velocity = new Vector2(0, enemyScrip.rb.velocity.y);
                return;
            }
        }

    }
}