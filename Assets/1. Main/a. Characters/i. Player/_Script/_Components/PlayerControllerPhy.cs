using UnityEngine;

namespace OriginL.Player {
    public class PlayerControllerPhy : MonoBehaviour {

        #region Variables
        [Header("Main_Script")]
        PlayerScript player;

        [Header("Movement Attributes")]
        float accel;
        float decell;
        float moveSpeed;
        float velPower;
        float movement;

        internal bool canMove;

        [Header("Jump & Gravity ")]
        float jumpForce;
        int fallMultiplier = 15;
        float jmpMoveSpeed;

        [Header("Input")]
        Vector2 inputMove;

        [Header("Components")]
        Rigidbody2D rb;

        #endregion

        private void Awake() {
            player = GetComponent<PlayerScript>();
            canMove = true;
        }

        void Start() {
            accel = player.accel;
            decell = player.decell;
            moveSpeed = player.moveSpeed;
            velPower = player.velPower;
            jumpForce = player.jumpForce;
            jmpMoveSpeed = player.jmpMoveSpeed;
            fallMultiplier = player.fallMultiplier;
            rb = player.Rb;
        }

        private void Update() {
            if(canMove == true)
                inputMove = player.playerInput._moveInput;


        }

        private void FixedUpdate() {

            #region Jump_Fall
            // Apply extra gravity to make the player fall faster after reaching the peak of the jump
            NormalFall();
            #endregion
        }

        #region Movement
        internal void Move(bool isGrounded) {
            float targetSpeed = inputMove.x * moveSpeed;
            float speedDif = targetSpeed - rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? accel : decell;
            movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
            if(isGrounded)
                player.Rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
            else
                rb.AddForce(jmpMoveSpeed * movement * Vector2.right, ForceMode2D.Force);
        }

        #endregion

        #region Jump
        internal void Jump() {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }



        internal void JumpMove() {
            Move(false);
            rb.AddForce(jmpMoveSpeed * movement * Vector2.right, ForceMode2D.Force);
        }

        #endregion

        #region FallPhy
        internal void ShortJumpFall() {
            rb.velocity = new Vector2(rb.velocity.x, -fallMultiplier);
        }

        internal void NormalFall() {
            if(rb.velocity.y < 0f) {
                rb.velocity += Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime * Vector2.up;
            }
        }
        #endregion
    }
}