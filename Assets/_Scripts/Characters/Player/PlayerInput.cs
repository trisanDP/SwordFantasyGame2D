using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region VARIABLES

    internal PlayerScript playerScrip;
    internal float _moveInput;


    [Header("")]
    internal int moveState = 0;


    [Header("Link")]
    internal bool attacking;

    [Header("Internal")]
    [SerializeField] internal bool jumpPressed = false;
    #endregion

    private void Start() {
        playerScrip = GetComponent<PlayerScript>();
    }
    private void Update() {

        #region Movement
        //............................

        _moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            moveState += 1;
            if (moveState >= 3) {
                moveState = 0;
            }
            Debug.Log(moveState);
        }

        //.............................
        #endregion 

        #region Jump
            //......................
        if (Input.GetKeyDown(KeyCode.Space)) {
                playerScrip.ActiveState = PlayerScript.State.Jumping_State; // Long Jump
                jumpPressed = true;
                Debug.Log("Pressed");
        }

        if (playerScrip.playerCollider.GroundCheck() == false) {
            jumpPressed = false;
        }

        if (Input.GetKeyUp(KeyCode.Space) && playerScrip.Rb.velocity.y > 0) {  // Short Jump
            playerScrip.playerController.ShortJump();
        }
        //.........................
        #endregion


        #region Attack
        //.....................
        //Attack2 . Normal Attack
        if (Input.GetKeyDown(KeyCode.F)) {
            playerScrip.playerCombact.MeleeAttack1();
        }

        //Attack 2 Strong Attack
        if (Input.GetKeyDown(KeyCode.R)) {
            playerScrip.playerCombact.MeleeAttack2();
        }

        //........................
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
