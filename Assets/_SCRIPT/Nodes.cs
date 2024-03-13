using OriginL.Building;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/*[RequireComponent(typeof(CircleCollider2D))]*/
public class Nodes : IIntractable {/*
    #region NodeComponent

    public Animator animator;
*//*    private SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;*/

    /*    [Header("Primitive")]
        private int currentSpriteIndex = 0;*//*
        #endregion

        #region BuildingComponent
        public GameObject building;
        BuildingBase baseScript;

        #endregion

        private void Start() {
            baseScript = building.GetComponent<BuildingBase>();
            Message = Message + "" + baseScript.BuildingName;
        }

        public override void OnEntract() {
            GameObject build = Instantiate(building,new Vector2(transform.position.x,building.transform.position.y) , Quaternion.identity);
            build.GetComponent<BuildingBase>().node = gameObject;
            gameObject.SetActive(false);

        }

        void SetBuilding() {
            *//*        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
            spriteRenderer.sprite = sprites[currentSpriteIndex];*//*
        }*/
    public GameObject GetGameObject() {
        throw new System.NotImplementedException();
    }

    public string Message() {
        throw new System.NotImplementedException();
    }

    public void OnIntract() {
        throw new System.NotImplementedException();
    }
}
