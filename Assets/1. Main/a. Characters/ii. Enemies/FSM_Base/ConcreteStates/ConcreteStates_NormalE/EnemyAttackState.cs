using UnityEngine;

namespace OriginL.EnemySpace {
    public class EnemyAttackState : EnemyState {
        public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {
        }

        public override void EnterState() {
            enemy.rb.velocity = Vector2.zero;
            enemy.State = "AttackState";
            GameManager.Instance.DebugMessage("Attack State", GameManager.MessageField.Enemy);
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
        }

        internal void AttackingState() {
            enemy.enemyCombact.MeleeAttack1();
        }

        public override void StateChangeCheaker() {
            base.StateChangeCheaker();
            CheckSwitch_Chase();
            CheckSwitch_Idel();
        }
        void CheckSwitch_Chase() {

        }

        void CheckSwitch_Idel() {
            if(distFromTarget > attackR)
                enemy.StateMachine.ChangeState(enemy.ChaseState);
/*            if(!enemy.enemyCollider.InDetectRange()) {
                enemy.StateMachine.ChangeState(enemy.IdelState);
                Debug.Log("1");
            }*/
        }
    }
}