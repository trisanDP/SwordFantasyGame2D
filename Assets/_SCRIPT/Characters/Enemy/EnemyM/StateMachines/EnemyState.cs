using UnityEngine;

namespace OriginL.EnemySpace {
    public class EnemyState {
        #region Variables

        [Header("Components")]
        protected Enemy enemy;
        protected EnemyStateMachine enemyStateMachine;
        protected GameObject targetObj = null;

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
            attackR = enemy.enemyCollider.attackRange;
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

        #region Extra Function

        public virtual void StateChangeCheaker() { }

        #endregion

        #region Event Function
        protected virtual void GetDistanceFromTarget() {
            if(enemy.enemyCollider.InDetectRange()) {
                targetObj = enemy.Target;
                distFromTarget = Vector2.Distance(targetObj.transform.position, enemy.transform.position);
                Debug.Log("Testing111");
            }
        }

        void CheckHealth() {
            if(enemy.enemyStatus.currentHealth <= 0) {
                enemyStateMachine.ChangeState(enemy.DeadState);
                Debug.Log("Change to death");
            }
        }

        #endregion
    }
}