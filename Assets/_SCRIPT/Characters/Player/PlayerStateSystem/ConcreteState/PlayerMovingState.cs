using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

namespace OriginL {
    public class PlayerMovingState : PlayerState {

        [Header("Movement Attributes")]

        [Range(0, 2)][SerializeField] float jmpMoveSpeed;
        Vector2 inputMove;
        public PlayerMovingState(PlayerScript player, PlayerStateMachine StateMachine) : base(player, StateMachine) {
        }

        public override void EnterState() {
            Debug.Log(" Moving State ");
            base.EnterState();
            jmpMoveSpeed = player.playerController.jmpMoveSpeed;
            player.p_animCont.animator_Player.SetInteger("Speed", 1);
        }

        public override void ExitState() {
            base.ExitState();
            player.p_animCont.animator_Player.SetInteger("Speed", 0);

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
            if(player.playerInput._isJumpPressed) {
                StateMachine.ChangeState(player.JumpState);
            }
        }
        /*
                void CheckMoveJump() {
                    if(player.playerInput.isJumpPressed() && !player.playerCollider.GroundCheck()) {
                        StateMachine.ChangeState(player.JumpMoveState);
                        player.playerController.Move(true);
                        Debug.Log("Testing111");
                    }
                }*/

    }
}