using OriginL.Building;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/*[RequireComponent(typeof(CircleCollider2D))]*/
public class Nodes : Intractable
{
    #region NodeComponent

    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;

/*    [Header("Primitive")]
    private int currentSpriteIndex = 0;*/
    #endregion

    #region BuildingComponent
    public GameObject building;
    BuildingBase baseScript;

    #endregion

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseScript = building.GetComponent<BuildingBase>();
        Message = Message + "" + baseScript.BuildingName;
    }

    public override void OnEntract() {
        GameObject build = Instantiate(building,transform.position, Quaternion.identity);
        build.GetComponent<BuildingBase>().node = gameObject;
        gameObject.SetActive(false);

    }

    void SetBuilding() {
        /*        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
        spriteRenderer.sprite = sprites[currentSpriteIndex];*/
    }
}
