using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "NPC/New NPC")]
public class NPCData : ScriptableObject {
    [SerializeField] private string NPCname;

    public string npcName {
        get { return NPCname; }
        set {
            NPCname = value;
            UpdateName();
        }
    }
    public Sprite portrait;
    [TextArea(3,10)]
    public string[] dialogues;
    public List<Quest> quests;
    public int friendshipLevel;

    // Reference to the NPC Database
    [SerializeField] private NPCDatabase npcDatabase;

    // Flag to check if the NPC is already registered
    private bool isRegistered = false;
    private void UpdateName() {
        this.name = NPCname; // Set the name of the ScriptableObject to the string value.
    }

    // Automatically registers the NPC when necessary
    private void OnValidate() {
        UpdateName(); // Ensure the name updates in the editor
        // Only register if the NPC has not been registered yet
        if(!isRegistered && npcDatabase == null) {
            // Try to load the NPC Database from Resources if not assigned
            npcDatabase = Resources.Load<NPCDatabase>("NPCDatabase");

            if(npcDatabase == null) {
                Debug.LogError("NPCDatabase not found in Resources. Please place an NPCDatabase in the Resources folder.");
                return;
            }

            RegisterToDatabase();
        }
    }

    // Context menu option to manually register the NPC to the database
    [ContextMenu("Register NPC to Database")]
    public void RegisterToDatabase() {
        if(npcDatabase != null) {
            // Check if the NPC is already registered
            if(npcDatabase.IsNPCRegistered(this)) {
                Debug.LogWarning($"{npcName} is already registered in the database.");
                isRegistered = true; // Mark as registered
                return;
            }

            // Register the NPC if it is not already registered
            npcDatabase.RegisterNPC(this);
            Debug.Log($"{npcName} successfully registered to the database.");
            isRegistered = true; // Mark as registered
        } else {
            Debug.LogWarning("NPC Database is not assigned.");
        }
    }
}
