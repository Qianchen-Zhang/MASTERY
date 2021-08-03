using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;

public class lobbyWindow : MonoBehaviour
{
    public int maxMessage = 25;

    public Scrollbar scrollbar;

    public bool selectChatInput = false;

    public TMP_InputField chatMessage;
    //public TMP_Text chatHistory;
    //public Scrollbar scrollbar;
    public GameObject chatPanel, textObject;
    public TMP_Text userName, StartBtn;
    public TMP_Text player1, player2, player3, player4;
    public GameObject player1Btn, player2Btn, player3Btn, player4Btn;
    public Button startBtn;
    public Sprite[] img = new Sprite[5];

    public Color32 playerMessage, info, myMessage, otherPlayerMessage;

    public waintingHallPlayerScrpit whPs;

    public bool needSendBackName = false;

    public GameObject gamePlayerPrefab;

    public NetworkManager net ;

    [SerializeField]
    List<Message> messageList = new List<Message>();


    //[SyncVar(hook = nameof(OnstatusTextChanged))]
    //public string otherPlayersMessage;


    public void Awake()
    {
        //whPs = FindObjectOfType<waintingHallPlayerScrpit>();
        waintingHallPlayerScrpit.OnMessage += OnplayerMessage;
        //whPs.CmdSetupPlayer(whPs.userName);
        //waintingHallPlayerScrpit.OnPlayerSetup += OnPlayerSetup;
        //whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        //SendMessageToChat("[" + System.DateTime.Now + "] " + PlayerPrefs.GetString("UserName")
            //+ " join the game", Message.MessageType.myMessage);
        //whPs.CmdSendPlayerMessage("[" + System.DateTime.Now + "] " + PlayerPrefs.GetString("UserName") + ": "
               // + chatMessage.text.Trim());
    }


    public void OnplayerMessage(waintingHallPlayerScrpit whPs, string message)
    {
        //string prettyMessage = message;
       
        if (!whPs.isLocalPlayer) SendMessageToChat(message, Message.MessageType.otherPlayerMessage);

        Debug.Log(message);
    }


    
   

    public void OnPlayerSetup(waintingHallPlayerScrpit whPs, string message)
    {
        this.whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        if (!whPs.isLocalPlayer)
        {
            if (this.whPs.position1)
            {
                if (this.whPs.position2)
                {
                    if (this.whPs.position3)
                    {
                        if (this.whPs.position4)
                        {
                            Debug.Log("This room is full !");
                        }
                        else
                        { 
                            player4.text = message;
                            this.whPs.position4 = true;
                            this.whPs.player4Img = 1;
                        }
                    }
                    else
                    {
                        player3.text = message;
                        this.whPs.position3 = true;
                        this.whPs.player3Img = 1;
                    }
                }
                else
                {
                    player2.text = message;
                    this.whPs.position2 = true;
                    this.whPs.player2Img = 1;
                }
            }
            else
            {
                player1.text = message;
                this.whPs.position1 = true;
                this.whPs.player1Img = 1;
            }
            //this.whPs.CmdSetupPlayer(this.whPs.userName);
        }
       
        Debug.Log(message);
    }


