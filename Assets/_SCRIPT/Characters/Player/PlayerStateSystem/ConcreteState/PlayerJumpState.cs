using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace OriginL
{
    public class PlayerJumpState : PlayerState {


        [Header("Jump & Gravity ")]
        [SerializeField] float jumpForce;
        [SerializeField] int fallMultiplier;
        [Range(0, 2)][SerializeField] float jmpMoveSpeed;

        public PlayerJumpState(PlayerScript player, PlayerStateMachine StateMachine) : base(player, StateMachine) {
        }

        public override void EnterState() {
            base.EnterState();
            Debug.Log(" Jump State ");
            jumpForce = player.jumpForce;
            fallMultiplier = player.fallMultiplier;
            jmpMoveSpeed = player.jmpMoveSpeed;

            Jump();
        }

        public override void ExitState() {
            base.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            if(!player._isJumpPressed && player.Rb.velocity.y > 0f) {
                ShortJumpFall();
            }
        }

        public override void PhysicUpdate() {
            base.PhysicUpdate();
            #region Jump_Fall
            // Apply extra gravity to make the player fall faster after reaching the peak of the jump
            if(player.Rb.velocity.y < 0f) {
                player.Rb.velocity += Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime * Vector2.up;
            }

            if(!player.playerCollider.GroundCheck() && player.playerInput._moveInput.x != 0) {
                player.Rb.AddForce(jmpMoveSpeed * movement * Vector2.right, ForceMode2D.Force);
                Debug.Log("Testing111");
            }
            #endregion


        }

        protected override void CheckStateChange() {
            base.CheckStateChange();
/*            if(player.playerInput.jumpPressed != true) {
                StateMachine.ChangeState(player.IdelState);  // Ideal
            }*/
            if(player.playerCollider.GroundCheck()) {
                StateMachine.ChangeState(player.IdelState);
            }

        }

        void Jump() {
            player.Rb.velocity = new Vector2(player.Rb.velocity.x, jumpForce);
            Debug.Log("Testing111");
        }

        internal void ShortJumpFall() {
            player.Rb.velocity = new Vector2(player.Rb.velocity.x, -fallMultiplier);
        }

    }
}
