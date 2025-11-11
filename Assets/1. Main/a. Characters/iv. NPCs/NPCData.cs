
using NPCSystem;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "NPC/New NPC")]
public class NPCData : ScriptableObject {
    public string npcName;                      // NPC name
    public Sprite portrait;                     // NPC portrait
    [TextArea(3, 10)] public string[] dialogues; // NPC dialogue lines
    private List<Quest> quests;                 // NPC quests
    public int friendshipLevel;                 // Friendship level of the NPC
    public int isUsed;

    public NPC conectedNPC;

    // Production and consumption rates of each resource
    public List<ResourceProductionData> resourceData = new List<ResourceProductionData>();

    // Resource storage
    public Storage storage = new(); // Ensure Storage is initialized

    // Reference to the NPC database
    [SerializeField] private NPCDatabase npcDatabase;

    // Flag to check if the NPC is already registered
    private bool isRegistered = false;

    public float deliveryTimeMultiplier = 1f; // To calculate delivery time
    public int currentMoral = 10;             // Moral ranges from 1 to 10

    // Automatically registers the NPC when necessary
/*    private void OnValidate() {
        if(!isRegistered) {
            RegisterIfNeeded();
        }

        // Initialize storage if not already initialized
        if(storage == null) {
            storage = new Storage(); // Initialize storage if it's null
        }

        // Initialize resources in storage from ResourceDatabase
        ResourceDatabase resourceDatabase = Resources.Load<ResourceDatabase>("ResourceDatabase");
        if(resourceDatabase != null) {
            storage.InitializeStorage(resourceDatabase); // Initialize storage with resources from the database
            InitializeResourceData(resourceDatabase);    // Initialize resource data for production/consumption
        } else {
            Debug.LogWarning("ResourceDatabase not found. Ensure it's placed in the Resources folder.");
        }
    }*/


    #region Register
    // Registers the NPC if it isn't registered and database is not null
    private void RegisterIfNeeded() {
        if(npcDatabase == null) {
            npcDatabase = Resources.Load<NPCDatabase>("NPCDatabase");
            if(npcDatabase == null) {
                Debug.LogError("NPCDatabase not found in Resources. Please place an NPCDatabase in the Resources folder.");
                return;
            }
        }

        RegisterToDatabase();
    }

    // Context menu option to manually register the NPC to the database
    [ContextMenu("Register NPC to Database")]
    public void RegisterToDatabase() {
        if(npcDatabase == null) {
            Debug.LogWarning("NPC Database is not assigned.");
            return;
        }

        if(npcDatabase.IsNPCDataRegistered(this)) {
            Debug.LogWarning($"{npcName} is already registered in the database.");
            isRegistered = true;
            return;
        }

        npcDatabase.RegisterNPCData(this);
        Debug.Log($"{npcName} successfully registered to the database.");
        isRegistered = true;
    }
    #endregion

    #region ResourceProduction_Functions

    // Method to get both production and consumption rate of a specific resource
    public ResourceProductionData GetResourceData(ResourceType type) {
        return resourceData.Find(resource => resource.resourceType == type);
    }

    public int GetProductionRate(ResourceType type) {
        ResourceProductionData data = GetResourceData(type);
        return data != null ? data.productionRate : 0;
    }

    public int GetConsumptionRate(ResourceType type) {
        ResourceProductionData data = GetResourceData(type);
        return data != null ? data.consumptionRate : 0;
    }
    #endregion

    public void InitializeResourceData(ResourceDatabase resourceDatabase) {
        if(resourceDatabase == null || resourceDatabase.allResources == null) {
            Debug.LogError("ResourceDatabase or its resources are null. Initialization failed.");
            return;
        }

        foreach(var resource in resourceDatabase.allResources) {
            // Check if the resource is already in the list
            var existingData = resourceData.Find(data => data.resourceType == resource);
            if(existingData == null) {
                // Add a new entry for this resource
                resourceData.Add(new ResourceProductionData { resourceType = resource });
            }
        }

        Debug.Log($"{npcName} resourceData initialized with {resourceData.Count} entries.");
    }
}

// Class to represent production/consumption data for a specific resource
[System.Serializable]
public class ResourceProductionData {
    public ResourceType resourceType;      // Type of the resource (ScriptableObject)
    public int productionRate = 0;         // Production rate per minute
    public int consumptionRate = 0;        // Consumption rate per day

/*    public void GenerateRandom() {
        // Generate a random production rate between 10 and 100 (inclusive)
        productionRate = Random.Range(10, 101);

        // Generate a random consumption rate that is less than the production rate
        consumptionRate = Random.Range(0, productionRate);

        Debug.Log($"Generated Random Rates for {resourceType?.resourceName ?? "Unnamed Resource"} - " +
                  $"Production: {productionRate}, Consumption: {consumptionRate}");
    }*/
}

