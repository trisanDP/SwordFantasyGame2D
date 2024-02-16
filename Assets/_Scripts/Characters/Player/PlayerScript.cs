using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    internal enum State {
        idel_State, Moving_State, Jumping_State, Attacking_State,Obj_Interaction
    };

    [SerializeField]internal State ActiveState;

    #region variables

    [Header("Scripts")]
    internal PlayerController_PC playerController;
    internal PlayerCollider playerCollider;
    internal PlayerInput playerInput;
    internal PlayerAnimationController p_animCont;
    internal PlayerStat playerHealth;
    internal PlayerStateController playerStateController;
    internal P_QuestManager playerQuestM;
    internal PlayerCombact playerCombact;
    internal PlayerIntract playerIntract;
    internal PlayerEquipmentManager playerEquipmentM;

    [Header("GameObjects")]
    internal Rigidbody2D Rb;

    public GameObject target;

    [SerializeField] internal bool isDead = false;
    #endregion

    protected void Awake() {
        ActiveState = State.idel_State;
        Rb = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<PlayerCollider>();
        playerController = GetComponent<PlayerController_PC>();
        playerInput = GetComponent<PlayerInput>();
        p_animCont = GetComponent<PlayerAnimationController>();
        playerHealth = GetComponent<PlayerStat>();
        playerQuestM = GetComponent<P_QuestManager>();
        playerCombact = GetComponent<PlayerCombact>();
        playerIntract = GetComponent<PlayerIntract>();
        playerEquipmentM = GetComponent <PlayerEquipmentManager>();


    }
    private void Start() {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null) {
            target = enemy;
        }
    }

    #region Extra Scrip

    #endregion
}
