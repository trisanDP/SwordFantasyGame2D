using UnityEngine;
using OriginL.System;

public abstract class Intractable : MonoBehaviour
{
    #region IIntractable

    protected InventoryManager inventory;
    [SerializeField]public string Message;

    private void Awake()
    {
        inventory = InventoryManager.instance;
        if(Message == null)
            Debug.LogWarning("Null Message");
    }

    public abstract void OnEntract();
    #endregion
}
