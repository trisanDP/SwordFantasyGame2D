using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdelState : EnemyState {

    protected bool canPetrol;

    [Header("Petrol")]
    [SerializeField] protected float idelDuration = 1;

    public EnemyIdelState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {
        
    }

    public override void EnterState() {
        base.EnterState();
        //................................
        enemy.rb.velocity = Vector2.zero;
        enemy.enemyAnimCont.PlayIdelAnimation();

        
    }

    public override void FrameUpdate() {
        base.FrameUpdate();
        Debug.Log("Ideal State");
    }

    public override void PhysicUpdate() {
        base.PhysicUpdate();
        if(canPetrol == true) {
            StartPetrolMovement();
        }
    }
    public override void ExitState() {
        base.ExitState();
    }

    internal void StartPetrolMovement() {
        
    }

    public override void StateChangeCheaker() {
        base.StateChangeCheaker();
        if(distFromTarget < detectR) {                 // Entering Detect Range
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }

    }
}
