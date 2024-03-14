using System.Collections.Generic;
using UnityEngine;
using OriginL.Item;

namespace OriginL.ChestSpace {
    public abstract class LootBox_Base : MonoBehaviour, IIntractable {

        #region Variables

        public List<ItemClass> ItemsRewards;
        [Header("Components")]
        protected Animator animator;
        protected ChestUI_Manager ChestUi_M;

        protected int range;
        protected bool isOpen;
        protected LayerMask playerLayer;

        public string message;

        #region IMP_Extras
        [HideInInspector] public static LootBox_Base activeLootBox;
        #endregion

        #endregion

        internal enum BoxState {
            Normal, Open, Close, Empty
        }
        BoxState activeState;

        protected virtual void Start() {
            playerLayer = 1 << LayerMask.NameToLayer("Player");
            animator = GetComponent<Animator>();
            ChestUi_M = UiManager.Instance.chestUI;
            isOpen = false;

        }

        protected virtual void Update() {
            switch(activeState) {
                case BoxState.Normal:

                break;
                case BoxState.Open:
                Collider2D col = Physics2D.OverlapCircle(transform.position, range, playerLayer);
                if(col == null || col.name != "Player") {
                    CloseChest();
                }
                break;
                case BoxState.Close:

                break;
                case BoxState.Empty:

                break;
            }

        }

        #region ChestFunctions
        protected void OpenChest() { // Called In OnEntract()
            isOpen = true;
            ChestUi_M.Show();
            animator.SetTrigger("Open");
            activeState = BoxState.Open;
        }

        protected void CloseChest() {
            isOpen = false;
            ChestUi_M.Hide();
            animator.SetTrigger("Close");
            activeLootBox = null;
            activeState = BoxState.Close;
        }

        public void RemoveChestItem(ItemClass item) {
            ItemsRewards.Remove(item);
            ChestUi_M.UpdateUI(this);
            if(ItemsRewards.Count <= 0) {
                activeState = BoxState.Empty;
            }
        }
        #endregion

        #region IntractableFunctions
        public void OnIntract() {
            if(isOpen == false) {
                activeLootBox = this;
                ChestUi_M.UpdateUI(this);
                OpenChest();
            } else {
                CloseChest();
            }

        }
        public abstract string Message();

        public GameObject GetGameObject() {
            return gameObject;
        }
        #endregion

        #region Extra/Collider
        private void OnTriggerEnter2D(Collider2D collision) {
            if(collision.CompareTag("Ground")) {
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
}