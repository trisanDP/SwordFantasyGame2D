using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
