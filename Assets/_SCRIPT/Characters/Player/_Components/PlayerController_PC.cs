using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController_PC : MonoBehaviour
{

    #region Variables
    [Header("Main_Script")]
    internal PlayerScript playerScrip;

    [Header("Movement Attributes")]  //Copyed
    [SerializeField]internal float accel;
    [SerializeField]internal float decell;
    [SerializeField]internal float moveSpeed;
    [SerializeField]internal float velPower;
    internal bool canMove = true;

    [Header("Jump & Gravity ")]
    [SerializeField]internal float jumpForce;
    [SerializeField]internal int fallMultiplier = 15;
    [Range(0, 2)][SerializeField]internal float jmpMoveSpeed;

    [Header("Internal")]
/*    Vector2 inputMove;
    float movement = 0;*/
    Rigidbody2D rb;




    #region To_Test
    [Header("Testing")]
    [SerializeField] float test_f;
    [SerializeField] bool test_b;

    #endregion

    #endregion

    void Start()
    {
        playerScrip = GetComponent<PlayerScript>();
        rb = playerScrip.Rb;
    }

    private void Update(){

    }

    private void FixedUpdate() {

/*        #region Jump_Fall
        // Apply extra gravity to make the player fall faster after reaching the peak of the jump
        if(rb.velocity.y < 0f) {
            rb.velocity += Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime * Vector2.up;
        }
        #endregion


        #region Movement Physic

        if(playerScrip.playerCollider.GroundCheck()) {
            rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        } else if(playerScrip.playerCollider.GroundCheck() == false && playerScrip.ActiveState == PlayerScript.State.Moving_State) {
            rb.AddForce(jmpMoveSpeed * movement * Vector2.right, ForceMode2D.Force);
        }

        #endregion*/
    }


    #region Jump
    internal void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    internal void ShortJumpFall() {
        rb.velocity = new Vector2(rb.velocity.x, -fallMultiplier);
    }


/*    void Move() {
        float targetSpeed = inputMove.x * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? accel : decell;
        movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

    }*/
    #endregion
}
