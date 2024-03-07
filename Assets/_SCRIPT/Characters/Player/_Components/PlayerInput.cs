using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region VARIABLES

    internal PlayerScript playerScrip;


    public Vector2 _moveInput;

    [Header("Link")]
    internal bool attacking;
    internal bool _Attack;

    [Header("Internal")]
    [SerializeField] internal bool jumpPressed = false;
    #endregion

    private void Start() {
        playerScrip = GetComponent<PlayerScript>();
    }

    private void Update() {

        if(playerScrip.playerCollider.GroundCheck() == false) {
            jumpPressed = false;
        }

        #region OldInputSystem

        #region UIcommand
        if(Input.GetKeyDown(KeyCode.P)) {
            playerScrip.playerEquipmentM.equipmentUI.ToggleUi();  // PlayerScript => PlayerEquipmentScript => UimanagerScript (ToggleUi())
        }
        if(Input.GetKeyDown(KeyCode.U)) {
            playerScrip.playerEquipmentM.UnEquipAll();
        }

        if(Input.GetKeyDown(KeyCode.I)) {
            GameObject ui = UiManager.Instance.InventoryUI.gameObject;
            ui.SetActive(!ui.activeSelf);
            ui = null;
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
    public void OnJump(InputAction.CallbackContext context) {
        if(context.performed) {
            jumpPressed = context.ReadValueAsButton();
        }
 
        if(context.canceled) {
            if(playerScrip.Rb.velocity.y > 0) {
                jumpPressed = context.ReadValueAsButton();
            }
        }
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
    public void OnAttack1(InputAction.CallbackContext context) {
        if(context.performed) {
            if(context.action.name == "Attack1")
                playerScrip.playerCombact.MeleeAttack1();
            if(context.action.name == "Attack2") 
                playerScrip.playerCombact.MeleeAttack2();
            
        }

    }

    //........................
    #endregion


    #endregion

}
