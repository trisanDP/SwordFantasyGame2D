using System.Collections;
using UnityEngine;

namespace OriginL.EnemySpace {

    public class EnemyAI : MonoBehaviour{

        protected Enemy enemyScript;

        #region Variables
        [Header("DerivedClass")]
        protected bool canMove;
        protected Rigidbody2D rb;

        [Header("Petrol")]
        [SerializeField] protected float idelDuration = 1;


        #endregion
        #region New
        /*
                internal virtual void StartPetrol() {
                    StartCoroutine(Petrol());
                }

                protected virtual IEnumerator Petrol() { // Petrol And Idel 
                    canMove = true;
                    enemyScript.enemyController.activeSpeed = 0;
                    rb.velocity = Vector3.zero;
                    yield return new WaitForSeconds(idelDuration);
                    enemyScript.enemyController.activeSpeed = enemyScript.enemyController.petrolSpeed;
                    enemyScript.enemyAnimCont.Flip();
                    canMove = false;
                }
        */
        private void Start() {
            enemyScript = GetComponent<Enemy>();
        }

        #region ShareAbles:
        internal void LookAt(GameObject target)  // Shouldnt be at any state cause, i maight make a enemy that looks at player not doing anything
        {   // Looks towards player When ever called
            Debug.Log("Testing111");
            Debug.Log(target.name);
            if((target.transform.position.x > transform.position.x && !enemyScript.enemyAnimCont._facingRight) ||
                (target.transform.position.x < transform.position.x && enemyScript.enemyAnimCont._facingRight)) {
                enemyScript.enemyAnimCont.Flip();
            }
        }
        #endregion



        #endregion
    }
}