
using UnityEngine;

namespace OriginL.Player {
    public class PlayerAnimationController : MonoBehaviour {
        PlayerScript playerScrip;

        internal bool _facingRight;
        [SerializeField] internal Animator animator_Player;

        // Start is called before the first frame update
        void Start() {
            playerScrip = GetComponent<PlayerScript>();
            _facingRight = true;
            animator_Player = GetComponentInChildren<Animator>();
        }


        private void Update() {
            if(playerScrip.playerInput._moveInput.x > 0 && !_facingRight) {
                Flip();

            }
            if(playerScrip.playerInput._moveInput.x < 0 && _facingRight) {
                Flip();
            }

            animator_Player.SetBool("IsGrounded", playerScrip.playerCollider.GroundCheck());
            animator_Player.SetFloat("yVelocity", playerScrip.Rb.velocity.y);
            animator_Player.SetBool("jumpPressed", playerScrip.playerInput.JumpAction());
            animator_Player.SetInteger("Speed", (int)playerScrip.playerInput._moveInput.x);

        }

        internal void Flip() {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            _facingRight = !_facingRight;
        }

        internal void PlayAttackAnim(string type) {
            animator_Player.SetTrigger(type);       // Attack Function is called within animation frame
        }

        internal void PlayIdelAnim(bool isIdel) {
            animator_Player.SetBool("IsIdel", isIdel);
        }
        /*
            internal void JumpPressedAnim(bool var) {
                animator_Player.SetBool("jumpPressed", var);
                animator_Player.SetBool("CanJump", var);
            }*/
    }
}