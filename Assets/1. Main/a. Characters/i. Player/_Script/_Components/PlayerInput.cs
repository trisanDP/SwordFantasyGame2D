using UnityEngine.InputSystem;
using UnityEngine;

namespace OriginL.Player {
    public class PlayerInput : MonoBehaviour {
        #region VARIABLES
        internal PlayerScript player;


        public Vector2 _moveInput;

        [Header("Link")]
        internal bool attacking;
        internal bool _Attack;


        /*    [SerializeField]bool canJump; 
            [SerializeField]bool _isJumpPressed;*/

        #endregion

        private void Start() {
            player = GetComponent<PlayerScript>();
            /*        canJump = true;
                    _isJumpPressed = false;*/
        }

        private void Update() {
            #region OldInputSystem

            #region UIcommand
            if(Input.GetKeyDown(KeyCode.P)) {
                player.playerEquipmentM.equipmentUI.ToggleUi();  // PlayerScript => PlayerEquipmentScript => UimanagerScript (ToggleUi())
            }
            if(Input.GetKeyDown(KeyCode.U)) {
                player.playerEquipmentM.UnEquipAll();
            }

            if(Input.GetKeyDown(KeyCode.I)) {
                UiManager.Instance.InventoryUI.Toggle();
            }
            #endregion

            #region GameCommand
            //GameOver....
            if(Input.GetKeyDown(KeyCode.O)) {
                GameManager.Instance.GameOver();

            }


            #endregion

            #endregion
        }



        #region NewInputSystemF

        #region Jump
        /*
            public void OnJump(InputAction.CallbackContext context) {
        *//*        _isJumpPressed = context.ReadValueAsButton();*//*
                if(context.performed && canJump) {
                    _isJumpPressed = true;
                    canJump = false;
                } else if(context.canceled) {
                    _isJumpPressed = false;
                    canJump = true;
                } else
                    _isJumpPressed = false;
            }*/

        public bool JumpAction() {
            return false;
        }

        #endregion

        #region Movement
        public void OnMove(InputAction.CallbackContext context) {
            if(context.performed) {
                _moveInput = context.ReadValue<Vector2>();
            } else
                _moveInput = new Vector2(0, 0);

        }
        #endregion

        #region Attack
        //.....................
        public void OnAttack(InputAction.CallbackContext context) {
            if(context.performed) {
                if(context.action.name == "Attack1")
                    player.playerCombact.MeleeAttack1();

            }
            if(context.action.name == "Attack2")
                player.playerCombact.MeleeAttack2();

        }

        public void OnTestAction(InputAction.CallbackContext context) {
            Debug.Log(context.ReadValueAsButton());
            if(context.started) {
                Debug.Log("Started");
            }
            if(context.performed) {
                Debug.Log("Performed");
            }
            if(context.canceled) {
                Debug.Log("Canceled");
            }
        }
        //........................
        #endregion

        #endregion

    }
}