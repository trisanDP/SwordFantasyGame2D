using System;
using UnityEngine;
using OriginL;
using System.Resources;
using OriginL.Player;
using BrokenLands;

public abstract class BuildingBase : MonoBehaviour, IDamageable, IIntractable, IEnergyConsumer {

    #region Variables

    [Header("BasicComponent")]
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Sprite sprite;
    protected Collider2D col;
    protected GameManager gameManager;


    [Header("Building_Component")]
    public string BuildingName;
    [SerializeField] protected string message;
    [SerializeField] protected float MaxHealth;
    protected float Health;
    [SerializeField] protected int energyCost;
    [SerializeField] protected bool isDestroyed;
    public ResourceCost[] buildCost;


    [Header("DetectionVar")]
    [SerializeField] protected float range;
    protected LayerMask playerLayer;
    protected GameResourceManager resourceManager;
    GameAssets gameAssets;

    #region BuildingStage


        [Header("State")]
        [Range(0, 3)]
        public int stateLimit;
        public enum State {
            Node, Build1, Build2, Build3
        }
        public State activeStage;

        Sprite DefaultSprite;
        protected Sprite mode1Sprite;
        protected Sprite mode2Sprite;
        protected Sprite mode3Sprite;
        #endregion

    #endregion

    // Functions

    #region DefaultFunctions
   
    protected virtual void Awake() {
        gameManager = GameManager.Instance;
         resourceManager = FindFirstObjectByType<GameResourceManager>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
        /*            playerLayer = LayerMask.NameToLayer("Player");*/
        GetComponent<Collider2D>().isTrigger = true;
        SetStage(State.Node);
        gameAssets = GameAssets.i;
        SetUp();
    }


    protected virtual void Start() {
        SetSprite();
        if(resourceManager == null) {
            Debug.LogError("ResourceManager not found in the scene.");
        }
    }

    protected virtual void Update() {
        Collider2D colArr = Physics2D.OverlapCircle(transform.position, range, playerLayer);
        if(activeStage == State.Node) {
            if(colArr != null) {
                ShowPreviewBuilding();
            } else { // Preview Off
                spriteRenderer.sprite = null;
            }
        } else { // Reset Opacity from Preview
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        }
    }

    void SetUp() {
        int PlayerLayerIndex = LayerMask.NameToLayer("Player");
        playerLayer = 1 << PlayerLayerIndex;
    }
    #endregion

    #region StageSelector

        protected virtual void UpgradeStage() {
            if(activeStage < (State)(Enum.GetValues(typeof(State)).Length - 1) && stateLimit > (int)activeStage) {
                SetStage(activeStage + 1);
            } else {
                Debug.Log("Fully Upgraded");
            }

        }

        protected virtual void SetStage(State active) {
            activeStage = active;
            switch(activeStage) {
                case State.Node:
                Stage_Node();
                break;
                case State.Build1:
                Stage_Build1();

                break;
                case State.Build2:
                Stage_Build2();
                break;
                case State.Build3:
                Stage_Build3();

                break;

            }
        }

        #endregion

    #region StagesFunctions_Virtual

        protected virtual void Stage_Node() {
            GameManager.Instance.DebugMessage("Node", GameManager.MessageField.Others);


        }
        protected virtual void Stage_Build1() {
            GameManager.Instance.DebugMessage("Build1", GameManager.MessageField.Others);
            spriteRenderer.sprite = mode2Sprite;
        }

        protected virtual void Stage_Build2() {
            GameManager.Instance.DebugMessage("Build2", GameManager.MessageField.Others);
        }

        protected virtual void Stage_Build3() {
            GameManager.Instance.DebugMessage("Build3", GameManager.MessageField.Others);
        }
        #endregion

    #region Abstract
        public abstract void SetSprite();
        #endregion

    #region Interface Functions

    #region Interactable
    public virtual void OnIntract() {
        if(CanAfford()) {
            UpgradeStage(); 
            DeductCost();
            gameManager.playerObj.GetComponent<PlayerStat>().Energy.DecreaseStat(EnergyCost());
        }
    }

    public string Message() {
        return message + "" + BuildingName;
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public int EnergyCost() {
        return energyCost;
    }
    #endregion

    #region Damageable
        public virtual void TakeDamage(float damageAmount, int knockBackF, GameObject damageFrom) {
            Health -= damageAmount;
            if(Health <= 0) {
                DestroyGameObj();
            }
        }

        void DestroyGameObj() {
            gameObject.SetActive(false);
        }
        #endregion

    #endregion

    #region QualityOfLIfe
        void ShowPreviewBuilding() {
            spriteRenderer.sprite = mode1Sprite;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        }

        #endregion


    #region ResourceCost
    public bool CanAfford() {
        foreach(var cost in buildCost) {
            if(resourceManager.GetResourceAmount(cost.resourceName) < cost.amountRequired) {
                Debug.Log($"Not enough {cost.resourceName}");
                StartCoroutine(GameUI.instance.DisplayMessage("Cannot Afford", 1));
                return false;
            } else if(gameManager.playerObj.GetComponent<PlayerScript>().playerStat.Energy.GetValue() < EnergyCost()) {
                StartCoroutine(GameUI.instance.DisplayMessage("Not Enough Energy", 1));
                return false;

            }
        }
        return true;

    }

    // Deduct the resources after building the miner
    public void DeductCost() {
        foreach(var cost in buildCost) {
            resourceManager.SubtractResource(cost.resourceName, cost.amountRequired);
        }
    }


    #endregion
    

    #region Gizmos
        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, range);
        }


        #endregion
}



[Serializable]
public class ResourceCost {
    public string resourceName;
    public int amountRequired;
}

