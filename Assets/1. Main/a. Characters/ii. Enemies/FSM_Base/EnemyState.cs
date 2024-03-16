using UnityEngine;

namespace OriginL.EnemySpace {
    public class EnemyState {
        #region Variables

        [Header("Components")]
        protected Enemy enemy;
        protected EnemyStateMachine enemyStateMachine;

        [Header("Ranges")]
        protected float attackR;
        protected float detectR;
        protected float distFromTarget;


        #endregion

        public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine) {
            this.enemy = enemy;
            this.enemyStateMachine = enemyStateMachine;
        }

        #region Virtual State Functions
        public virtual void EnterState() {
            attackR = enemy.enemyCollider.GetAttackRange();
            detectR = enemy.enemyCollider.GetDetectRange();

        }

        public virtual void FrameUpdate() {
            CheckHealth();
            GetDistanceFromTarget();
            StateChangeCheaker();
        }

        public virtual void PhysicUpdate() { }

        public virtual void ExitState() { }

        #endregion

        #region StateChangeCheck

        public virtual void StateChangeCheaker() { 
            
        }

        #endregion

        #region mainFunctions
        protected virtual void GetDistanceFromTarget() {
            if(enemy.enemyCollider.InDetectRange()) {
                distFromTarget = Vector2.Distance(enemy.GetTarget().transform.position, enemy.transform.position);
            }
        }

        void CheckHealth() {
            if(enemy.enemyStatus.CurrentHealth <= 0) {
                enemyStateMachine.ChangeState(enemy.DeadState);
                Debug.Log("Change to death");
            }
        }

        #endregion

    }
}