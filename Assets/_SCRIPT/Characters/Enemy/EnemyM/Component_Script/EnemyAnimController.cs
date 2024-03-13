using UnityEngine;

namespace OriginL.EnemySpace {
    public class EnemyAnimController : MonoBehaviour {
        Enemy enemyScrip;

        #region Variables

        [Header("Sprite")]
        [SerializeField] internal bool _facingRight;

        [Header("Animator")]
        Animator anim;

        #endregion

        private void Awake() {
            _facingRight = true;
            anim = GetComponent<Animator>();
            enemyScrip = GetComponent<Enemy>();
        }

        private void Update() {
            anim.SetInteger("Speed", (int)enemyScrip.rb.velocity.x);    
            anim.SetBool("isGrounded", enemyScrip.isGrounded);
        }

        internal void PlayAttackAnim(string type) {
            anim.SetTrigger(type);
        }

        internal void PlayDeathAnim() {
            anim.SetTrigger("Died");
        }

        public void AfterDeathAnim() {
/*            enemyScrip.enemyStatus.DropItems();*/
            Destroy(gameObject);
        }

        public void PlayIdelAnimation() {
            anim.SetBool("IsIdel", true);
        }


        internal void Flip() {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;
            _facingRight = !_facingRight;
        }

        internal void DamageTaken() { // When enemy takes damage
            anim.SetTrigger("DamageTaken");
        }


    }
}