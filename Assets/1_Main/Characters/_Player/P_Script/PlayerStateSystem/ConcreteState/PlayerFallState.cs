using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL
{
    public class PlayerFallState : PlayerState {
        public PlayerFallState(PlayerScript player, PlayerStateMachine StateMachine) : base(player, StateMachine) {
        }
        [SerializeField] int fallMultiplier;
        public override void EnterState() {
            /*base.EnterState();
            Debug.Log(" Fall State ");
            fallMultiplier = player.fallMultiplier;*/

        }

        public override void ExitState() {
            base.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            /*player.p_animCont.animator_Player.SetFloat("yVelocity", player.Rb.velocity.y);
*/
        }

        public override void PhysicUpdate() {
            base.PhysicUpdate();
/*
            
            player.Rb.velocity += Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime * Vector2.up;
            
*//*            if(!player.playerInput.JumpAction() && player.Rb.velocity.y < 0f) {
                ShortJumpFall();
            }*/
        }

        protected override void CheckStateChange() {
            base.CheckStateChange();
/*            CheckIdelState();*/
        }


        internal void ShortJumpFall() {
            player.Rb.velocity = new Vector2(player.Rb.velocity.x, -fallMultiplier);
        }
/*
        void CheckIdelState() {
            if(player.playerCollider.GroundCheck()) {
                StateMachine.ChangeState(player.IdelState);
            }
        }*/

    }
}
