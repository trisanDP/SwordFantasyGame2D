using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/Misc")]
public class MiscClass : ItemClass
{
    #region Abstract 
    /* public override ItemClass GetItem() { return this; }
     public override ToolsClass GetTool() { return null; }
     public override ConsumableClass GetConsumable() { return null; }
     public override MiscClass GetMisc() { return this; }*/
    #endregion
}
