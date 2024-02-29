using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdelState IdelState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState {  get; set; }
    public EnemyDeathState DeadState {  get; set; }

    #endregion

    #region ScriptVariables
    [Header("Scripts")]
    internal EnemyCollider enemyCollider;
    internal EnemyStat enemyStatus;
    internal EnemyController enemyController;
    internal EnemyAI enemyAI;
    internal EnemyAnimController enemyAnimCont;
    internal EnemyCombact enemyCombact;
    #endregion

    public List<ItemClass> drops;

    #region Primitives

    internal Rigidbody2D rb;
    internal GameManager gameManager;

    public GameObject Target { get;  set; }

    internal bool isGrounded;


    #endregion

    private void Awake() {
        #region State instantiation 
        StateMachine = new EnemyStateMachine();
        IdelState = new EnemyIdelState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        DeadState = new EnemyDeathState(this, StateMachine);



        #endregion
        #region ScriptLinker
        enemyAI = GetComponent<EnemyAI>();
        enemyController = GetComponent<EnemyController>();
        enemyCombact = GetComponent<EnemyCombact>();
        enemyAnimCont = GetComponent<EnemyAnimController>();
        enemyCollider = GetComponent<EnemyCollider>();
        enemyStatus = GetComponent<EnemyStat>();



        if(enemyCollider == null)
            Debug.LogError("enemyCollider is null!");
        if(enemyStatus == null)
            Debug.LogError("enemyStatus is null!");
        if(enemyController == null)
            Debug.LogError("enemyController is null!");
        if(enemyAnimCont == null)
            Debug.LogError("enemyAnimCont is null!");
        if(enemyCombact == null)
            Debug.LogError("enemyCombact is null!");
        if(enemyAI == null)
            Debug.LogError("enemyAI is null!");

        #endregion

        #region Components
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        Target = GameObject.FindGameObjectWithTag("Player");
        #endregion
    }

    private void Start() {
        StateMachine.Initialize(IdelState);
    }

    private void Update() {
        StateMachine.CurrentState.FrameUpdate();

    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicUpdate();
    }

}
