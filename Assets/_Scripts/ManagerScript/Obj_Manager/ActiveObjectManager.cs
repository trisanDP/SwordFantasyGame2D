using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveObjectManager : MonoBehaviour
{

    [SerializeField] GameObject[] objectsToControl;
    GameObject player;
    public float distance,detectRange = 15f;
    public TagAttribute tg;


    private void Start() {
        player = GameObject.Find("Player");
        objectsToControl = GameObject.FindGameObjectsWithTag("Ground");

        objectsToControl ??= new GameObject[0]; //if null creates new gameobjects

    }

    private void Update() {
        if (objectsToControl.Length > 0 && !GameManager.Instance.isGameOver)
        {
            foreach (GameObject obj in objectsToControl)
            {
                distance = Vector2.Distance(player.transform.position, obj.transform.position);

                if (distance <= detectRange)
                {
                    obj.SetActive(true);
                }

                if (distance > detectRange)
                {
                    obj.SetActive(false);
                }
            }
        }
        if(GameManager.Instance.isGameOver) {
            return;
        }
        
    }

}
