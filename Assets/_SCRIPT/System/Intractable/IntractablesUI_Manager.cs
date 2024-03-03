using TMPro;
using UnityEngine;

public class IntractablesUI_Manager : MonoBehaviour
{
    public GameObject intractObj;
    public TextMeshProUGUI intractTxt;

    private void Awake() {
        if(intractObj == null)
            Debug.Log("IntractObj is Null");
        if(intractTxt == null)
            Debug.Log("Intract Txt is Null");
    }

    private void OnEnable() {
        ScenesManager.Instance.OnSceneChange += RefreshRefrences;
    }
    private void OnDisable() {
        ScenesManager.Instance.OnSceneChange -= RefreshRefrences;
    }
    private void RefreshRefrences() {

    }
    public void Show()  // called in PlayerIntract 
    {
        intractObj.SetActive(true); 
        intractTxt.text = "Press E To Intract";

    }
    public void Hide() // Called in Player INtract
    {
        intractObj.SetActive(false);
    }
}
