using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPositionChanger : StateMachineBehaviour
{
    GameObject player;
    Vector3 current, target;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        player = FindObjectOfType<PlayerController>().gameObject;
        current = player.transform.position;
        target = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
        timeInSec = 0;
    }
    bool oneTime = true;
    float timeInSec = 0;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeInSec += Time.deltaTime;
        if (timeInSec > 3 && player.transform.position.y < target.y)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 0.2f);
            
        }
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     
      
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
