using UnityEngine;

namespace OriginL.Player {
    public class PlayerCollider : MonoBehaviour {
        [Header("Parent Script")]
        internal PlayerScript playerScrip;

        #region Variable

        [SerializeField] int knockBack_F;
        [SerializeField] Transform groundCheck;
        [SerializeField] Vector2 size;
        [SerializeField] LayerMask groundLayerMask;


        [Header("Attack Attributes")]
        [SerializeField] float attackRange;
        [SerializeField] float attackRate;
        [SerializeField] GameObject attackPoint;
        [SerializeField] LayerMask hitLayer;



        /*    [SerializeField] internal bool isOnRope = false;*/
        #endregion


        void Start() {
            if(hitLayer == 0 || groundLayerMask == 0) {
                Debug.LogError("PlayerCollider/LayerMask not set");
            }
            playerScrip = GetComponent<PlayerScript>();
        }

        #region Ground
        internal bool GroundCheck() {

            RaycastHit2D raycasthit = Physics2D.BoxCast(groundCheck.position, size, 0, Vector2.down, 0, groundLayerMask);
            if(raycasthit.collider != null) {
                return true;
            } else {

                return false;
            }
        }
        #endregion

        #region Collider

        private void OnCollisionEnter2D(Collision2D collision) {
            #region Enemy
            if(collision.gameObject.CompareTag("Enemy")) {
                KnockBack(collision.gameObject);

            }
            #endregion
        }



        #endregion

        #region onTrigger
        /*
            private void OnTriggerEnter2D(Collider2D collision)
            {
                #region Quest

                #endregion
            }*/
        #endregion

        #region EnemyDetect
        internal Collider2D[] HitColRange() {
            Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, hitLayer);
            return hit;
        }

        #endregion

        #region Special 
        void KnockBack(GameObject col) {
            Vector2 direction = (transform.position - col.transform.position).normalized;
            playerScrip.Rb.AddForce(knockBack_F * direction, ForceMode2D.Impulse);
        }

        #endregion


        #region Gizmos
        private void OnDrawGizmos() {
            //GroundCheck
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(groundCheck.position, size);

            //Attack Range
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
        }
        #endregion

    }
}