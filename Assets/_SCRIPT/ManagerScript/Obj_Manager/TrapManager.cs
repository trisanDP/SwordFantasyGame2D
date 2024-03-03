 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    GameObject player;
    GameObject[] trap;

    public int detectRange,rotateSpeed;
    public float distance;

    void Start(){
        player = GameObject.Find("Player");
        trap = GameObject.FindGameObjectsWithTag("Trap1");
        for(int i  = 0; i < trap.Length; i++) {
            trap[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void FixedUpdate(){
        if (player != null) {
            foreach (GameObject obj in trap) {
                distance = Vector2.Distance(player.transform.position, obj.transform.position);

                if (distance <= detectRange) {
                    obj.transform.Rotate(Time.fixedDeltaTime * rotateSpeed * Vector3.forward);
                    obj.SetActive(true);
                } else {
                    obj.SetActive(false);
                }
            }
        }
        
    }

}
