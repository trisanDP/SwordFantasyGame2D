using UnityEngine;

namespace OriginL.Building
{
    public abstract class BuildingBase : MonoBehaviour, IDamageable {
        #region Variables

        [Header("Node_Component")]
        public GameObject node;

        [Header("BasicComponent")]
        protected Animator animator;

        [Header("Building_Component")]
        public string BuildingName;

        [Header("DetectionVar")]
        [SerializeField]protected LayerMask player;
        [SerializeField]protected float range;
        #endregion

        private void Start() {

        }

        protected virtual void Update() {
            Collider2D colArr = Physics2D.OverlapCircle(transform.position, range, player); // To find All Intractable Objects in Range
            if(colArr != null) {
                ShowPreviewBuilding();
                animator.SetTrigger("Destroy");
            }

        }

        #region Basic
        void DestroyGameObj() {
            node.SetActive(true);
            Destroy(gameObject);
        }

        #endregion

        public abstract void OnTriggered();

        #region QualityOfLIfe
        void ShowPreviewBuilding() {

        }
        #endregion

        #region Gizmos
        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        public virtual void TakeDamage(float damageAmount, int knockBackF, GameObject damageFrom) {

        }


        #endregion
    }
}
