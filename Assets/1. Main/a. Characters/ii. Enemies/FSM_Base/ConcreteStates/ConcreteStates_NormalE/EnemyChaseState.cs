using UnityEngine;
using UnityEditor.Build.Content;

namespace OriginL.EnemySpace {
    public class EnemyChaseState : EnemyState {

        float ChaseSpeed;
        bool interupted;
        public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {

        }

        public override void EnterState() {
            interupted = false;
            ChaseSpeed = enemy.chasingSpeed;
            enemy.State = "ChaseState";
            GameManager.Instance.DebugMessage("Chase State", GameManager.MessageField.Enemy);
            base.EnterState();
           
        }


        public override void FrameUpdate() {
            base.FrameUpdate();
        }
        public override void PhysicUpdate() {
            base.PhysicUpdate();
            if(!interupted)
                StartChasing(enemy.GetTarget());
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
            CheckIdelSwitch();
            CheckAttactSwitch();
            OnHitWall();
            
        }
        #region StateCheckers
        void CheckIdelSwitch() {
            if(!enemy.enemyCollider.InDetectRange()) {     //Out of Detect range then start chasing again:
                enemyStateMachine.ChangeState(enemy.IdelState);
            }

        }

        void CheckAttactSwitch() {
            if(distFromTarget < attackR && interupted == false) {//On Entering Attacking Range
                enemyStateMachine.ChangeState(enemy.AttackState);
                Debug.Log("2");
            }
        }

        void OnHitWall() {
            if(enemy.enemyCollider.HasHitWall()) {
                enemyStateMachine.ChangeState(enemy.AttackState);
                interupted = true;
                Debug.Log("1");
            } else
                interupted = false;
        }
    }
    #endregion
}