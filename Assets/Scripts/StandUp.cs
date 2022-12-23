using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp : StateMachineBehaviour
{
   
    public Vector3 targetPosition;
     float stepVelocity = 0.2f;
    public Transform playerTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPosition = PlayerController.instance.positionWhereToStandAfterFall;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerController.instance.transform.position.y < targetPosition.y)
            PlayerController.instance.transform.position = Vector3.MoveTowards(PlayerController.instance.transform.position, targetPosition, stepVelocity);
        else
        {
            animator.Play("waitForLine");
            PlayerController.instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
