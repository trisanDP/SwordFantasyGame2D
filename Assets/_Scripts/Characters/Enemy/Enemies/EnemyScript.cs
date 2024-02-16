using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    #region Variables

    #region StatesVariables
    internal enum State
    {
        Ideal_State, Chasing_State, Attacking_State, Stund_State
    };

    [SerializeField] internal State ActiveState;
    #endregion

    #region ScriptVariables
    [Header("Scripts")]
    internal EnemyCollider enemyCollider;
    internal EnemyStat enemyStatus;
    internal EnemyController enemyController;
    internal EnemyAI enemyAI;
    internal EnemyAnimController enemyAnimCont;
    internal EnemyCombact enemyCombact;
    internal EnemyStateManager enemyStateManager;
    #endregion

    public EnemyType enemyType;

    #region Components
    [Header("GameObjects")]
    public GameObject dropBox; 
    internal GameObject Target;
    internal GameManager gameManager;
    internal Rigidbody2D rb;

    public List<ItemClass> drops;
    #endregion

    [Header("Collider and Physic")]
    internal bool isGrounded = true;

    #endregion


    private void Awake()
    {
        enemyCollider = GetComponent<EnemyCollider>();
        enemyStatus = GetComponent<EnemyStat>();
        enemyController = GetComponent<EnemyController>();
        enemyAI = GetComponent<EnemyAI>();
        enemyAnimCont = GetComponent<EnemyAnimController>();
        enemyCombact = GetComponent<EnemyCombact>();
        ActiveState = State.Ideal_State;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GetComponent<GameManager>();

        Target = GameObject.Find("Player");
    }



    public void OnDeath() { // Called in EnemyStat 
        enemyAnimCont.PlayDeathAnim();
    }


    public void DropItems() {  // Called in EnemyAnimation Script
        dropBox = Instantiate(dropBox, transform.position, Quaternion.identity);
        if (dropBox != null) {
            foreach (ItemClass item in drops) { // Loop through all items in Drops
                LootDrop itemDropsScript = dropBox.GetComponent<LootDrop>();
                if (itemDropsScript != null) {
                    itemDropsScript.ItemsRewards.Add(item);
                } else
                    Debug.Log("LootDrop Component in Dropbox in enemy Is Empty");
            }
        } else
            Debug.Log("DropBox Prefab Empty");
    }
}

public enum EnemyType
{
    Normal, Ghost, Heavy, Light
}