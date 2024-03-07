using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

namespace OriginL
{
    public class PlayerMovingState : PlayerState {

        [Header("Movement Attributes")]
        [SerializeField] float accel;
        [SerializeField] float decell;
        [SerializeField] float moveSpeed;
        [SerializeField] float velPower;
        internal bool canMove = true;
        float movement = 0;

        Vector2 inputMove;


        public PlayerMovingState(PlayerScript player, PlayerStateMachine StateMachine) : base(player, StateMachine) {
        }

        public override void EnterState() {
            accel = player.playerController.accel;
            decell = player.playerController.decell;
            moveSpeed = player.playerController.moveSpeed;
            velPower = player.playerController.velPower;
            Debug.Log(" Moving State ");
            base.EnterState();
        }

        public override void ExitState() {
            base.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            
          
        }

        public override void PhysicUpdate() {
            base.PhysicUpdate();
            if(player.playerCollider.GroundCheck()) {
                player.Rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
            }
            if(canMove == true) {
                inputMove = player.playerInput._moveInput;
                Move();
            }
        }

        protected override void CheckStateChange() {
            base.CheckStateChange();
            if(player.playerInput._moveInput.x == 0 && player.playerInput.jumpPressed == false) {
                StateMachine.ChangeState(player.IdelState);
            }

        }


        #region Movement

        void Move() {
            float targetSpeed = inputMove.x * moveSpeed;
            float speedDif = targetSpeed - player.Rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? accel : decell;
            movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        }

        #endregion

    }
}
