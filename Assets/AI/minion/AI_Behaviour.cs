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
    public bool isGround;//��������С���Ƿ��ٵ���
    public bool isJump;
    
    public bool goinUp = false;
   
    public LayerMask groundMask;
    public Vector3 move;
    public Rigidbody rigidbody;


    //С������
    public float jumpForce = 100f;
    public float groundDistance = 0.4f;
    public float life = 100f;
    public float damage = 4f;
    public float speed = 10f;

    public float friction = 0.1f;//Ħ����
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


        //if (!isJump) //�����������Ծ
        //{
            Vector3 _playerPosition = target.position;     // Check Player Distance  ��ҵ�λ��
            float playerDistance = (_playerPosition - this.transform.position).magnitude;  // npc�����֮���ʵ�ʾ���
            _animator.SetFloat("PlayerDistance", playerDistance);   // player distance changes AI state 
            
        //}

        //if (AI.isOnOffMeshLink)//���С�򵽴���Ծ��Ե
        //{
        //    OffMeshLinkData offMeshLinkData = AI.currentOffMeshLinkData;
        //    transform.LookAt(offMeshLinkData.endPos);//�泯��Ծ�Ľ�����
        //    AI.enabled = false; //�ر��Զ�Ѱ·
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
            //if (AI.isOnOffMeshLink) //���С�򵽴���Ծ��Ե
            //{

            //    move = AI.transform.right + AI.transform.forward;

            //    if (jumpSound != null) A0.PlayOneShot(jumpSound);

            //    OffMeshLinkData offMeshLinkData = AI.currentOffMeshLinkData;
            //    transform.LookAt(offMeshLinkData.endPos); //�泯��Ծ������
            //    //AI.enabled = false;//�ر��Զ�Ѱ·
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



        //Vector3 _playerPosition = target.position;     // Check Player Distance  ��ҵ�λ��
        //float playerDistance = (_playerPosition - this.transform.position).magnitude;  // npc�����֮���ʵ�ʾ���
        //_animator.SetFloat("PlayerDistance", playerDistance);   // player distance changes AI state 
        // print("Player Distance is " + playerDistance);
    }
}
