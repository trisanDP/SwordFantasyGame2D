using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL
{
    public class PlayerIdelState : PlayerState {
        public PlayerIdelState(PlayerScript player, PlayerStateMachine StateMachine) : base(player, StateMachine) {

        }

        public override void EnterState() {
            base.EnterState();
            player.p_animCont.PlayIdelAnim(true);
        }

        public override void ExitState() {
            base.ExitState();
            player.p_animCont.PlayIdelAnim(false);
        }

        public override void FrameUpdate() {
            base.FrameUpdate();

        }

        public override void PhysicUpdate() {
            base.PhysicUpdate();
        }

        protected override void CheckStateChange() {
            if(player.playerInput._moveInput.x != 0) {
                StateMachine.ChangeState(player.MovingState); 
            }

        }
    }
}
