using UnityEngine;

namespace OriginL.Player
{
    public class PlayerState {
        public PlayerScript player;
        protected PlayerStateMachine StateMachine;
        protected float movement = 0;

        public PlayerState(PlayerScript player, PlayerStateMachine StateMachine) {
            this.player = player;
            this.StateMachine = StateMachine;
        }
        #region Virtual State Functions
        public virtual void EnterState() {

        }

        public virtual void FrameUpdate() {
            CheckStateChange();
        }

        public virtual void PhysicUpdate() { }

        public virtual void ExitState() { }

        #endregion

        protected virtual void CheckStateChange() {
        }





    }
}
