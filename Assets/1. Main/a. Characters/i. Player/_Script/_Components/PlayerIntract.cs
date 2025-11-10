using OriginL.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BrokenLands {
    public class PlayerIntract : MonoBehaviour {

        #region Variables
        public float range;
        private PlayerScript player;


        #endregion

        #region MonoBehaviour Methods
        private void Awake() {

        }
        private void Start() {
            player = GetComponent<PlayerScript>();
            if(player == null) {
                Debug.LogWarning("PlayerScript not found on the GameObject!");
            }
        }
        #endregion

        #region Input
        public void PressedE(InputAction.CallbackContext context) {
            if(context.performed) {
                IIntractable intract = HasIntractObj();
                intract?.OnIntract();  // Only call if object is not null
            }
        }
        #endregion

        #region Intract Detection
        public IIntractable HasIntractObj() {
            Collider2D[] colArr = Physics2D.OverlapCircleAll(transform.position, range);
            IIntractable closest = null;
            float closestDistanceSqr = float.MaxValue; // Use squared distance to avoid square root calculations

            foreach(Collider2D col in colArr) {
                if(col.TryGetComponent(out IIntractable intract)) {
                    float distanceSqr = (col.transform.position - transform.position).sqrMagnitude;
                    if(distanceSqr < closestDistanceSqr) {
                        closest = intract;
                        closestDistanceSqr = distanceSqr;
                    }
                }
            }

            return closest;
        }
        #endregion

        #region Gizmos
#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
#endif
        #endregion
    }

}
