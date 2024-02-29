using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyScript enemyScript;


    [SerializeField]internal float distFromTarget;

    internal bool canMove;
    
    private void Start() {
        enemyScript = GetComponent<EnemyScript>();

    }

    private void Update() {/*
        distFromTarget = enemyScript.enemyAI.distFromTarget;

        switch (enemyScript.ActiveState) {

            case EnemyScript.State.Ideal_State:
            if (distFromTarget < detectR ) {                 // Entering Detect Range
                enemyScript.ActiveState = EnemyScript.State.Chasing_State;
            }
            break;
            case EnemyScript.State.Chasing_State:

            if (distFromTarget > detectR) {                 //Out of Detect range then start chasing again:
                enemyScript.ActiveState = EnemyScript.State.Ideal_State;
            }

            if (distFromTarget < attackR) {                 //On Entering Attacking Range
                enemyScript.ActiveState = EnemyScript.State.Attacking_State;
            }
            break;

            case EnemyScript.State.Attacking_State:
            if (distFromTarget > attackR) {
                enemyScript.ActiveState = EnemyScript.State.Chasing_State;
            }
            break;
        }*/

    }

}

