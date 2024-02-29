using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState {
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {
    }

    public override void EnterState() {
        base.EnterState();
    }

    public override void FrameUpdate() {
        base.FrameUpdate();
        AttackingState();
       
    }

    public override void PhysicUpdate() {
        base.PhysicUpdate();
        enemy.enemyController.StopEnemyMovement();
    } 

    public override void ExitState() {
        base.ExitState();
    }

    internal void AttackingState() {
        
        enemy.enemyCombact.MeleeAttack1();
        Debug.Log("Attack");

    }

    public override void StateChangeCheaker() {
        base.StateChangeCheaker();
        if(distFromTarget > attackR) {             //On Entering Attacking Range
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }
    }
}
