using UnityEngine;

namespace OriginL.Item {
    [CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/Misc")]
    public class MiscClass : BaseItemSO {
        #region Abstract 
        /* public override BaseItemSO GetItem() { return this; }
         public override ToolsClass GetTool() { return null; }
         public override ConsumableClass GetConsumable() { return null; }
         public override MiscClass GetMisc() { return this; }*/
        #endregion
    }
}