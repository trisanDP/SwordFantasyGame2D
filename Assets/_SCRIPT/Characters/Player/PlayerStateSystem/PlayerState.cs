using UnityEngine;
using UnityEngine.InputSystem;


namespace OriginL
{
    public class PlayerState 
    {
        public PlayerScript player;
        protected PlayerStateMachine StateMachine;

        [SerializeField] private InputActionAsset controls;
        private InputActionMap _inputActionMap;
        private InputAction _jumpAction;
        public PlayerState(PlayerScript player, PlayerStateMachine StateMachine) {
            this.player = player;
            this.StateMachine = StateMachine;
        }
        #region Virtual State Functions
        public virtual void EnterState() {

        }

        public virtual void FrameUpdate() {
            CheckStateChange();   
            CheckStateChange();
        }

        public virtual void PhysicUpdate() { }

        public virtual void ExitState() { }

        #endregion

        protected virtual void CheckStateChange() {
            if(player.Rb.velocity == Vector2.zero) {
                player.ActiveState = PlayerScript.State.idel_State;
            }
        }
        
        void CheckJumpPressed() {

        }

    }
}
