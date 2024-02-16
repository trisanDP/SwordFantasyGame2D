using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Chest : LootBox_Base
{
    #region Variables
    public string chest_name;
    public static int chestID;

    public enum ChestType
    {
        Normal,Silver,Gold
    }

    public ChestType type;
    #endregion

/*    void AssigneItems() {
        switch(type) {
                
        }
    }*/
    #region IntractableF
    protected override void Start() {
        base.Start();
        range = 5;
        chestID++;
    }


    #endregion

}
