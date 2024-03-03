using UnityEngine;

namespace OriginL.EnemySpace {

    public class EnemyCollider : MonoBehaviour {

        [Header("Internal")]
        internal Enemy enemyScript;



        [SerializeField] float HitSize;



        [Header("Range")]
        [SerializeField] internal float attackRange = 4;
        [SerializeField] private float detectRange { get; set; }

        #region Variable


        [Header("Components")]
        [SerializeField] internal GameObject wallDetectPoint;
        [SerializeField] GameObject hitPos;
        [SerializeField] LayerMask hitLayer;
        [SerializeField] LayerMask platformLayor;
        [SerializeField] LayerMask playerLayer;


        [Header("Vectors")]
        [SerializeField] Vector3 size;



        #endregion

        private void Start() {
            detectRange = 10;
            attackRange = 4;
            if(hitLayer == 0 || platformLayor == 0) {
                Debug.LogError("EnemyCollider/LayerMask not set");
            }

            enemyScript = GetComponent<Enemy>();
        }

        private void OnEnable() {

        }

        #region Collision

        private void OnCollisionStay(Collision collision) {
            if(collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform")) {
                enemyScript.isGrounded = true;
            }
        }

        #endregion

        internal bool HasHitWall() {
            if(Physics2D.OverlapBox(wallDetectPoint.transform.position, size, 0, platformLayor)) {
                return true;
            } else
                return false;
        }



        internal Collider2D[] HitRange() {  // returns damageables object collider
            Collider2D[] hit = Physics2D.OverlapCircleAll(hitPos.transform.position, HitSize, hitLayer);
            return hit;
        }

        internal bool InDetectRange() {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, GetDetectRange(), playerLayer);
            if(hit != null && !enemyScript.hasAggroed) {
                enemyScript.AggroTo(hit.gameObject);
            }
            return hit;
        }



        #region Extra And Gizmos
        private void OnDrawGizmos() {
            //Detection Box
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(wallDetectPoint.transform.position, size);


            //Attack Box
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(hitPos.transform.position, HitSize);


            //Detect Range
            if(enemyScript != null) {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, GetDetectRange());

            }
            //Attack Range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        internal float GetDetectRange() {
            if(enemyScript.hasAggroed)
                return detectRange * 1.5f;
            else
                return detectRange;
        }
        #endregion
    }
}