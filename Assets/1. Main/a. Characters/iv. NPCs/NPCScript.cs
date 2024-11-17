using UnityEngine;

public class NPCScript : MonoBehaviour, IIntractable {

    [SerializeField] private NPCData npcData;
    [SerializeField] string message = "Hello Mate";

    private void Start() {

    }
    // Method to show NPC dialogue
/*    private void ShowDialogue() {
        // Example: Show the first dialogue line
        if(npcData.dialogues.Length > 0) {
            Debug.Log($"{npcData.npcName}: {npcData.dialogues[0]}");
            // Here you can implement your dialogue UI system
        } else {
            Debug.LogWarning($"{npcData.npcName} has no dialogues.");
        }
    }*/

    // Method to get the NPC's current friendship level
    public int GetFriendshipLevel() {
        return npcData.friendshipLevel;
    }

    public void OnIntract() {
        /*if(npcData != null) {
            ShowDialogue();
        } else {
            Debug.LogWarning("NPC Data not assigned for " + gameObject.name);
        }*/
    }


    public string Message() {
        return message;
    }

    public GameObject GetGameObject() {
        return gameObject;
    }


}
