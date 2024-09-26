using TMPro;
using UnityEngine;

public class IntractablesUI_Manager : MonoBehaviour
{
    public GameObject intractObj;
    public TextMeshProUGUI intractTxt;
    PlayerIntract playerIntract;

    private void Awake() {
        if(intractObj == null)
            Debug.Log("IntractObj is Null");
        if(intractTxt == null)
            Debug.Log("Intract Txt is Null");
        playerIntract = PlayerIntract.instance;
    }

    private void Update() {
        if(playerIntract.HasIntractObj() != null) {
            Show(playerIntract.HasIntractObj().Message());
        } else
            Hide();
    }
    private void OnEnable() {
        ScenesManager.Instance.OnSceneChange += RefreshRefrences;
    }
    private void OnDestroy() {
        ScenesManager.Instance.OnSceneChange -= RefreshRefrences;
    }
    private void RefreshRefrences() {

    }
    public void Show(string txt)  // called in PlayerIntract 
    {
        intractObj.SetActive(true); 
        intractTxt.text = txt; 

    }
    public void Hide() // Called in Player INtract
    {
        intractObj.SetActive(false);
    }
}
