using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

namespace OriginL {
    public class PlayerMovingState : PlayerState {

        [Header("Movement Attributes")]

        Vector2 inputMove;

        public PlayerMovingState(PlayerScript player, PlayerStateMachine StateMachine) : base(player, StateMachine) {
        }

        public override void EnterState() {
            GameManager.Instance.DebugMessage("Moving State");
            base.EnterState();
/*            player.p_animCont.animator_Player.SetInteger("Speed", 1);*/
        }

        public override void ExitState() {
            base.ExitState();
/*            player.p_animCont.animator_Player.SetInteger("Speed", 0);*/

        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            inputMove = player.playerInput._moveInput;
            /*            player.animator.SetInteger("Speed", (int)player.playerInput._moveInput.x);*/
        }

        public override void PhysicUpdate() {
            base.PhysicUpdate();
            if(player.playerCollider.GroundCheck()) {
                player.playerController.Move(true);
            }
        }

        protected override void CheckStateChange() {
            base.CheckStateChange();
            CheckIdelState();
            CheckJumpState();

        }

        void CheckIdelState() {
            if(inputMove.x == 0) {
                StateMachine.ChangeState(player.IdelState);
            }
        }

        void CheckJumpState() {
            if(player.playerInput.JumpAction()) {
                StateMachine.ChangeState(player.JumpState);
            }
        }

    }
}