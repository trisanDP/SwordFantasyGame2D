using UnityEngine;

namespace OriginL.Item {

    [CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/Tool")]
    public class ToolsClass : ItemClass {


        public override void Use() {
            base.Use();
        }
        #region Function
        /*public override ItemClass GetItem() { return this; }

        public override ToolsClass GetTool() { return this; }

        public override ConsumableClass GetConsumable() { return null; }

        public override MiscClass GetMisc() { return null; }*/
        #endregion


    }
}