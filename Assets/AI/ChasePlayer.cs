using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : StateMachineBehaviour
{
    public AI_Behaviour ai_Behaviour;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai_Behaviour = animator.GetComponentInParent<AI_Behaviour>();
        ai_Behaviour.AI.isStopped = false;
        ai_Behaviour.aiState = AIState.chase;
        ai_Behaviour.AI.destination = ai_Behaviour.target.position;
        ai_Behaviour._animator.SetBool("FindPlayer",true);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai_Behaviour.AI.destination = ai_Behaviour.target.position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        PlayerPrefs.SetFloat("targetX",ai_Behaviour.transform.position.x);
        PlayerPrefs.SetFloat("targetY",ai_Behaviour.transform.position.y);
        PlayerPrefs.SetFloat("targetZ",ai_Behaviour.transform.position.z);
        ai_Behaviour._animator.SetBool("FindPlayer", false);
        // stop chasing
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
