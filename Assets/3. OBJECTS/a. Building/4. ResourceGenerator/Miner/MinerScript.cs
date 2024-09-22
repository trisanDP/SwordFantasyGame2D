using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL.Building {
    public class MinerScript : BuildingBase {

        public string resourceName;  // Name of the resource the miner will mine


        private float miningTimer;

        public int miningAmount;
        public float miningRate = 1f;

        protected override void Awake() {
            base.Awake();
        }
        protected override void Start() {
            base.Start();
        }

        public override void SetSprite() {
            mode1Sprite = GameAssets.i.Miner1;
            mode2Sprite = GameAssets.i.Miner2;
            mode3Sprite = GameAssets.i.Miner3;
        }

        protected override void Update() {
            base.Update();
            if(resourceManager != null && activeStage != State.Node) {
                miningTimer += Time.deltaTime;
                if(miningTimer >= miningRate) {
                    resourceManager.AddResource(resourceName, miningAmount); // Adjust the amount as needed
                    miningTimer = 0f;
                    Debug.Log("Mining");
                }
            }
        }


        protected override void Stage_Node() {
            base.Stage_Node();
        }

        protected override void Stage_Build1() {
            base.Stage_Build1();
            miningAmount = 1;
        }

        protected override void Stage_Build2() {
            base.Stage_Build2();
            miningAmount = 10;
        }

        protected override void Stage_Build3() {
            base.Stage_Build3();
        }
    }
}
