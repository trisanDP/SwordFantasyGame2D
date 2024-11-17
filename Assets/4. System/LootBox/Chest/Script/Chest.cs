using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

namespace OriginL.ChestSpace {
    public class Chest : LootBox_Base {
        #region Variables
        public string chest_name;
        public static int chestID;

        public enum ChestType {
            Normal, Silver, Gold
        }

        public ChestType type;
        #endregion

        #region IntractableF
        protected override void Start() {
            base.Start();
            range = 5;
            chestID++;
        }

        public override string Message() {
            return message + "" + chest_name;
        }

        #endregion

    }
}