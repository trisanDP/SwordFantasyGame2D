
using UnityEngine;



    public class PlayerControllerPhy : MonoBehaviour
    {

        #region Variables
        [Header("Main_Script")]
        internal PlayerScript player;

        [Header("Movement Attributes")]
        [SerializeField] float accel;
        [SerializeField] float decell;
        [SerializeField] float moveSpeed;
        [SerializeField] float velPower;
        internal bool canMove = true;

        [Header("Jump & Gravity ")]
        [SerializeField] float jumpForce;
        [SerializeField] int fallMultiplier = 15;
        [Range(0, 2)][SerializeField]internal float jmpMoveSpeed;
        //[SerializeField] int Climb_Speed = 100;

        [Header("Internal")]
        Vector2 inputMove;
        float movement = 0;
        Rigidbody2D rb;

        #endregion

        void Start() {
            player = GetComponent<PlayerScript>();
            rb = player.Rb;
        }

        private void Update() {
            if(canMove == true)
                inputMove = player.playerInput._moveInput;
            

        }

        private void FixedUpdate() {

            #region Jump_Fall
            // Apply extra gravity to make the player fall faster after reaching the peak of the jump
            if(rb.velocity.y < 0f) {
                rb.velocity += Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime * Vector2.up;
            }
            #endregion

            #region Movement Physic
                

/*            if(player.playerCollider.GroundCheck() == false && movement != 0) {
                
            }*/

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

        internal void ShortJumpFall() {
            rb.velocity = new Vector2(rb.velocity.x, -fallMultiplier);
        }

        internal void JumpMove() {
            Move(false);
/*            rb.AddForce(jmpMoveSpeed * movement * Vector2.right, ForceMode2D.Force);*/
        }

        #endregion
    }
