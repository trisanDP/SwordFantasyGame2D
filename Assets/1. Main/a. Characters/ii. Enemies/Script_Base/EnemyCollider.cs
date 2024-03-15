using OriginL.Building;
using Unity.VisualScripting;
using UnityEngine;

namespace OriginL.EnemySpace {

    public class EnemyCollider :MonoBehaviour  {

    

        #region Variable


        [Header("Components")]
        [SerializeField] GameObject wallDetectPoint;
        [SerializeField] GameObject hitPos;
        [SerializeField] LayerMask hitLayer;
        [SerializeField] LayerMask wallLayer;
        LayerMask playerLayer;


        [Header("Vectors")]
        [SerializeField] Vector3 size;

        [Header("Internal")]
        internal Enemy enemy;



        [SerializeField] float HitSize;



        [Header("Range")]
        [SerializeField] float attackRange = 4;
        [SerializeField] float detectRange = 10;
        #endregion


        private void Awake() {
            if(wallDetectPoint == null) {
                wallDetectPoint = transform.Find("WallDetect").gameObject;
                Debug.LogWarning("Wall detect was empty, So assigned using Script");
            }if(hitPos == null) {
                hitPos = transform.Find("HitPos").gameObject;
                Debug.LogWarning("HitPos was empty, So assigned using Script");
            }
            if(hitLayer == 0 || wallLayer == 0) {
                Debug.LogError("EnemyCollider/LayerMask not set");
            }
            playerLayer = LayerMask.GetMask("Player");
            enemy = GetComponent<Enemy>();
        }

        #region Collision

        private void OnCollisionStay(Collision collision) {
            if(collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform")) {
                enemy.isGrounded = true;
            }
        }

        #endregion


        internal Collider2D HasHitWall() {
            Collider2D hit = Physics2D.OverlapBox(wallDetectPoint.transform.position, size, 0, wallLayer);
            if(hit != null && hit.gameObject.GetComponent<BuildingBase>().activeStage != 0) {
                enemy.AggroTo(hit.gameObject);
                return hit; 
            }else 
                return null;
        }



        internal Collider2D[] HitRange() {  // returns damageables object collider
            Collider2D[] hit = Physics2D.OverlapCircleAll(hitPos.transform.position, HitSize, hitLayer);
            return hit;
        }

        internal bool InDetectRange() {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, GetDetectRange(), playerLayer);
            if(hit != null && !enemy.hasAggroed) {
                enemy.AggroTo(hit.gameObject);
            }
            return hit;
        }


        internal float GetDetectRange() {
            if(enemy.hasAggroed)
                return detectRange * 1.5f;
            else
                return detectRange;
        }

        internal float GetAttackRange() {
            return attackRange;
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
            if(enemy != null) {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, GetDetectRange());

            }
            //Attack Range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        #endregion
    }
}