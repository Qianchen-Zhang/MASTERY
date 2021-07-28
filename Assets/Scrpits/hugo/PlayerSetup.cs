using Mirror;
using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    public static event Action<PlayerSetup, string> OnMessage;

    [SyncVar]
    private string hostName;

    private string userName;

    private PlayerInfo pi;

    public GameObject floatingInfo;
    public GameObject Cam;
    public TextMesh nameText;

    public GameObject PlayerBody;

    [SyncVar(hook = nameof(onPlayerNameChanged))]
    private string playerName;

    [SyncVar(hook = nameof(onPlayerColorChanged))]
    private Color playerColor;

    [SyncVar(hook = nameof(onPlayerStatusChanged))]
    public string statusText;

    private Material playerMaterialClone;

    [SerializeField]
    List<PlayerSetup> playerList = new List<PlayerSetup>();

    [SyncVar]
    public int playerReadyNum;


   
    private void onPlayerStatusChanged(string oldStr, string newStr)
    {
        pi.canavsStatusText.text = statusText;
    }

    private void onPlayerNameChanged(string oldStr, string newStr)
    {
        nameText.text = newStr;
    }

    private void onPlayerColorChanged(Color oldCol, Color newCol)
    {
        nameText.color = newCol;

        playerMaterialClone = new Material(PlayerBody.GetComponent<Renderer>().material);
        playerMaterialClone.SetColor("_Color", newCol);

        PlayerBody.GetComponent<Renderer>().material = playerMaterialClone;
    }

    private void Awake()
    {
        pi = FindObjectOfType<PlayerInfo>();
        userName = PlayerPrefs.GetString("UserName");
    }


    [Command]
    public void CmdSetupPlayer(string nameValue, Color colorValue)
    {
        playerName = nameValue;
        playerColor = colorValue;
        //sCript.statusText = $"{playerName} joined.";
        CmdSetPlayerStatusText(userName + " joined game.");

    }

    [Command]
    public void CmdSetupPlayerReadyNum(int num)
    {
        playerReadyNum += num;
    }
    
    [Command]
    public void CmdSetPlayerStatusText(string str)
    {
        statusText = str;
    }

    //client 命令 host 发送消息
    [Command]
    public void CmdSendPlayerMessage(string msg)
    {
        if (msg != null)
            RpcShowMessage(msg);
    }

    //host 向 所有客户端发送消息
    [ClientRpc]
    public void RpcShowMessage(string msg)
    {
        OnMessage?.Invoke(this, msg);
    }


    private void Start()
    {
        
        playerReadyNum = 0;
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

        }
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        hostName = PlayerPrefs.GetString("UserName");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if (!isLocalPlayer)
        {
            pi.canvas.SetActive(false);
        }

        if (isLocalPlayer)
        {
            if (!userName.Equals(hostName))
            {

            }
            else
            {

            }
        }
    }


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        pi.SendMessageToChat("[" + System.DateTime.Now + "] " + hostName
            + " is host", Message.MessageType.info);
        pi.SendMessageToChat("[" + System.DateTime.Now + "] " + userName
            + " join the game", Message.MessageType.info);
        CmdSendPlayerMessage("[" + System.DateTime.Now + "] " + userName
            + " join the game");

        //floatingInfo.transform.localPosition = new Vector3(-0.355f, 1.519f, -.128f);
        //floatingInfo.transform.localScale = new Vector3(1f, 1f, 1f);
        ChangePlayerNameAndColor();
        
    }

    private void ChangePlayerNameAndColor()
    {
        var tempName = userName;
        var tempColor = new Color
        (
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            1
         );

        CmdSetupPlayer(tempName, tempColor);
    }


    public void readyButtonFunction()
    {
        

        if (isServer)
        {
            NetworkManager.singleton.ServerChangeScene("waitingHall1");
        }
                
    }
   
}



