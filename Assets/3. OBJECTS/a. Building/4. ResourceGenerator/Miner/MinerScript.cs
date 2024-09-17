using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL.Building {
    public class MinerScript : BuildingBase {
        int generatedAmount = 0;
        bool onNode;

        public override void OnIntract() {
            base.OnIntract();
        }

        public override void SetSprite() {
            mode1Sprite = GameAssets.i.Miner1;
            mode2Sprite = GameAssets.i.Miner2;
            mode3Sprite = GameAssets.i.Miner3;
        }

        protected override void Awake() {
            base.Awake();
        }

        protected override void SetStage(Stage active) {
            base.SetStage(active);
        }

        protected override void Stage_Build1() {
            base.Stage_Build1();
        }

        protected override void Stage_Build2() {
            base.Stage_Build2();
        }

        protected override void Stage_Build3() {
            base.Stage_Build3();
        }

        protected override void Stage_Node() {
            base.Stage_Node();
        }

        protected override void Update() {
            base.Update();
        }

        protected override void UpgradeStage() {
            base.UpgradeStage();
        }
    }
}
