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
            isOpen = true;
            animator.SetTrigger("Open");
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        void CloseDoor() {
            isOpen = false;
            animator.SetTrigger("Close"); 
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