    public void Start()
    {
        //whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        userName.text = PlayerPrefs.GetString("UserName") + ":";
        //SendMessageToChat("[" + System.DateTime.Now + "] " + PlayerPrefs.GetString("UserName") 
          //  + " join the game",Message.MessageType.myMessage);
        //whPs.CmdSendPlayerMessage("Hello");
    }

    

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return) && selectChatInput && chatMessage.text != "")
        {
            whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
            SendMessageToChat("[" + System.DateTime.Now +"] " +PlayerPrefs.GetString("UserName") + ": "
                + chatMessage.text.Trim(),Message.MessageType.myMessage);
            whPs.CmdSendPlayerMessage("[" + System.DateTime.Now + "] " + PlayerPrefs.GetString("UserName") + ": "
                + chatMessage.text.Trim());
            chatMessage.text = "";
            Debug.Log(messageList.Count);
        }
        scrollbar.value = 0;
    }

   

   


    public void selectChatInputField()
    {
        selectChatInput = true;
    }

    public void deselectChatInputField()
    {
        selectChatInput = false;
    }


    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if (messageList.Count >= maxMessage)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        //在改变otherplayersmessage变量后，如果是自己调用的，会打开ismymessage标签
        //如果是其他玩家改变了otherplayersmessage变量，则ismyMessage = false;
        


            Message newMessage = new Message();
            newMessage.text = text;
            newMessage.messageType = messageType;

            GameObject newText = Instantiate(textObject, chatPanel.transform);

            newMessage.textObject = newText.GetComponent<TMP_Text>();

            newMessage.textObject.text = newMessage.text;

            newMessage.textObject.faceColor = MessageTypeColor(messageType);

            //如果已经连接网络，且消息的发送方是自己的话
            //就将自己的消息同步到其他玩家中去
            if (whPs != null && messageType == Message.MessageType.myMessage)
            {
                //otherPlayersMessage = text;
            }
            messageList.Add(newMessage);
        
        
    }

    //给客户端发送信息后，发送的信息会直接到达对方的sendmessagetochat函数
    //问题：
   

    


    public Color32 MessageTypeColor(Message.MessageType messageType)
    {
        Color32 color = info;

        switch (messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;
            case Message.MessageType.myMessage:
                color = myMessage;
                break;
            case Message.MessageType.otherPlayerMessage:
                color = otherPlayerMessage;
                break;
           
        }

        return color;
    }

   
    private void OnstatusTextChanged(string oldStr, string newStr)
    {
        
        //SendMessageToChat(otherPlayersMessage, Message.MessageType.otherPlayerMessage);
        
    }

    public void disconnected()
    {
        whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        if (whPs != null)
        {

            whPs.CmdSendPlayerMessage("[" + System.DateTime.Now + "] " + whPs.userName
            + " leave the game");
            whPs.DeletePlayer(whPs.userName);
        }
    }

    public void ReadyBtn()
    {
        whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        if(whPs != null)
        {
            if (whPs.getUserReadyState(whPs.userName))
            {
                whPs.CmdReady(whPs.userName);
                Debug.Log(whPs.userName + " cancel Ready");
            }
            else
            {
                whPs.CmdReady(whPs.userName);
                Debug.Log(whPs.userName + " Ready");
            }
            
        }
        
    }

    public void StartButtonFonction()
    {

        
       
        whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        whPs.p1Ready = true;

        //switch (net.numPlayers)
        //{
        //    case 0:
        //        Debug.Log("dont have player!");
        //        break;
        //    case 1:
        //        if (whPs.p1Ready)
        //        {
        //            Debug.Log("start");
        //            //net.playerPrefab = gamePlayerPrefab;
        //            whPs.RpcGamePrefabs();

        //            net.ServerChangeScene("map1");


        //            //SceneManager.LoadScene("waitingHall1");

        //        }
        //        else
        //            Debug.Log("Someone not ready!");
        //        break;
        //    case 2:
        //        if (whPs.p2Ready)
        //        {
        //            Debug.Log("start");
        //            //net.playerPrefab = gamePlayerPrefab;
        //            whPs.RpcGamePrefabs();

        //            net.ServerChangeScene("map1");


        //            //SceneManager.LoadScene("waitingHall1");

        //        }
        //        else
        //            Debug.Log("Someone not ready!");
        //        break;
        //    case 3:
        //        if (whPs.p2Ready && whPs.p3Ready)
        //        {
        //            Debug.Log("start");
        //            //net.playerPrefab = gamePlayerPrefab;
        //            whPs.RpcGamePrefabs();

        //            net.ServerChangeScene("map1");


        //            //SceneManager.LoadScene("waitingHall1");

        //        }
        //        else
        //            Debug.Log("Someone not ready!");
        //        break;
        //    case 4:
        //        if (whPs.p2Ready && whPs.p3Ready && whPs.p4Ready)
        //        {
        //            Debug.Log("start");
        //            //net.playerPrefab = gamePlayerPrefab;
        //            whPs.RpcGamePrefabs();

        //            net.ServerChangeScene("map1");


        //            //SceneManager.LoadScene("waitingHall1");

        //        }
        //        else
        //            Debug.Log("Someone not ready!");
        //        break;
        if (whPs.p1Ready)
        {
            Debug.Log("start");
            //net.playerPrefab = gamePlayerPrefab;
            whPs.RpcGamePrefabs();

            net.ServerChangeScene("map1");


            //SceneManager.LoadScene("waitingHall1");

        }
        else
            Debug.Log("Someone not ready!");
 



       
    }





}

[System.Serializable]
public class Message
{
    public string text;
    public TMP_Text textObject;
    public MessageType messageType;

    public enum MessageType
    {
        playerMessage,
        info,
        hostMessage,
        myMessage,
        otherPlayerMessage
        
    }


    
}



