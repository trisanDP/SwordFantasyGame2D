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
        [SerializeField] LayerMask detectLayer;

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
            if(detectLayer == 0) {
                detectLayer = LayerMask.GetMask("Player");
                Debug.LogWarning("DetectLayer Empty");
            }
            
            enemy = GetComponent<Enemy>();
        }

        #region Collision

        private void OnCollisionStay(Collision collision) {
            if(collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform")) {
                enemy.isGrounded = true;
            }
        }

        #endregion

        internal Collider2D HasHitWall() { // returns WALL If Has Hit Wall
            Collider2D hit = Physics2D.OverlapBox(wallDetectPoint.transform.position, size, 0, wallLayer);
            if(hit != null ) {
                BuildingBase building = hit.gameObject.GetComponent<BuildingBase>();
                if(building.activeStage != 0) {
                    enemy.AggroTo(hit.gameObject);
                    return hit;
                }
            }
            return null;
        }

        internal bool InDetectRange() { // Returns Player If Hasnot Hit Wall
            Collider2D hit = Physics2D.OverlapCircle(transform.position, GetDetectRange(), detectLayer);
            if(hit != null && !HasHitWall()) {
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


        internal Collider2D[] HitRange() {  // returns damageables object collider
            Collider2D[] hit = Physics2D.OverlapCircleAll(hitPos.transform.position, HitSize, hitLayer);
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