using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameAssets : MonoBehaviour {

    private static GameAssets _i;

    public static GameAssets i {
        get {
            if(_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }


    public GameObject gameUiHandler;

    [Header("Miner")]
    public Sprite Miner1;
    public Sprite Miner2;
    public Sprite Miner3;

    [Header("Shooter")]
    public Sprite Shooter1;
    public Sprite Shooter2;
    public Sprite Shooter3;

    public Sprite node;
}

