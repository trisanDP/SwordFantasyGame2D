using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static Chest;

public class LootBox_Base : Intractable  {

    #region Variables

    public List<ItemClass> ItemsRewards;
    [Header("Components")]
    protected Animator animator;
    protected ChestUI_Manager UiManager;

    protected int range;
    protected bool isOpen = false;
    protected LayerMask playerLayer;

    #region IMP_Extras
    [HideInInspector] public static LootBox_Base activeLootBox;
    #endregion

    #endregion


    protected virtual void Start() {
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        animator = GetComponent<Animator>();
        UiManager = GameObject.Find("GameUI_Handler").GetComponent<ChestUI_Manager>();
    }

    protected virtual void Update() {
        Collider2D col = Physics2D.OverlapCircle(transform.position, range, playerLayer);
        if (isOpen == true && (col == null || col.name != "Player")) {
            CloseChest();
        }
    }

    #region IntractableF
    public override void OnEntract() {
        if (isOpen == false) {
            activeLootBox = this;
            UiManager.UpdateUI(this);
            OpenChest();
        } else {
            CloseChest();
        }

    }
    #endregion

    protected void OpenChest() { // Called In OnEntract()
        isOpen = true;
        UiManager.Show();
        animator.SetTrigger("Open");
    }

    protected void CloseChest() {
        isOpen = false;
        UiManager.Hide();
        animator.SetTrigger("Close");
        activeLootBox = null;
    }

    public void RemoveChestItem(ItemClass item) {
        ItemsRewards.Remove(item);
        UiManager.UpdateUI(this);
    }

    #region Extra/Collider
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ground"))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    #endregion
}
