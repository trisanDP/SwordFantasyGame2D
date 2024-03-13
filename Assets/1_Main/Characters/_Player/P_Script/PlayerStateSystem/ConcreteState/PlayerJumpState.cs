using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace OriginL
{
    public class PlayerJumpState : PlayerState {

        public PlayerJumpState(PlayerScript player, PlayerStateMachine StateMachine) : base(player, StateMachine) {
        }

        [SerializeField] int fallMultiplier = 15;

        public override void EnterState() {
            base.EnterState();
            fallMultiplier = player.fallMultiplier;
            GameManager.Instance.DebugMessage("JumpSpace", GameManager.MessageField.PlayerState);
            player.playerController.Jump();

        }
        public override void FrameUpdate() {
            base.FrameUpdate();
            if(player.Rb.velocity.y < 0) {
                player.Rb.velocity += Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime * Vector2.up;
            }
        }

        public override void ExitState() {
            base.ExitState();

        }



        public override void PhysicUpdate() {
            base.PhysicUpdate();
            CheckFall();
            if(player.playerInput._moveInput.x != 0) {
                player.playerController.JumpMove();
            }
        }





        protected override void CheckStateChange() {
            base.CheckStateChange();
            CheckIdelState();
            CheckFall();
            CheckGrounded();
        }

        #region State Check
        void CheckIdelState() {
            if(player.playerCollider.GroundCheck()) {
                StateMachine.ChangeState(player.IdelState);
            }
        }

        /*        void CheckFallState() {
                    if(player.Rb.velocity.y < 0f) {
                        if(player.)
                        StateMachine.ChangeState(player.FallState);
                    }
                    if(player.Rb.velocity.y < 0 && !player._isJumpPressed) {
                        player.playerController.ShortJumpFall();
                    }
                }
        */
        void CheckGrounded() {
            if(player.playerCollider.GroundCheck()) {
                StateMachine.ChangeState(player.IdelState);
            }
        }

        void CheckFall() {
            if(player.Rb.velocity.y > 0 && !player.playerInput.JumpAction()) {
                player.playerController.ShortJumpFall();
            }


            /*else
                 player.Rb.velocity += Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime * Vector2.up;*/
        }

        #endregion


    }
}
