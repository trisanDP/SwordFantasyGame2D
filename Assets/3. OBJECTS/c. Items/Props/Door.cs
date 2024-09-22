using UnityEngine;

namespace OriginL
{
    public class Door : MonoBehaviour, IIntractable {

        [Header("Component")]
        Animator animator;

        [Header("Basic")]
        bool isOpen;
        public string DoorName;
        public string message;

        private void Awake() {
            animator = GetComponent<Animator>();
            isOpen = false;
        }
        #region DoorFunction

        void OpenDoor() {
            isOpen = true;
            animator.SetTrigger("Open");
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        void CloseDoor() {
            isOpen = false;
            animator.SetTrigger("Close");
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }


        #endregion

        #region Interface Functions  
        public void OnIntract() {
            if(isOpen)
                CloseDoor();
            else
                OpenDoor();
        }
        public string Message() {
            return message + ""+ DoorName;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }

        #endregion
    }
}
