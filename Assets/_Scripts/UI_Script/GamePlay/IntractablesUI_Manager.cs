using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class IntractablesUI_Manager : MonoBehaviour
{
    public GameObject intractObj;
    public TextMeshProUGUI intractTxt;

    public GameManager gameManager;
    private void Start() {
        gameManager = GameManager.Instance;
        if(intractObj == null)
            Debug.Log("IntractObj is Null");
        if(intractTxt == null)
            Debug.Log("Intract Txt is Null");
    }

    public void Show(Intractable obj)  // called in PlayerIntract 
    {
        intractObj.SetActive(true); 
        intractTxt.text = "Press E for " + obj.name;

    }
    public void Hide() // Called in Player INtract
    {
        intractObj.SetActive(false);
    }
}
