using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NPCSystem {
    public class NPCUiManager : MonoBehaviour {
        [Header("UI References")]
        public GameObject npcUiPannel;
        public Button Hub;
        public Button Close;
        public TextMeshProUGUI npcNameTxt;
        public Image npcPortraitImage;

        public List<TextMeshProUGUI> resourceStorageUI;
        public List<TextMeshProUGUI> produceRate;
        public List<TextMeshProUGUI> consumeRate;

        private NPC npc;
        private NPCData npcData;

        public void GetNPCDetail(NPC npc, NPCData npcData) {
            this.npc = npc;
            this.npcData = npcData;

            npcNameTxt.text = npcData.npcName; // Ensure it's npcName instead of name
            /*npcPortraitImage.sprite = npcData.portrait; // Update portrait*/

            UpdateResourceUI();
            UpdateStorageUI();
        }

        private void UpdateResourceUI() {
            for(int i = 0; i < npcData.resourceData.Count; i++) {
                if(i < produceRate.Count && i < consumeRate.Count) {
                    ResourceProductionData resourceData = npcData.resourceData[i];
                    produceRate[i].text = resourceData.productionRate.ToString();
                    consumeRate[i].text = resourceData.consumptionRate.ToString();
                }
            }
        }

        public void UpdateStorageUI() {
            if(npcData == null || resourceStorageUI == null) {
                Debug.LogWarning("NPC Data or Resource Storage UI is not assigned.");
                return;
            }

            for(int i = 0; i < resourceStorageUI.Count; i++) {
                if(i < npcData.resourceData.Count) {
                    var resource = npcData.resourceData[i]?.resourceType;

                    if(resource == null) {
                        Debug.LogWarning($"Resource at index {i} is null in NPC Data.");
                        resourceStorageUI[i].text = "0"; // Default display for null resources
                        continue;
                    }

                    int storedAmount = npcData.storage.GetResourceAmount(resource);
                    resourceStorageUI[i].text = storedAmount.ToString();
                } else {
                    resourceStorageUI[i].text = "0"; // Clear excess UI elements
                }
            }
        }



        public void SendResourceButton() {  // Temp
            if(npc != null) {
                npc.SendResourceBehaviour();
            } else {
                Debug.LogWarning("No NPC selected to send resources.");
            }
        }

        public void ShowNPCUi(NPC npc, NPCData npcData) {
            GetNPCDetail(npc, npcData);
            Show();
        }

        public void OnHubButton() {
            UiManager.Instance.hubUiManager.ShowHubUi();
            Hide();
        }

        public void Show() {
            npcUiPannel.SetActive(true);
        }

        public void Hide() {
            npcUiPannel.SetActive(false);
        }
    }
}