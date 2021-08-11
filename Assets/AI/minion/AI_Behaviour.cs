using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum AIState {patrol, chase, attack, check}
public class AI_Behaviour : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioSource A0;

    public Transform target;
    public AIState aiState;
    public Animator _animator;
    public NavMeshAgent AI;
    public bool isGround;//用于设置小球是否再地面
    public bool isJump;
    
    public bool goinUp = false;
   
    public LayerMask groundMask;
    public Vector3 move;
    public Rigidbody rigidbody;


    //小兵属性
    public float jumpForce = 100f;
    public float groundDistance = 0.4f;
    public float life = 100f;
    public float damage = 4f;
    public float speed = 10f;

    public float friction = 0.1f;//摩擦力
    // Start is called before the first frame update

    public void Awake()
    {
        AI = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
        

    void Start()
    {
        Debug.Log("AI SETTING");
        
        _animator.SetBool("FindPlayer", false);
        //_animator.SetFloat("speed", speed);
        aiState = AIState.patrol;
        isGround = true;
        isJump = false;
        if(target == null)
        {
            target = this.transform;
        }
    }

    // Update is called once per frame

    IEnumerator Jumping() {
        AI.Stop(true);
        yield return new WaitForSeconds(2f);
        AI.Resume();
    }


    void Update()
    {

        isGround = Physics.CheckSphere(AI.transform.position,groundDistance,groundMask);
        float upForce = 0;


        //if (!isJump) //如果不是在跳跃
        //{
            Vector3 _playerPosition = target.position;     // Check Player Distance  玩家的位置
            float playerDistance = (_playerPosition - this.transform.position).magnitude;  // npc和玩家之间的实际距离
            _animator.SetFloat("PlayerDistance", playerDistance);   // player distance changes AI state 
            
        //}

        //if (AI.isOnOffMeshLink)//如果小球到达跳跃边缘
        //{
        //    OffMeshLinkData offMeshLinkData = AI.currentOffMeshLinkData;
        //    transform.LookAt(offMeshLinkData.endPos);//面朝跳跃的结束点
        //    AI.enabled = false; //关闭自动寻路
        //    isJump = true;
        //    rigidbody.velocity = new Vector3(offMeshLinkData.endPos.x * 3,5,offMeshLinkData.endPos.z);
        //}
        //if(this.transform.position.y< -3)
        //{
        //    Debug.LogError("unknow area");
        //}


        //if (isGround)
        //{

        //    AI.enabled = true;
            //if (AI.isOnOffMeshLink) //如果小球到达跳跃边缘
            //{

            //    move = AI.transform.right + AI.transform.forward;

            //    if (jumpSound != null) A0.PlayOneShot(jumpSound);

            //    OffMeshLinkData offMeshLinkData = AI.currentOffMeshLinkData;
            //    transform.LookAt(offMeshLinkData.endPos); //面朝跳跃结束点
            //    //AI.enabled = false;//关闭自动寻路
            //    StartCoroutine(Jumping());

            //    //isGround = false;
            //    goinUp = true;
            //    //upForce = jumpForce;

            //}

            //if (target != null)
            //{
            //    AI.SetDestination(target.position);
                
            //    CharacterController.Move(AI.desiredVelocity,false,goinUp);
            //}
            //else
            //{
            //    ai.move(vector3.zero);
            //}




        //}
        //else {
        //    if (AI.move.magnitude > 0) move -= AI.transform.forward * Time.deltaTime * friction;
        //}

        //if (goinUp) { 


        //}



        //Vector3 _playerPosition = target.position;     // Check Player Distance  玩家的位置
        //float playerDistance = (_playerPosition - this.transform.position).magnitude;  // npc和玩家之间的实际距离
        //_animator.SetFloat("PlayerDistance", playerDistance);   // player distance changes AI state 
        // print("Player Distance is " + playerDistance);
    }
}
