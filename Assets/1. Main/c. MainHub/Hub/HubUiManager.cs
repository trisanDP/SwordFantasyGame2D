using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NPCSystem;

public class HubUiManager : MonoBehaviour {
    [Header("Button")]
    public GameObject npcButtonPrefab;      // Prefa
                                            // b for the NPC button
    [Header("HubManager")]
    public GameObject RequestResourcePanel;
    public Transform npcListContent;        // The content holder for the ScrollView (where buttons will be added)
    public List<TextMeshProUGUI> hubStorageTxt;


    [Header("Request")]
    public List<TMP_InputField> resourceInputField;
    /*public List<ResourceType> resourceTypes;*/
    ResourceDatabase resourceDB;
    NPC activeNPC;

    [Header("Delivery")]
    public GameObject deliveryUIPanel;
    public DeliveryUIManager deliveryUIManager;


    [Header("Communication")]
    public GameObject commWindow;
    public GameObject commScreen;
    public TextMeshProUGUI npcNameTxtComm;
    public Transform npcListContentComm;        // The content holder for the ScrollView (where buttons will be added)

    public GameObject hubUiPannel;

    private GameManager gameManager;
    
    private Hub hub;
    private HubData hubData;

    private void Start() {
        gameManager = GameManager.Instance;
        deliveryUIManager = deliveryUIPanel.GetComponent<DeliveryUIManager>();
        hub = gameManager.hub;
        hubData = Resources.Load<HubData>("HubDatabase");
        resourceDB = Resources.Load<ResourceDatabase>("ResourceDatabase");
        HideAllPanel();
    }

    public void Initialize() {
        UpdateStorageUI();
        PopulateNPCList();
    }
    public void HideAllPanel() {
        commScreen.SetActive(false);
        commWindow.SetActive(false);
        deliveryUIPanel.SetActive(false);
    }

    // Show Hub UI and manage active panel


    #region HubManagement_Window
    // Populate the ScrollView with NPC buttons
    public void PopulateNPCList() {
        // Clear any existing NPC buttons
        List<NPC> npcs = new(gameManager.GetActiveNPCList());
        foreach(Transform child in npcListContent) {
            Destroy(child.gameObject);
        }

        // Iterate through NPCs from the GameManager's npcDataList and create a button for each NPC
        foreach(NPC npc in npcs) {
            if(!gameManager) {
                Debug.Log("No GM");
            }
            GameObject newButton = Instantiate(npcButtonPrefab, npcListContent);
            if(npc == null) { Debug.Log("No NPCs"); } 

            if(newButton.GetComponentInChildren<TextMeshProUGUI>() == null) Debug.Log("Null");
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = npc.name;  // Set the NPC's name on the butt
            newButton.GetComponent<Button>().onClick.AddListener(() => GetNPCDetail(npc, npc.npcData));
        }
    }

    // Show NPC details when an NPC is selected from the list
    public void GetNPCDetail(NPC npc, NPCData npcData) {
/*        Debug.Log(npc.name);*/
        UiManager.Instance.npcUiManager.ShowNPCUi(npc, npcData/*, UIManager.State.SubState*/);
    }

    public void UpdateStorageUI() {
/*        for(int i = 0; i < hubStorageTxt.Count; i++) {
            // Assume each index in resourceStorageUI corresponds to a resource index
            hubStorageTxt[i].text = hubData.hubstorage.storedResources[i].ToString();
        }*/
    }
    #endregion

    #region RequestResource_Panel

    public void InitializeResourceFields() {
        if(resourceDB.allResources.Count != resourceInputField.Count) {
            Debug.Log("Resource or InputField is not equal");
        }
    }
    public void Btn_SendResource() {
        // Create a dictionary to store the resources and their corresponding amounts
        Dictionary<ResourceType, int> resourcePackage = new Dictionary<ResourceType, int>();

        // Loop through the input fields to gather resources
        for(int i = 0; i < resourceInputField.Count; i++) {
            string input = resourceInputField[i].text;

            if(string.IsNullOrEmpty(input)) {
                input = "0"; // Set default value to 0 if the input is empty
            }

            // Parse input and proceed if it's a valid number
            if(int.TryParse(input, out int amount)) {
                // Example: Get the resource by index or name
                ResourceType resourceType = resourceDB.GetResourceByIndex(i);

                if(amount > 0 && resourceType != null) {
                    // Add the resource and its amount to the dictionary
                    resourcePackage.Add(resourceType, amount);
/*                    Debug.Log($"Requested {amount} of resource: {resourceType.resourceName}");*/
                }
            }
        }

        // Call CreateSendOrder with the resource package
        if(resourcePackage.Count > 0 && activeNPC != null) {
            hub.SendResources(resourcePackage, activeNPC);
        } else {
            Debug.LogWarning("No resources to send or no NPC selected.");
        }
    }

    public void Btn_RequestResource() {
        InitializeResourceFields();
        for(int i = 0; i < resourceInputField.Count; i++) {
            string input = resourceInputField[i].text;
            input ??= "0"; // if input == null, input = "0":

            // Parse input and proceed if it's a valid number
            if(int.TryParse(input, out int amount)) {
                // Example: Get the resource by index or name
                ResourceType resourceType = resourceDB.GetResourceByIndex(i);

                if(amount != 0 && resourceType != null) {
                    activeNPC.ReceiveRequest(resourceType, amount);
                    Debug.Log("Requested " + amount + " of resource: " + resourceType.resourceName);
                }
            }/* else {
                Debug.LogWarning("Invalid input for resource at index " + i);
            }*/
        }
    }

    #endregion

    #region Communication_Panel
    public void Btn_OpenCommWin() {
/*        UIManager.Instance.HideHubUi();*/
        commWindow.SetActive(true);
        PopulateNPCListInCommunication();
    }

    public void Btn_CloseCommWin() {
        commWindow.SetActive(false);
        HideHubUi();
/*        UIManager.Instance.ShowHubUi();*/
    }
    public void PopulateNPCListInCommunication() {
        // Clear any existing NPC buttons
        foreach(Transform child in npcListContentComm) {
            Destroy(child.gameObject);
        }

        // Iterate through NPCs from the GameManager's npcDataList and create a button for each NPC
        foreach(NPC npc in GameManager.Instance.GetActiveNPCList()) {
            GameObject newButton = Instantiate(npcButtonPrefab, npcListContentComm);
            if(newButton.GetComponentInChildren<TextMeshProUGUI>() == null) Debug.Log("Null");
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = npc.name;  // Set the NPC's name on the button
            newButton.GetComponent<Button>().onClick.AddListener(() => Btn_ActivateComm(npc));
        }
    }

    public void Btn_ActivateComm(NPC npc) {
        commScreen.SetActive(true);
        npcNameTxtComm.text = npc.name;
        activeNPC = npc;
/*        npc.StartCommunication();*/
    }
    #endregion

    #region Delivery_Panel

    public void Btn_ShowDeliveryUIPanel() {
        deliveryUIPanel.SetActive(true);
        /*deliveryUIManager.Initialize();*/
    }
    public void Btn_HideDeliveryUIPanel() { 
        deliveryUIPanel.SetActive(false);
    }


    #endregion

    #region ShowHide
    public void ShowHubUi() {
        hubUiPannel.SetActive(true);
        Initialize();
    }

    // Hide Hub UI
    public void HideHubUi() {
        hubUiPannel.SetActive(false);
        HideAllPanel();
    }

    public void ShowCommUi() {
        commWindow.SetActive(true);
    }
    public void HideCommUi() {
        commWindow.SetActive(false);
    }
    #endregion
}
