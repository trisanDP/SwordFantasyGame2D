using UnityEngine;

namespace OriginL.EnemySpace {
    public class EnemyAttackState : EnemyState {
        public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {
        }

        public override void EnterState() {
            enemy.rb.velocity = Vector2.zero;
            base.EnterState();
        }

        public override void FrameUpdate() {

            base.FrameUpdate();
            AttackingState();

        }

        public override void PhysicUpdate() {
            base.PhysicUpdate();
        }

        public override void ExitState() {
            base.ExitState();
            Debug.Log("ChangeState");
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
}