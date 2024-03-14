using UnityEngine;

namespace OriginL.Player {
    public class PlayerScript : MonoBehaviour {

        #region variables

        #region ComponentsScript
        [Header("Scripts")]
        internal PlayerControllerPhy playerController;
        internal PlayerCollider playerCollider;
        internal PlayerInput playerInput;
        internal PlayerAnimationController p_animCont;
        internal PlayerStat playerHealth;
        internal P_QuestManager playerQuestM;
        internal PlayerCombact playerCombact;
        internal PlayerIntract playerIntract;
        internal PlayerEquipmentManager playerEquipmentM;
        #endregion

        #region StateScripts
        public PlayerIdelState IdelState { get; private set; }
        public PlayerMovingState MovingState { get; private set; }
        public PlayerAttackingState PlayerAttackingState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }

        public PlayerStateMachine playerStateMachine;
        #endregion

        #region Components
        [Header("Components")]
        internal Rigidbody2D Rb;
        public GameObject target { get; private set; }
        #endregion

        #region Primitives
        [SerializeField] internal bool isDead = false;

        [Header("Movement Attributes")]  //Copyed
        [SerializeField] internal float accel;
        [SerializeField] internal float decell;
        [SerializeField] internal float moveSpeed;
        [SerializeField] internal float velPower;

        [Header("Jump & Gravity ")]
        [SerializeField] internal float jumpForce;
        [SerializeField] internal int fallMultiplier = 15;
        [Range(0, 2)][SerializeField] internal float jmpMoveSpeed;

        #endregion

        #endregion


        protected void Awake() {
            Rb = GetComponent<Rigidbody2D>();

            #region ComponentScriptLink
            playerController = GetComponent<PlayerControllerPhy>();
            playerCollider = GetComponent<PlayerCollider>();
            playerInput = GetComponent<PlayerInput>();
            p_animCont = GetComponent<PlayerAnimationController>();
            playerHealth = GetComponent<PlayerStat>();
            playerQuestM = GetComponent<P_QuestManager>();
            playerCombact = GetComponent<PlayerCombact>();
            playerIntract = GetComponent<PlayerIntract>();
            playerEquipmentM = GetComponentInChildren<PlayerEquipmentManager>();
            #endregion

            #region StateScriptLink
            playerStateMachine = new PlayerStateMachine();
            IdelState = new PlayerIdelState(this, playerStateMachine);
            PlayerAttackingState = new PlayerAttackingState(this, playerStateMachine);
            MovingState = new PlayerMovingState(this, playerStateMachine);
            JumpState = new PlayerJumpState(this, playerStateMachine);

            #endregion

        }
        private void Start() {
            playerStateMachine.Initialize(IdelState);
        }
        private void Update() {
            playerStateMachine.CurrentState.FrameUpdate();
        }
        private void FixedUpdate() {
            playerStateMachine.CurrentState.PhysicUpdate();
        }
    }
}