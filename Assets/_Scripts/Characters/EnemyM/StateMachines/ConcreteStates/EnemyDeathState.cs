using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UIElements;

public class EnemyDeathState : EnemyState {
    public GameObject dropBox;
    public EnemyDeathState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {
    }

    public override void EnterState() {
        base.EnterState();
        Debug.Log("Death State");  
        enemy.enemyStatus.Die();
    }
    public override void FrameUpdate() { 
        
    }

    public override void ExitState() {
        base.ExitState();
        Debug.Log("Dead exit");
    }

}
