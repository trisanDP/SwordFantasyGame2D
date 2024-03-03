using OriginL.EnemySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace OriginL
{
    public class PlayerState 
    {
        public PlayerScript player;
        protected PlayerStateMachine playerStateMachine;
        public PlayerState(PlayerScript player, PlayerStateMachine playerStateMachine) {
            this.player = player;
            this.playerStateMachine = playerStateMachine;
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
            if(player.Rb.velocity.x != 0) {
                playerStateMachine.ChangeState(player.playerMovingState);
            }
        }

    }
}
