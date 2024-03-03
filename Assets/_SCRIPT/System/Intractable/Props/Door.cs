using UnityEngine;

namespace OriginL
{
    public class Door : Intractable {

        [SerializeField] Animator animator;
        bool isOpen;
        private void Awake() {
            animator = GetComponent<Animator>();
            isOpen = false;
        }
        public override void OnEntract() {
            if(isOpen)
                CloseDoor();
            else
                OpenDoor();
        }

        void OpenDoor() {
            animator.SetTrigger("Open");
            Debug.Log("OpenDoor");
            isOpen = true;
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        void CloseDoor() {
            animator.SetTrigger("Close");
            Debug.Log("CloseDoor");
            isOpen = false;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
