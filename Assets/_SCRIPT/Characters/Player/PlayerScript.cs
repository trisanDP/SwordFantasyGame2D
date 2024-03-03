using OriginL;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{

    internal enum State {
        idel_State, Moving_State, Jumping_State, Attacking_State,Obj_Interaction
    };

    [SerializeField] internal State ActiveState;

    #region variables

    #region ComponentsScript
    [Header("Scripts")]
    internal PlayerController_PC playerController;
    internal PlayerCollider playerCollider;
    internal PlayerInput playerInput;
    internal PlayerAnimationController p_animCont;
    internal PlayerStat playerHealth;
    internal PlayerStateController playerStateController;
    internal P_QuestManager playerQuestM;
    internal PlayerCombact playerCombact;
    internal PlayerIntract playerIntract;
    internal PlayerEquipmentManager playerEquipmentM;
    #endregion

    #region StateScripts
    public PlayerIdelState playerIdelState {  get; private set; }
    public PlayerMovingState playerMovingState {  get; private set; }
    public PlayerAttackingState playerAttackingState {  get; private set; }

    #endregion

    public PlayerStateMachine playerStateMachine;

    [Header("Components")]
    internal Rigidbody2D Rb;
    public GameObject target;

    [SerializeField] internal bool isDead = false;
    #endregion

    protected void Awake() {
        ActiveState = State.idel_State;
        Rb = GetComponent<Rigidbody2D>();

        #region ComponentScriptLink
        playerCollider = GetComponent<PlayerCollider>();
        playerController = GetComponent<PlayerController_PC>();
        playerInput = GetComponent<PlayerInput>();
        p_animCont = GetComponent<PlayerAnimationController>();
        playerHealth = GetComponent<PlayerStat>();
        playerQuestM = GetComponent<P_QuestManager>();
        playerCombact = GetComponent<PlayerCombact>();
        playerIntract = GetComponent<PlayerIntract>();
        playerEquipmentM = GetComponent <PlayerEquipmentManager>();
        #endregion

        #region StateScriptLink
        playerStateMachine = new PlayerStateMachine();
        playerIdelState = new PlayerIdelState(this,playerStateMachine);
        playerAttackingState = new PlayerAttackingState(this,playerStateMachine);
        playerMovingState = new PlayerMovingState(this,playerStateMachine);

        #endregion

    }
    private void Start() {
        playerStateMachine.Initialize(playerIdelState);
    }
    private void Update() {
        playerStateMachine.CurrentState.FrameUpdate();
    }
    private void FixedUpdate() {
        playerStateMachine.CurrentState.PhysicUpdate();
    }

    #region Extra Scrip

    #endregion
}
