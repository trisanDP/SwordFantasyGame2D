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
