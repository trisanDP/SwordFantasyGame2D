using OriginL;
using UnityEngine;

namespace BrokenLands {
    public class ResourceGiverItem : MonoBehaviour, IIntractable {
        [Header("Resource Settings")]
        [SerializeField] private string message;
        [SerializeField] private string itemName; // Changed from 'Name' to avoid conflict with `Object.name`.
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int amount;

        [Header("References")]
        [SerializeField] private Collider2D col;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private InventoryManager inventoryManager;

        #region Unity Lifecycle

        private void Awake() {
            if(string.IsNullOrEmpty(itemName)) {
                Debug.LogError($"Please assign a name for {gameObject.name}", this);
            }

            // Assign components if not already set
            col ??= GetComponent<Collider2D>();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
        }

        private void Start() {
            // Assign InventoryManager instance if not set
            if(inventoryManager == null) {
                inventoryManager = InventoryManager.instance;
            }

            // Initialize sprite renderer properties
            InitializeSpriteRenderer();
        }

        private void OnValidate() {
            // Editor-only validations and assignments
            gameObject.name = itemName;

            // Safely assign sprite renderer for preview
            if(spriteRenderer == null) {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            if(resourceType != null && spriteRenderer != null) {
                spriteRenderer.sprite = resourceType.resourceIcon;
            }
        }

        #endregion

        #region Interface Implementation

        public GameObject GetGameObject() => gameObject;

        public string Message() => message + itemName;

        public void OnIntract() {
            if(inventoryManager == null) {
                Debug.LogError("InventoryManager is not set!", this);
                return;
            }

            inventoryManager.AddResource(resourceType, amount);
            Destroy(gameObject);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initializes the sprite renderer's properties.
        /// </summary>
        private void InitializeSpriteRenderer() {
            if(spriteRenderer == null) {
                Debug.LogError($"SpriteRenderer missing on {gameObject.name}", this);
                return;
            }

            if(resourceType != null) {
                spriteRenderer.sprite = resourceType.resourceIcon;
            }

            spriteRenderer.sortingLayerName = "ForGround";
        }

        #endregion
    }
}
