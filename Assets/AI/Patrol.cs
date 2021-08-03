using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour
{
    public AI_Behaviour ai_Behaviour;
    public Vector3 wayPoint;
    private Vector3 PreviousPosition;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // OnStateEnter 在转换开始并且状态机开始评估此状态时被调用
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai_Behaviour = animator.GetComponentInParent<AI_Behaviour>();
        ai_Behaviour.AI.isStopped = false;
        ai_Behaviour.aiState = AIState.patrol;

        if (PlayerPrefs.GetFloat("targetX") != 0.0f && PlayerPrefs.GetFloat("targetY") != 0.0f && PlayerPrefs.GetFloat("targetZ") != 0.0f)
        {
            wayPoint = new Vector3(PlayerPrefs.GetFloat("targetX"), PlayerPrefs.GetFloat("targetY"), PlayerPrefs.GetFloat("targetZ")); 
        }
        else 
        {
            wayPoint = new Vector3(Random.Range(-30, 30), 1.08f, Random.Range(-30, 30)); // pick initial patrol location 选择初始的巡逻地点
        }
        
        ai_Behaviour.AI.destination = wayPoint;
        PreviousPosition = wayPoint;
        Debug.Log(wayPoint);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // OnStateUpdate 在 OnStateEnter 和 OnStateExit 回调之间的每个更新帧上调用
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {



        // change patrol waypoint after one has been reached
        // 到达后更改巡逻的waypoint
        
        // 判断npc是否到达制定巡逻点，如果到达，则切换到下一个巡逻点
        // 必须判断npc是卡住了还是已经到达
        if (PreviousPosition == ai_Behaviour.transform.position)
        {
            wayPoint = new Vector3(Random.Range(-30, 30), 1.08f, Random.Range(-30, 30));
            //Debug.Log(wayPoint);
        }
        ai_Behaviour.AI.destination = wayPoint;
        PreviousPosition = ai_Behaviour.transform.position;
        Debug.Log("NPC POSITION : " + ai_Behaviour.transform.position + " WAYPOINT : " + wayPoint);
        //Debug.Log("WAYPOINT : " + wayPoint);


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
          // stop patrolling
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
