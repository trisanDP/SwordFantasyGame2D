using UnityEngine;

namespace OriginL.Player
{
    public class PlayerAttackingState : PlayerState {
        public PlayerAttackingState(PlayerScript player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {

        }

        public override void EnterState() {
            Debug.Log(" AttackState ");
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
        }
    }
}
