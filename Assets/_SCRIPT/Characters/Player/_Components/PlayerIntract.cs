using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIntract : MonoBehaviour
{
    #region Variables
    PlayerScript playerScrip;
    public int range;
    IntractablesUI_Manager intractablesUI;

    #endregion
    private void Awake() {
        intractablesUI = UiManager.Instance.intractableUi;
        Debug.Log("Testing111");
    }

    private void Start(){
        playerScrip = GetComponent<PlayerScript>();
        
        if(intractablesUI == null ) {
            Debug.Log(" intractable UI missing ");
        }
    }

    private void Update() { 
        #region Intract UI 
        DisplayUI();
        #endregion
    }

    #region Input
    public void PressedE(InputAction.CallbackContext context) {
        if(context.performed) {
            Debug.Log("Pressed E");
            Intractable intra = HasIntractObj();
            if(intra != null) {
                intra.OnEntract();
            }
        }
    }

    #endregion

    #region Ui
    void DisplayUI() {
        if(HasIntractObj() != null) {
            intractablesUI.Show(HasIntractObj().Message);
        } else {
            intractablesUI.Hide();
        }
    }
    #endregion

    #region IntractDetect:
    public Intractable HasIntractObj() { 
        List<Intractable> intractableList = new();
        // To find All Intractable Objects in Range
        #region FindALlOBJ
        Collider2D[] colArr = Physics2D.OverlapCircleAll(transform.position, range); 
        foreach (Collider2D col in colArr)
        {
            if (col.TryGetComponent(out Intractable intract))
            {
                intractableList.Add(intract);
            }
        }
        #endregion

        // To Find Closest Object
        #region FindClosestOBJ
        Intractable closest = null;
        foreach (Intractable objs in intractableList)  
        {
            if (closest == null)
            {
                closest = objs;
            } else
            {
                if (Vector2.Distance(transform.position, objs.transform.position) < Vector2.Distance(transform.position, closest.transform.position)) //....
                {
                    closest = objs;
                }
            }
        }
        #endregion

        return closest;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    #endregion 
}
