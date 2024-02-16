using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootDrop : LootBox_Base
{
    public string dropedFrom;

    protected override void Start() {
        base.Start();
        range = 3;
    }

    protected override void Update() {
        base.Update();
        if (ItemsRewards.Count == 0)
            Destroy(gameObject, 2);
    }

    private void OnDestroy() {
        CloseChest();
    }

}
