using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdelState IdelState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState {  get; set; }
    #endregion
    private void Awake() {
        StateMachine = new EnemyStateMachine();
        IdelState = new EnemyIdelState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
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
