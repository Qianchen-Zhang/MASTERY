using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPosition : StateMachineBehaviour
{

    public AI_Behaviour ai_Behaviour;
    public Vector3 wayPoint;
    private Vector3 PreviousPosition;
    public float timeSpend;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai_Behaviour = animator.GetComponentInParent<AI_Behaviour>();
        ai_Behaviour.AI.isStopped = false;
        ai_Behaviour.aiState = AIState.check;
        wayPoint = new Vector3(PlayerPrefs.GetFloat("targetX"), PlayerPrefs.GetFloat("targetY"), PlayerPrefs.GetFloat("targetZ"));
        ai_Behaviour.AI.destination = wayPoint;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //如果位置不变
        if (PreviousPosition == ai_Behaviour.transform.position)
        {

            timeSpend += Time.deltaTime;
            ai_Behaviour._animator.SetFloat("InspectionTime", timeSpend);
        }
        PreviousPosition = ai_Behaviour.transform.position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai_Behaviour._animator.SetFloat("InspectionTime", 0.0f);
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
