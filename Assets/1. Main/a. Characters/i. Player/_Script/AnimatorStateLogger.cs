using UnityEngine;

public class AnimationStateLogger : StateMachineBehaviour {
    // This is called when entering a new animation state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Entered State: " + stateInfo.shortNameHash);
    }

    // This is called when exiting an animation state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Exited State: " + stateInfo.shortNameHash);
    }
}