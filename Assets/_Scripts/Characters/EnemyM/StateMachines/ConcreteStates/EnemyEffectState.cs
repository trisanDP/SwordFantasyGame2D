using System.Collections;
using UnityEngine;

public class EnemyEffectState : EnemyState {

    internal float stundDuration;
    public enum EffectState {
        Stund,Freez,Burn,Bleed
    }
    public EffectState activeEffect;
    public EnemyEffectState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {
    }

    public override void EnterState() {
        stundDuration = enemy.enemyStatus.stundDuration;
        Debug.Log("Effect State");
        base.EnterState();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void FrameUpdate() {

        base.FrameUpdate();
        EffectTrigger();
    }

    public override void PhysicUpdate() {
        base.PhysicUpdate();
    }

    public override void StateChangeCheaker() { 
        base.StateChangeCheaker();
    }

    internal void EffectTrigger() {
        switch(activeEffect) {
            case EffectState.Stund:
                enemy.enemyStatus.StartStundEffect();
            break;
        }
    }

    internal IEnumerator Stund() {
        enemy.enemyController.activeSpeed = 0;  //make movement manager as base
        yield return new WaitForSeconds(stundDuration);
        StateChangeCheaker();
    }

    
}
