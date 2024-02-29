using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected Enemy enemyScript;

    #region Variables
    [Header("DerivedClass")]
    internal float distFromTarget;
    protected bool canMove;
    protected Rigidbody2D rb;
    GameObject player;
    [Header("Petrol")]
    [SerializeField] protected float idelDuration = 1;


    public float x = 1;
    #endregion

    protected virtual void Start()
    {
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.playerObj;
    }

    protected virtual void Update()
    {
        distFromTarget = Vector2.Distance(player.transform.position, transform.position);
    }

    private void FixedUpdate() {
         /*   StateManager();*/
        
    }

    /*
     protected virtual void StateManager() {

         switch(enemyScript.ActiveState) {
             case EnemyScript.State.Ideal_State:
             enemyScript.enemyController.StartPetrolMovement();
             if(enemyScript.enemyCollider.HasHitWall() && !canMove) {
                 StartCoroutine(Petrol());
                 return;
             }
             break;
             case EnemyScript.State.Chasing_State:
             enemyScript.enemyController.StartChasing(enemyScript.Target);
             break;

             case EnemyScript.State.Attacking_State:
             enemyScript.enemyController.AttackingState();
             break;

             case EnemyScript.State.Stund_State:
             enemyScript.enemyController.StundState();

             break;

         }
     }*/
    #region New
    internal virtual void StartPetrol() { 
        StartCoroutine(Petrol());
    }

    internal float DistanceFromTarget() {
        if(enemyScript.enemyCollider.InDetectRange()) {
            return Vector2.Distance(player.transform.position, transform.position);
        } else
            return 1000;
    }
    protected virtual IEnumerator Petrol()
    { // Petrol And Idel 
        canMove = true;
        enemyScript.enemyController.activeSpeed = 0;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(idelDuration);
        enemyScript.enemyController.activeSpeed = enemyScript.enemyController.petrolSpeed;
        enemyScript.enemyAnimCont.Flip();
        canMove = false;
    }


    #region ShareAbles:
    internal void  LookAt(GameObject target)  // Shouldnt be at any state cause, i maight make a enemy that looks at player not doing anything
    {   // Looks towards player When ever called
        if((target.transform.position.x > transform.position.x && !enemyScript.enemyAnimCont._facingRight) ||
            (target.transform.position.x < transform.position.x && enemyScript.enemyAnimCont._facingRight)) {
            enemyScript.enemyAnimCont.Flip();
        }
    }


    #endregion



    #endregion
}
