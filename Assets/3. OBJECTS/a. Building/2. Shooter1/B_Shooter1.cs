using OriginL.Building;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL
{
    public class B_Shooter1 : BuildingBase {


        protected override void Awake() {
            base.Awake();
            MaxHealth = 100;
            Health = MaxHealth;
        }

    }
}
