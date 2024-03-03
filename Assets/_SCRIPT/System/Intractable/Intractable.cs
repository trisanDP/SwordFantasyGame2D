using UnityEngine;
using OriginL.System;

public abstract class Intractable : MonoBehaviour
{
    #region IIntractable

    protected InventoryManager inventory;
    private void Awake()
    {
        inventory = InventoryManager.instance;
    }
    public abstract void OnEntract();
    #endregion
}
