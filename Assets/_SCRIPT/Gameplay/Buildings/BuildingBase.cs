using UnityEngine;

namespace OriginL.Building
{
    public abstract class BuildingBase : MonoBehaviour
    {
        #region Variables
        public Sprite Icon;
        public string BuildingName;

        public Animator animator;
        public GameObject Building;

        public LayerMask player;

        public GameObject node;

        public float range;
        #endregion

        private void Start() {
            
        }

        protected virtual void Update() {
            Collider2D colArr = Physics2D.OverlapCircle(transform.position, range, player); // To find All Intractable Objects in Range
            if(colArr != null ) {
                ShowPreviewBuilding();
                animator.SetTrigger("Destroy");
            }

        }
        void DestroyGameObj() {
            node.SetActive(true);
            Destroy(gameObject);
        }

        void ShowPreviewBuilding() {
            
        }
        public abstract void OnTriggered();

        #region Gizmos
        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, range);
        }
        #endregion
    }
}
