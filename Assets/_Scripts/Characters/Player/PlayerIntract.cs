using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerIntract : MonoBehaviour
{
    #region Variables
    PlayerScript playerScrip;
    public int range;
    IntractablesUI_Manager intractablesUI;

    #endregion

    private void Start(){
        playerScrip = GetComponent<PlayerScript>();
        intractablesUI = UiManager.Instance.intractableUi;
    }

    private void Update()
    {
        //Intract Input
        if (Input.GetKeyDown(KeyCode.E))
        {
            Intractable intra = HasIntractObj();
            if (intra != null)
            {
                intra.OnEntract();
            }
        }
        #region Intract UI 
        if (HasIntractObj() != null) {
            intractablesUI.Show(HasIntractObj());
        } 
        else {
            intractablesUI.Hide();
        }
        #endregion
    }

    #region IntractDetect:
    public Intractable HasIntractObj()
    {

        List<Intractable> intractableList = new();

        Collider2D[] colArr = Physics2D.OverlapCircleAll(transform.position, range); // To find All Intractable Objects in Range
        foreach (Collider2D col in colArr)
        {
            if (col.TryGetComponent(out Intractable intract))
            {
                intractableList.Add(intract);
            }
        }


        Intractable closest = null;
        foreach (Intractable objs in intractableList)  // To Find Closest Object
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

        return closest;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    #endregion 
}
