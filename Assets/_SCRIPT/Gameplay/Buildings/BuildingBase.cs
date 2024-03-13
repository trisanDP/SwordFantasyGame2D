using System;
using UnityEngine;

namespace OriginL.Building
{
    public abstract class BuildingBase : MonoBehaviour, IDamageable, IIntractable {
        #region Variables

        [Header("BasicComponent")]
        [SerializeField] protected Animator animator;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Sprite sprite;
        protected Collider2D col;

        [Header("Building_Component")]
        public string BuildingName;
        [SerializeField] protected string message;

        public float Health { get; private set;}
        public float MaxHealth { get; private set;}

        [Header("DetectionVar")]
        [SerializeField]protected LayerMask player;
        [SerializeField]protected float range;
      

        #region BuildingStage
        public enum Stage {
            Node, Build1, Build2, Build3
        }
        [Header("Stage")]
        public Stage activeStage;

        #endregion
        #endregion

        // Functions

        #region DefaultFunctions

        private void Awake() {
            col = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            MaxHealth = 100;
            SetStage(Stage.Node);

            sprite = spriteRenderer.sprite;
            GetComponent<Collider2D>().isTrigger = true;

        }

        protected virtual void Update() {
            Collider2D colArr = Physics2D.OverlapCircle(transform.position, range, player); // To find All Intractable Objects in Range
            if(colArr != null) {
                ShowPreviewBuilding();
/*                animator.SetTrigger("Destroy");*/
            }

        }
        #endregion

        #region Basie

        void DestroyGameObj() {
/*            node.SetActive(true);*/
            Destroy(gameObject);
        }

        #endregion

        #region StageSelector
        protected virtual void SetStage(Stage active) {
            activeStage = active;
            
            switch(activeStage) {
                case Stage.Node:
                    GameManager.Instance.DebugMessage("Node",GameManager.MessageField.Others);
                break;
                case Stage.Build1:
                    GameManager.Instance.DebugMessage("Build1", GameManager.MessageField.Others);
                animator.SetTrigger("Build1");
                break;
                case Stage.Build2:
                    GameManager.Instance.DebugMessage("Build2", GameManager.MessageField.Others);

                break;
                case Stage.Build3:
                    GameManager.Instance.DebugMessage("Build3", GameManager.MessageField.Others);
                break;

            }
        }

        protected virtual void UpgradeStage() {
            if((int)activeStage < Enum.GetNames(typeof(Stage)).Length) 
                SetStage(activeStage + 1);
            else
                Debug.Log("Fully Upgraded");
        }
        #endregion

        #region Intractable
        public virtual void OnIntract() {
            UpgradeStage();
        }

        public string Message() {
            return message + "" + BuildingName;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }
        #endregion

        #region Damageable
        public virtual void TakeDamage(float damageAmount, int knockBackF, GameObject damageFrom) {
            Health -= damageAmount;
            if(Health <= 0) {
                DestroyGameObj();
            }
        }
        #endregion

        #region QualityOfLIfe
        void ShowPreviewBuilding() {

        }
        #endregion

        #region Gizmos
        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, range);
        }



        #endregion
    }
}
