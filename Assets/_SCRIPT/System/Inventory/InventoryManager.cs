using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OriginL.System {
    public class InventoryManager : MonoBehaviour {

        public List<ItemClass> items = new(); 
        public delegate void OnItemChange();
        public OnItemChange itemChangeCallBack;
        public int space = 20;

        #region Singleton
        public static InventoryManager instance;

        private void Awake() {
            if(instance != null) {
                Destroy(instance);
            } else {
                instance = this;
            }
        }
        #endregion

        public bool AddItem(ItemClass item) {
            if(!item.isDefault) {
                if(items.Count >= space) {
                    Debug.Log("Inventory Full");
                    return false;
                }
                items.Add(item);
                itemChangeCallBack?.Invoke();
            }
            return true;
        }

        public void RemoveInventoryItem(ItemClass item) {
            items.Remove(item);
            itemChangeCallBack.Invoke();
        }

    }

}
