using UnityEngine;

public class EnemyState {
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;
    protected float distFromTarget;
    protected GameObject target = null;

    [Header("Values")]
    [SerializeField] internal float jumpForce;
    [SerializeField] internal float ChasingSpeed;
    [SerializeField] internal float petrolSpeed;

    [Header("Ranges")]
    protected float attackR;
    protected float detectR;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine){
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        

    }
   

    public virtual void EnterState() {
        attackR = enemy.enemyCollider.attackRange;
        detectR = enemy.enemyCollider.detectRange;
        jumpForce = enemy.enemyController.jumpForce;
        ChasingSpeed = enemy.enemyController.ChasingSpeed;
        petrolSpeed = enemy.enemyController.petrolSpeed;
        target = enemy.Target;
    }

    public virtual void FrameUpdate() {
        distFromTarget = enemy.enemyAI.distFromTarget;

        if(enemy.enemyStatus.currentHealth <= 0) {
            enemyStateMachine.ChangeState(enemy.DeadState);
            Debug.Log("Change to death");
        }
        

        StateChangeCheaker();
    }

    public virtual void PhysicUpdate() { }
    public virtual void Triggers() { }

    public virtual void ExitState() { }

    public virtual void StateChangeCheaker() {

    }

    public void ChangeStateForEffect() {
        
    }
}
