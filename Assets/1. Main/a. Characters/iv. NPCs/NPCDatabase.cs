using System.Collections.Generic;
using UnityEngine;

public class NPCDatabase : ScriptableObject {
    // A list that stores all registered NPCs
    public List<NPCData> npcList = new List<NPCData>();

    // Method to check if the NPC is already registered
    public bool IsNPCRegistered(NPCData npc) {
        return npcList.Contains(npc);
    }

    // Method to register a new NPC
    public void RegisterNPC(NPCData npc) {
        // Check if the npcName is empty
        if(string.IsNullOrEmpty(npc.npcName)) {
            Debug.LogWarning("NPC name is empty. Cannot register NPC.");
            return; // Exit early to prevent adding the NPC
        }

        if(!IsNPCRegistered(npc)) {
            npcList.Add(npc);
            Debug.Log($"{npc.npcName} has been added to the NPC Database.");

            // Ensure the NPC was added correctly
            if(npcList.Contains(npc)) {
                Debug.Log($"{npc.npcName} has been successfully added to the list.");
            } else {
                Debug.LogError($"Failed to add {npc.npcName} to the NPC Database.");
            }
        } else {
            Debug.LogWarning($"{npc.npcName} is already registered.");
        }
    }
}
