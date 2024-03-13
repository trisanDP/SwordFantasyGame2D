using UnityEngine;
using UnityEditor.Build.Content;

namespace OriginL.EnemySpace {
    public class EnemyChaseState : EnemyState {

        float ChaseSpeed;

        public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {

        }

        public override void EnterState() {
            targetObj = enemy.Target;
            ChaseSpeed = enemy.chasingSpeed;
            /*        Debug.Log("Chase State");*/
            base.EnterState();
           
        }


        public override void FrameUpdate() {
            base.FrameUpdate();
            Debug.Log("Chase");
        }
        public override void PhysicUpdate() {
            base.PhysicUpdate();
            StartChasing(targetObj);
        }

        public override void ExitState() {
            base.ExitState();
            enemy.CancelAgroo();

        }
        internal void StartChasing(GameObject obj) {
            enemy.enemyController.MoveToObj(obj, ChaseSpeed);
        }

        public override void StateChangeCheaker() {
            base.StateChangeCheaker();

            if(distFromTarget < attackR) {//On Entering Attacking Range
                enemyStateMachine.ChangeState(enemy.AttackState);
            } else if(!enemy.enemyCollider.InDetectRange()) {     //Out of Detect range then start chasing again:
                enemyStateMachine.ChangeState(enemy.IdelState);
            }


        }


    }
}