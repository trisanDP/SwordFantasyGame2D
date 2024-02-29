using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyChaseState : EnemyState {


    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {
        
    }

    public override void EnterState() {
/*        Debug.Log("Chase State");*/
        base.EnterState();

    }


    public override void FrameUpdate() {
        base.FrameUpdate();
        Debug.Log("Chase");
    }
    public override void PhysicUpdate() {
        base.PhysicUpdate();
        StartChasing(target);
    }

    public override void ExitState() {
        base.ExitState();

    }
    internal void StartChasing(GameObject obj) {
        enemy.enemyController.EnemyMoveController(obj, ChasingSpeed);
    }

    public override void StateChangeCheaker() {
        base.StateChangeCheaker();

        if(distFromTarget < attackR) {//On Entering Attacking Range
            enemyStateMachine.ChangeState(enemy.AttackState);
        } else if(distFromTarget > detectR) {     //Out of Detect range then start chasing again:
            enemyStateMachine.ChangeState(enemy.IdelState);
        }


    }


}
