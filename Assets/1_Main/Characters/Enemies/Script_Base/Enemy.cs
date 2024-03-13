using System.Collections.Generic;
using UnityEngine;



namespace OriginL.EnemySpace {
    public class Enemy : MonoBehaviour {
        #region State Machine Variables

        public EnemyStateMachine StateMachine { get; set; }
        public EnemyIdelState IdelState { get; set; }
        public EnemyChaseState ChaseState { get; set; }
        public EnemyAttackState AttackState { get; set; }
        public EnemyDeathState DeadState { get; set; }

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

        #region Primitives

        internal Rigidbody2D rb;

        [SerializeField] internal GameObject Target;

        internal bool isGrounded;
        internal bool hasAggroed;

        #endregion

        #region Events
        #endregion

        #region Type
        [SerializeField] internal float petrolSpeed;
        [SerializeField] internal float chasingSpeed;
        [SerializeField] internal float activeSpeed;

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
            enemyController = new EnemyController(this, activeSpeed);

            enemyAI = GetComponent<EnemyAI>();
            enemyCombact = GetComponent<EnemyCombact>();
            enemyAnimCont = GetComponent<EnemyAnimController>();
            enemyCollider = GetComponent<EnemyCollider>();
            enemyStatus = GetComponent<EnemyStat>();



            if(enemyCollider == null)
                Debug.LogWarning("enemyCollider is null!");
            if(enemyStatus == null)
                Debug.LogWarning("enemyStatus is null!");
            if(enemyController == null)
                Debug.LogWarning("enemyController is null!");
            if(enemyAnimCont == null)
                Debug.LogWarning("enemyAnimCont is null!");
            if(enemyCombact == null)
                Debug.LogWarning("enemyCombact is null!");
            if(enemyAI == null)
                Debug.LogWarning("enemyAI is null!");

            #endregion

            #region Components
            rb = GetComponent<Rigidbody2D>();
            Target = GameObject.FindGameObjectWithTag("Player");
            #endregion
        }

        private void Start() {
            StateMachine.Initialize(IdelState);
            petrolSpeed = Random.Range(petrolSpeed * 1f, petrolSpeed);
            chasingSpeed = Random.Range(chasingSpeed * 0.85f, chasingSpeed);
        }

        private void Update() {
            StateMachine.CurrentState.FrameUpdate();
        }

        private void FixedUpdate() {
            StateMachine.CurrentState.PhysicUpdate();
        }

        internal void AggroTo(GameObject target) {
            hasAggroed = true;
            this.Target = target;
            Debug.Log("TargetSet");
        }
        internal void CancelAgroo() {
            hasAggroed = false;
            Target = null;
        }

    }
}