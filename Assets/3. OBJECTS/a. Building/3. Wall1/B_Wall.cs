using UnityEngine;
using OriginL;

namespace OriginL.Building
{  
    public class B_Wall : BuildingBase {

        protected override void Awake() {
            base.Awake();
            MaxHealth = 100;
            Health = MaxHealth;
        }
        protected override void Stage_Node() {
            base.Stage_Node();
        }
        protected override void Stage_Build1() {
            base.Stage_Build1();
            animator.SetTrigger("Build1");

        }

        protected override void Stage_Build2() {
            base.Stage_Build2();
        }

        protected override void Stage_Build3() {
            base.Stage_Build3();
        }

        public override void SetSprite() {
            throw new System.NotImplementedException();
        }
    }
}
