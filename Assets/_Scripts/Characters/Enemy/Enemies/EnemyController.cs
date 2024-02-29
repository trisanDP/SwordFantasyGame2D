using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


public class EnemyController : MonoBehaviour
{

    internal Enemy enemyScrip;

    #region Variables
    [Header("Values")]
    [SerializeField] internal float jumpForce;
    [SerializeField] internal float ChasingSpeed = 200;
    [SerializeField] internal float activeSpeed;
    [SerializeField] internal float petrolSpeed = 150;
    internal bool canAttack = true;

    #endregion

    private void Start()
    {
        enemyScrip = GetComponent<Enemy>();
        petrolSpeed = Random.Range(petrolSpeed * 1f, petrolSpeed);
        ChasingSpeed = Random.Range(ChasingSpeed * 0.85f, ChasingSpeed);
        activeSpeed = petrolSpeed;
    }

    internal void StundState() {
        if (enemyScrip.enemyStatus.isStund == false) {
            enemyScrip.enemyStatus.isStund = true;
        }
    }

    internal virtual void StartPetrolMovement(){
        activeSpeed = petrolSpeed;
        // Petrol Movement
        if (enemyScrip.enemyAnimCont._facingRight)
        {
            enemyScrip.rb.AddForce(Vector2.right * activeSpeed, ForceMode2D.Force);
        } else
        {
            enemyScrip.rb.AddForce(Vector2.left * activeSpeed, ForceMode2D.Force);
        }

    }
    internal void EnemyMoveController(GameObject obj, float activeSpeed) {
        enemyScrip.enemyAI.LookAt(obj);
        enemyScrip.rb.AddForce(((obj.transform.position) - enemyScrip.transform.position).normalized * activeSpeed, ForceMode2D.Force);
    }

    internal void StopEnemyMovement() {
        if(enemyScrip.rb.velocity.x != 0) {
            enemyScrip.rb.velocity = new Vector2(0,enemyScrip.rb.velocity.y);

        }
    }

}
