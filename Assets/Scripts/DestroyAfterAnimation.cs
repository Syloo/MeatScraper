using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : StateMachineBehaviour
{
    public bool destroyParent = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject target = animator.gameObject;
        if (destroyParent) target = target.transform.parent.gameObject;

        Destroy(target, stateInfo.length);
    }
}
