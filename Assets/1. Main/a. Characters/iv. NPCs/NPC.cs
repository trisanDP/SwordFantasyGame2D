using System.Collections.Generic;
using UnityEngine;

namespace NPCSystem {
    public class NPC : MonoBehaviour, IResourceEntity {
        /*[HideInInspector]*/ public NPCData npcData;
        /*[HideInInspector]*/ public NPCDatabase database;
        /*[HideInInspector]*/ public DeliveryManager deliveryManager;
        /*[HideInInspector]*/ public Hub hub;
        /*[HideInInspector]*/ public HubData hubData;


        #region Unity Lifecycle Methods

       
        private void Start() {

            deliveryManager = DeliveryManager.instance;
            database = Resources.Load<NPCDatabase>("NPCDatabase");
            hub = hub == null ? FindAnyObjectByType<Hub>() : hub;
            hubData = hub.hubData;

            if(npcData == null) {
                Debug.Log("NPCdata is null");
            }
            name = npcData.npcName;
            npcData.isUsed++;
            if(npcData.isUsed > 1) {
                Debug.Log($"{npcData.npcName} is being used {npcData.isUsed} times");
                Destroy(gameObject);
            }

            database.RegisterActiveNPCs(this);
        }

        private void OnValidate() {
            if(npcData != null) {
                name = npcData.npcName;
                npcData.conectedNPC = this;
            }
        }
        void OnEnable() {
            GameManager.Instance.timeManager.OnNightStarted += NPCBehaviourOverNight;
        }

        private void OnDestroy() {
            if(GameManager.Instance != null) {
                GameManager.Instance.timeManager.OnNightStarted -= NPCBehaviourOverNight;
            }

            if(npcData != null) {
                npcData.isUsed = 0;
            }
        }

        #endregion

        #region Resource Management Methods

        public void ProduceResources(Dictionary<ResourceType, int> resourcePackage) {
            foreach(var entry in resourcePackage) {
                ResourceType resource = entry.Key;
                int producedAmount = npcData.GetProductionRate(resource);

                npcData.storage.AddResources(new Dictionary<ResourceType, int> { { resource, producedAmount } });
            }
        }

        public void ConsumeResources(Dictionary<ResourceType, int> resourcePackage) {
            foreach(var entry in resourcePackage) {
                ResourceType resource = entry.Key;
                int consumedAmount = npcData.GetConsumptionRate(resource);

                npcData.storage.RemoveResource(resource, consumedAmount);
            }
        }

        public void ReceiveResources(Dictionary<ResourceType, int> resourcePackage, IResourceEntity from) {
            npcData.storage.AddResources(resourcePackage);
        }

        public void SendResources(Dictionary<ResourceType, int> resourcePackage, IResourceEntity to) {
            Dictionary<ResourceType, int> deliverableResources = new();

            foreach(var entry in resourcePackage) {
                ResourceType resource = entry.Key;
                int amount = entry.Value;

                int totalStoredAmount = npcData.storage.GetResourceAmount(resource);

                if(totalStoredAmount >= amount) {
                    deliverableResources.Add(resource, amount);
                } else {
                    Debug.Log($"Not enough {resource} to send to {to.GetEntityName()}. Required: {amount}, Available: {totalStoredAmount}");
                }
            }

            if(deliverableResources.Count > 0) {
                deliveryManager.DeliverResources(this, hub, deliverableResources, transform.position, hub.transform.position);

                foreach(var entry in deliverableResources) {
                    npcData.storage.RemoveResource(entry.Key, entry.Value);
                }
            } else {
                Debug.Log("No resources to deliver.");
            }
        }

        public string GetEntityName() {
            return npcData.npcName;
        }

        public Transform GetTransform() { return transform; }

        #endregion

        #region Request 

        public void ReceiveRequest(ResourceType resourceType, int amount) {
            Dictionary<ResourceType, int> resourcePackage = new() { { resourceType, amount } };
            SendResources(resourcePackage, hub);
        }

        #endregion

        #region NPC Behaviour Methods

        void NPCBehaviourOverNight() {
            Dictionary<ResourceType, int> producedResources = new();
            Dictionary<ResourceType, int> consumedResources = new();

            foreach(var entry in npcData.resourceData) {
                ResourceType resource = entry.resourceType;
                int producedAmount = npcData.GetProductionRate(resource);
                int consumedAmount = npcData.GetConsumptionRate(resource);

                producedResources.Add(resource, producedAmount);
                consumedResources.Add(resource, consumedAmount);
            }

            ProduceResources(producedResources);
            ConsumeResources(consumedResources);
            SendResourceBehaviour();
        }

        public void SendResourceBehaviour() {
            Dictionary<ResourceType, int> resourcePackage = new();

            foreach(var entry in npcData.resourceData) {
                ResourceType resource = entry.resourceType;
                int totalStoredAmount = npcData.storage.GetResourceAmount(resource);

                if(totalStoredAmount > 0) {
                    int amountToSend = CalculateRandomResourceAmountToSend(totalStoredAmount);
                    if(amountToSend > 0) {
                        resourcePackage.Add(resource, amountToSend);
                    }
                }
            }

            if(resourcePackage.Count > 0) {
                SendResources(resourcePackage, hub);
            }
        }

        #endregion

        #region Helper Methods

        private int CalculateRandomResourceAmountToSend(int totalStoredAmount) {
            int randomPercentage = Random.Range(10, 31);
            float percentage = randomPercentage / 100f;
            int calculatedAmount = Mathf.CeilToInt(totalStoredAmount * percentage);

            int amountFor10Percent = Mathf.FloorToInt(totalStoredAmount * 0.1f);
            int amountFor20Percent = Mathf.FloorToInt(totalStoredAmount * 0.2f);

            if(amountFor10Percent < 1 && amountFor20Percent < 1) {
                return 0;
            }

            return calculatedAmount >= 1 ? calculatedAmount : 0;
        }

        private void OnMouseDown() {
            if(UiManager.Instance == null) {
                Debug.LogError("UiManager instance is missing!");
                return;
            }

            UiManager.Instance.npcUiManager.ShowNPCUi(this, npcData);
        }

        #endregion
    }
}