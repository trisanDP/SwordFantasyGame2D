using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL
{
    public class PlayerJumpState : PlayerState {


        [Header("Jump & Gravity ")]
        [SerializeField] float jumpForce;
        [SerializeField] int fallMultiplier = 15;
        [Range(0, 2)][SerializeField] float jmpMoveSpeed;

        public PlayerJumpState(PlayerScript player, PlayerStateMachine StateMachine) : base(player, StateMachine) {
        }

        public override void EnterState() {
            base.EnterState();
            if(player.playerCollider.GroundCheck()) {
                Jump();
            }
        }

        public override void ExitState() {
            base.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
           

        }

        public override void PhysicUpdate() {
            base.PhysicUpdate();
            #region Jump_Fall
            // Apply extra gravity to make the player fall faster after reaching the peak of the jump
            if(player.Rb.velocity.y < 0f) {
                player.Rb.velocity += Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime * Vector2.up;
            }

            if(player.playerCollider.GroundCheck() == false) {
                player.Rb.AddForce(jmpMoveSpeed * Vector2.right, ForceMode2D.Force);
                Debug.Log(" Check ");
            }
            #endregion


        }

        protected override void CheckStateChange() {
            base.CheckStateChange();
            if(player.playerInput.jumpPressed != true) {
                player.ActiveState = PlayerScript.State.idel_State;  // Ideal
            }


        }


        void Jump() {
            player.Rb.velocity = new Vector2(player.Rb.velocity.x, jumpForce);
        }

        internal void ShortJump() {
            player.Rb.velocity = new Vector2(player.Rb.velocity.x, -fallMultiplier);
        }

    }
}
