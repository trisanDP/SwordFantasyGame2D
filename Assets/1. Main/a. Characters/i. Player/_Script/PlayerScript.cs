using BrokenLands;
using UnityEngine;

namespace OriginL.Player {
    public class PlayerScript : MonoBehaviour {

        #region Variables

        #region Components
        [Header("Scripts")]
        internal PlayerControllerPhy playerController;
        internal PlayerCollider playerCollider;
        internal PlayerInput playerInput;
        internal PlayerAnimationController p_animCont;
        internal PlayerStat playerStat;
        internal PlayerCombact playerCombact;
        internal PlayerIntract playerIntract;
        internal PlayerEquipmentManager playerEquipmentM;

        [Header("Components")]
        internal Rigidbody2D rb;
        public GameObject Target { get; private set; }

        #endregion

        #region State Scripts
        public PlayerIdelState IdelState { get; private set; }
        public PlayerMovingState MovingState { get; private set; }
        public PlayerAttackingState PlayerAttackingState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }

        public PlayerStateMachine PlayerStateMachine { get; private set; }
        #endregion

        #region Movement Attributes
        [Header("Movement Attributes")]
        [SerializeField] internal float accel;
        [SerializeField] internal float decell;
        [SerializeField] internal float moveSpeed;
        [SerializeField] internal float velPower;

        [Header("Jump & Gravity")]
        [SerializeField] internal float jumpForce;
        [SerializeField] internal int fallMultiplier = 15;
        [Range(0, 2)][SerializeField] internal float jmpMoveSpeed;
        #endregion

        [SerializeField] internal bool isDead = false;

        #endregion

        public static PlayerScript Instance;
        #region MonoBehaviour Methods

        private void Awake() {
            InitializeComponents();
            InitializeStateMachine();

            if(Instance == null) {
                Instance = this;
/*                DontDestroyOnLoad(gameObject);  // Optional, depending on usage*/
            } else {
                Destroy(gameObject);  // Destroy duplicate instance
            }
        
        }

        private void Start() {
            PlayerStateMachine.Initialize(IdelState);
        }

        private void Update() {
            PlayerStateMachine.CurrentState.FrameUpdate();
/*            if(rb.linearVelocity.y < 0)
*//*                Debug.Log("Here");*/
        }

        private void FixedUpdate() {
            PlayerStateMachine.CurrentState.PhysicUpdate();
        }

        #endregion

        #region Initialization Methods

        private void InitializeComponents() {
            rb = GetComponent<Rigidbody2D>();

            playerController = GetComponent<PlayerControllerPhy>();
            playerCollider = GetComponent<PlayerCollider>();
            playerInput = GetComponent<PlayerInput>();
            p_animCont = GetComponent<PlayerAnimationController>();
            playerStat = GetComponent<PlayerStat>();

            playerCombact = GetComponent<PlayerCombact>();
            playerIntract = GetComponent<PlayerIntract>();
            playerEquipmentM = GetComponentInChildren<PlayerEquipmentManager>();
        }

        private void InitializeStateMachine() {
            PlayerStateMachine = new PlayerStateMachine();

            IdelState = new PlayerIdelState(this, PlayerStateMachine);
            PlayerAttackingState = new PlayerAttackingState(this, PlayerStateMachine);
            MovingState = new PlayerMovingState(this, PlayerStateMachine);
            JumpState = new PlayerJumpState(this, PlayerStateMachine);
        }

        #endregion
    }
}
