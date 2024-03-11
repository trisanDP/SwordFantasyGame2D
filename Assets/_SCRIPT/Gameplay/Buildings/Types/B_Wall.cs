using UnityEngine;
using OriginL;

namespace OriginL.Building
{
    public class B_Wall : BuildingBase {

        private void Start() {
            animator = GetComponent<Animator>();
            animator.SetTrigger("Building");
        }
        protected override void Update() {
            base.Update();
        }

        public override void OnTriggered() {
            animator.SetTrigger("Destroy");
        }

        public void TakeDamage(float damageAmount, int knockBackF, GameObject damageFrom) {
            Debug.Log(damageAmount);
        }
    }
}
