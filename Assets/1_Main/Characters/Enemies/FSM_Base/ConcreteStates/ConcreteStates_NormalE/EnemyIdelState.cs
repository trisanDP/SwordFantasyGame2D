using UnityEngine;

namespace OriginL.EnemySpace {
    public class EnemyIdelState : EnemyState {

        bool canPetrol;


        [Header("Petrol")]
        [SerializeField] protected float idelDuration = 1;
        float petrolSpeed;

        public EnemyIdelState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {

        }

        public override void EnterState() {
            base.EnterState();
            petrolSpeed = enemy.petrolSpeed;
            canPetrol = false;
            //................................
            enemy.rb.velocity = Vector2.zero;
            enemy.enemyAnimCont.PlayIdelAnimation();


        }

        public override void FrameUpdate() {
            base.FrameUpdate();
        }

        public override void PhysicUpdate() {
            base.PhysicUpdate();
            if(canPetrol == true) {
                StartPetrolMovement();
            }
        }
        public override void ExitState() {
            base.ExitState();
        }

        public override void StateChangeCheaker() {
            base.StateChangeCheaker();
            if(enemy.hasAggroed) {                 // Entering Detect Range
                enemyStateMachine.ChangeState(enemy.ChaseState);
            }

        }

        #region Petrol Script

        void StartPetrolMovement() {
            // Petrol Movement
            if(enemy.enemyAnimCont._facingRight) {
                enemy.enemyController.EnemyMoveDirectionCont(Vector2.right, petrolSpeed);
            } else {
                enemy.enemyController.EnemyMoveDirectionCont(Vector2.left, petrolSpeed);
            }

        }

        #endregion
    }
}