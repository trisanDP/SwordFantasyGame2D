using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{

    PlayerScript playerScrip;

    private void Awake() {
        playerScrip = GetComponent<PlayerScript>();
    }

    private void Update() {
/*        switch(playerScrip.ActiveState) {
            case PlayerScript.State.Moving_State:
            if(playerScrip.playerInput._moveInput.x == 0 && playerScrip.playerInput.jumpPressed == false) {
                playerScrip.ActiveState = PlayerScript.State.idel_State;  // Moving 
            }
            break;

            case PlayerScript.State.idel_State:
            //PlayIdeal Animation 
            if(playerScrip.playerInput._moveInput.x != 0) {
                playerScrip.ActiveState = PlayerScript.State.Moving_State;  // Moving 
            }
            break;

            case PlayerScript.State.Jumping_State:
            if(playerScrip.playerInput.jumpPressed != true) {
                playerScrip.ActiveState = PlayerScript.State.idel_State;  // Ideal
            }
            break;


            default:
            Debug.Log("Default Case");
            break;
        }*/
    }
}
