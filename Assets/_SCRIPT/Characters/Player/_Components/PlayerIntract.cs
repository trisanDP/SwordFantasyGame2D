using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIntract : MonoBehaviour
{
    #region Variables
    public int range;
    IntractablesUI_Manager intractablesUI;

    #endregion
    private void Awake() {
        intractablesUI = UiManager.Instance.intractableUi;
    }

    private void Start(){        
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
            IIntractable intra = HasIntractObj();
            intra?.OnIntract();
        }
    }

    #endregion

    #region Ui
    void DisplayUI() {
        if(HasIntractObj() != null) {
            intractablesUI.Show(HasIntractObj().Message());
        } else {
            intractablesUI.Hide();
        }
    }
    #endregion

    #region IntractDetect:
    public IIntractable HasIntractObj() { 
        List<IIntractable> intractableList = new();
        // To find All Intractable Objects in Range
        #region FindALlOBJ
        Collider2D[] colArr = Physics2D.OverlapCircleAll(transform.position, range); 
        foreach (Collider2D col in colArr)
        {
            if (col.TryGetComponent(out IIntractable intract))
            {
                intractableList.Add(intract);
            }
        }
        #endregion

        // To Find Closest Object
        #region FindClosestOBJ
        IIntractable closest = null;
        foreach(IIntractable objs in intractableList)  
        {
            GameObject objGameObject = objs.GetGameObject();
            if (closest == null)
            {
                closest = objs;
            } else
            {
                if (Vector2.Distance(transform.position, objGameObject.transform.position) < Vector2.Distance(transform.position, closest.GetGameObject().transform.position)) //....
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
