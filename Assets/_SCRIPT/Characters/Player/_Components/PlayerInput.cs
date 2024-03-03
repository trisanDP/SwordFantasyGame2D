using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region VARIABLES

    internal PlayerScript playerScrip;
    public Vector2 _moveInput;


    [Header("Link")]
    internal bool attacking;

    [Header("Internal")]
    [SerializeField] internal bool jumpPressed = false;
    #endregion

    private void Start() {
        playerScrip = GetComponent<PlayerScript>();
    }
    #region NewInputSystemF
    public void OnJump(InputAction.CallbackContext context) {
        if(context.performed) {
            playerScrip.ActiveState = PlayerScript.State.Jumping_State;
            jumpPressed = true;
        }
    }

    public void OnShortJump(InputAction.CallbackContext context) {
        // Check if the action was canceled
        if(context.canceled) {
            // Check if the player is still jumping
            if(playerScrip.Rb.velocity.y > 0) {
                // Call the short jump method from the player controller
                playerScrip.playerController.ShortJump();
            }
        }
    }
    #region Movement

    #endregion
    public void OnMove(InputAction.CallbackContext context) {
        if(context.performed) {
            _moveInput = context.ReadValue<Vector2>();
        }else
            _moveInput = new Vector2(0,0);
    }

    #region Attack
    //.....................
    public void OnAttack1(InputAction.CallbackContext context) {
        if(context.performed)
            playerScrip.playerCombact.MeleeAttack1();
    }

    public void OnAttack2(InputAction.CallbackContext context) {
        if(context.performed)
            playerScrip.playerCombact.MeleeAttack2();

    }
    //........................
    #endregion


    #endregion

    private void Update() {
        if(playerScrip.playerCollider.GroundCheck() == false) {
            jumpPressed = false;
        }


        #region Jump
        //......................
        /*        if (Input.GetKeyDown(KeyCode.Space)) {
                        playerScrip.ActiveState = PlayerScript.State.Jumping_State; // Long Jump
                        jumpPressed = true;
                        Debug.Log("Pressed");
                }*/


        /*
                if (Input.GetKeyUp(KeyCode.Space) && playerScrip.Rb.velocity.y > 0) {  // Short Jump
                    playerScrip.playerController.ShortJump();
                }*/
        //.........................
        #endregion



        #region UIcommand
        if (Input.GetKeyDown(KeyCode.P)) {
            playerScrip.playerEquipmentM.equipmentUI.ToggleUi();  // PlayerScript => PlayerEquipmentScript => UimanagerScript (ToggleUi())
        }
        if (Input.GetKeyDown(KeyCode.U)) {
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
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameManager.Instance.GameOver();

        }

        #endregion

        #region Test
        /*        if (Input.GetKeyDown(KeyCode.C))  
                {
                    playerScrip.ActiveState = PlayerScript.State.idel_State;
                }
                if(Input.GetKeyDown(KeyCode.Q)) {
                }*/
        #endregion


    }

}
