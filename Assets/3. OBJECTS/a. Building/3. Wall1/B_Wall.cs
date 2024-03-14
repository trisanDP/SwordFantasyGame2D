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
    }
}
