using UnityEngine;
using TMPro;

namespace OriginL {
    public class GameResourceManager : MonoBehaviour {
        public Resource[] resources;

        // UI Elements

        public TextMeshProUGUI woodAmountText;
        public TextMeshProUGUI metalAmountText;
        public TextMeshProUGUI stoneAmountText;
        public TextMeshProUGUI componentsAmountText;

        private void Start() {
            UpdateUI();

        }

        public void AddResource(string resourceName, int amountToAdd) {
            Resource resource = GetResourceByName(resourceName);
            if(resource != null) {
                resource.amount += amountToAdd;
                UpdateUI();
            } else {
                Debug.LogWarning($"Resource '{resourceName}' not found.");
            }
        }

        public void SubtractResource(string resourceName, int amountToSubtract) {
            Resource resource = GetResourceByName(resourceName);
            if(resource != null) {
                resource.amount = Mathf.Max(resource.amount - amountToSubtract, 0);
                UpdateUI();
            } else {
                Debug.LogWarning($"Resource '{resourceName}' not found.");
            }
        }

        private Resource GetResourceByName(string resourceName) {
            foreach(var resource in resources) {
                if(resource.resourceName == resourceName) {
                    return resource;
                }
            }
            return null;
        }

        private void UpdateUI() {
            if(woodAmountText != null)
                woodAmountText.text = $"Wood: {GetResourceAmount("Wood")}";
            if(metalAmountText != null)
                metalAmountText.text = $"Metal: {GetResourceAmount("Metal")}";
            if(stoneAmountText != null)
                stoneAmountText.text = $"Stone: {GetResourceAmount("Stone")}";
            if(componentsAmountText != null)
                componentsAmountText.text = $"Components: {GetResourceAmount("Components")}";
        }

        public int GetResourceAmount(string resourceName) {
            Resource resource = GetResourceByName(resourceName);
            return resource != null ? resource.amount : 0;
        }
    }
}