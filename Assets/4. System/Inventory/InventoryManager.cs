using System.Collections.Generic;
using UnityEngine;
using OriginL.Item;
using System.Dynamic;
using OriginL.Inventory;
using Mono.Cecil;
using Codice.Client.Commands.Matcher;

namespace OriginL {
    public class InventoryManager : MonoBehaviour {

        public List<ItemClass> items = new();
        public delegate void OnItemChange();
        public OnItemChange itemChangeCallBack;
        public delegate void OnResourceAdd();
        public OnResourceAdd OnResourceAddCallBack;
        public int space = 20;

        public Storage storage = new();


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


        #region AddAndRemove
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

        public void AddResource(ResourceType resourceType, int amount) {
            Dictionary<ResourceType, int> deliverableResources = new();
            deliverableResources.Add(resourceType, amount);
            storage.AddResources(deliverableResources);
            Debug.Log(resourceType + " " + amount);
            OnResourceAddCallBack?.Invoke();
        }

        public void SubtractResource(ResourceType resource, int amountToSubtract) {
            if(resource != null) {
                storage.RemoveResource(resource, amountToSubtract);
                OnResourceAddCallBack?.Invoke(); // Notify listeners
            } else {
                Debug.LogWarning($"Resource '{resource}' not found.");
            }
        }



        public void RemoveInventoryItem(ItemClass item) {
            items.Remove(item);
            itemChangeCallBack?.Invoke();
        }
        #endregion
    }

}
