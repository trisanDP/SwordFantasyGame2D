
using System.Collections.Generic;
using UnityEngine;

namespace NPCSystem {
    [CreateAssetMenu(fileName = "NPCDatabase", menuName = "NPC/NPC Database")]
    public class NPCDatabase : ScriptableObject {
        // A list that stores all registered NPCs
        public List<NPCData> npcDataList = new List<NPCData>();
        public List<NPC> ActiveNPCLists;
        public int creditAmount;

        // Method to check if the NPC is already registered

        public bool IsNPCDataRegistered(NPCData npc) {
            return npcDataList.Contains(npc);
        }

        public bool IsActiveNPCRegistered(NPC npc) {
            return ActiveNPCLists.Contains(npc);
        }

        // Method to register a new NPC
        public void RegisterNPCData(NPCData npc) {
            if(!IsNPCDataRegistered(npc)) {
                npcDataList.Add(npc);
                /*            Debug.Log($"{npc.npcName} has been added to the NPC Database.");*/
            } else {
                /*            Debug.LogWarning($"{npc.npcName} is already registered.");*/
            }
        }

        public void RegisterActiveNPCs(NPC npc) {
            if(!IsActiveNPCRegistered(npc)) {
                ActiveNPCLists.Add(npc);
                /*            Debug.Log($"{npc.name} has been added to the NPC Database.");*/
            } else {
                /*            Debug.LogWarning($"{npc.name} is already registered.");*/
            }
            RegisterNPCData(npc.npcData);
        }
    }
}