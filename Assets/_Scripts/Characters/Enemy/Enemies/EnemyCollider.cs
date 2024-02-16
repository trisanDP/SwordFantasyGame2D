using System.Collections;
using UnityEngine;

public class EnemyCollider : MonoBehaviour {

    [Header("Internal")]
    internal EnemyScript enemyScript;


    [SerializeField] GameObject hitPos; 
    [SerializeField] LayerMask hitLayer;
    [SerializeField] float attackSize;



    [Header("Range")]
    [SerializeField] internal float detectRange = 10;
    [SerializeField] internal float attackRange = 4;

    #region Variable


    [Header("Components")]
    [SerializeField] internal GameObject detectPoint;
    [SerializeField] internal LayerMask platformLayor;

    [Header("Vectors")]
    [SerializeField] Vector3 size;



    #endregion 

    private void Start() {
        if(hitLayer == 0 || platformLayor == 0)
        {
            Debug.LogError("EnemyCollider/LayerMask not set");
        }

        enemyScript = GetComponent<EnemyScript>();
    }

    #region Collision

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform")) {
            enemyScript.isGrounded = true;
        }
    }

    #endregion

    #region Trigger

    #endregion

    internal bool HasHitWall() {
        if (Physics2D.OverlapBox(detectPoint.transform.position, size, 0, platformLayor)) {
            return true;
        }
        else 
            return false;
    }



    internal Collider2D[] ColInRange() {  // returns damageables object collider
        Collider2D[] hit = Physics2D.OverlapCircleAll(hitPos.transform.position, attackSize, hitLayer);
        return hit;
    }

    #region Extra And Gizmos
    private void OnDrawGizmos() {
        //Detection Box
        Gizmos.color = Color.yellow; 
        Gizmos.DrawWireCube(detectPoint.transform.position, size);


        //Attack Box
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitPos.transform.position, attackSize);


        //Attack Range
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        //Detect Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    #endregion
}
