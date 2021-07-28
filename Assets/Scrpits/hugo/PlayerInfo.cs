using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class PlayerInfo : MonoBehaviour
{
    public int maxMessage = 25;
    public Scrollbar scrollbar;

    public bool selectChatInput = false;

    public TMP_InputField chatMessage;

    public GameObject chatPanel, textObject;

    public TMP_Text userName;

    public Color playerMessageCol, infoCol, myMessageCol, otherPlayerMessageCol,hostMessageCol;

    public GameObject lobbyWindow;

    public PlayerSetup ps;

    public GameObject canvas;
    public TMP_Text canavsStatusText;

    public GameObject readyBtn;

    public bool isReady;

    public NetworkManager net;

    [SerializeField]
    List<Message> messageList = new List<Message>();


    private void Awake()
    {
        PlayerSetup.OnMessage += OnPlayerMessage;
        
    }

    private void Start()
    {
        userName.text = PlayerPrefs.GetString("UserName") + ":";
        isReady = false;
    }

   

    public void OnPlayerMessage( PlayerSetup ps, string message)
    {
        if (!ps.isLocalPlayer)
            SendMessageToChat(message,Message.MessageType.otherPlayerMessage);

        Debug.Log(message);
    }


    public void selectChatInputField()
    {
        selectChatInput = true;
    }

    public void deselectChatInputField()
    {
        selectChatInput = false;
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (lobbyWindow.activeSelf)
            {
                lobbyWindow.SetActive(false);
                //readyBtn.SetActive(false);

            }
            else
            {
                lobbyWindow.SetActive(true);
                //readyBtn.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (lobbyWindow.activeSelf && chatMessage.text.Equals("") )
            {
                chatMessage.OnControlClick();
            }

            if (lobbyWindow.activeSelf && chatMessage.text != "" && selectChatInput)
            {

                ps = NetworkClient.connection.identity.GetComponent<PlayerSetup>();
                SendMessageToChat("[" + System.DateTime.Now + "] " + PlayerPrefs.GetString("UserName") + ": "
                + chatMessage.text.Trim(), Message.MessageType.myMessage);

                ps.CmdSendPlayerMessage("[" + System.DateTime.Now + "] " + PlayerPrefs.GetString("UserName") + ": "
                + chatMessage.text.Trim());

                chatMessage.text = "";
                Debug.Log(messageList.Count);
            }
        }

        scrollbar.value = 0;
        
    }



    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if(messageList.Count >= maxMessage)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        //Message newMessage = new Message();
        //newMessage.text = text;
        //newMessage.messageType = messageType;

        //GameObject newText = Instantiate(textObject, chatPanel.transform);

        //newMessage.textObject = newText.GetComponent<TMP_Text>();

        //newMessage.textObject.text = newMessage.text;

        //newMessage.textObject.faceColor = MessageTypeColor(messageType);

        //messageList.Add(newMessage);

        messageList.Add(generateMsg(text, messageType));

    }


    public Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color = infoCol;

        switch (messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessageCol;
                break;
            case Message.MessageType.myMessage:
                color = myMessageCol;
                break;
            case Message.MessageType.otherPlayerMessage:
                color = otherPlayerMessageCol;
                break;
            case Message.MessageType.hostMessage:
                color = hostMessageCol;
                break;
        }
        return color;
    }

    public Message generateMsg(string text, Message.MessageType msgType)
    {
        Message newMessage = new Message();
        newMessage.text = text;
        newMessage.messageType = msgType;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<TMP_Text>();

        newMessage.textObject.text = newMessage.text;

        newMessage.textObject.faceColor = MessageTypeColor(msgType);

        return newMessage;
    }

    public void readyButtonFunction()
    {
        ps = NetworkClient.connection.identity.GetComponent<PlayerSetup>();
        if (ps.isLocalPlayer)
        {
            if (isReady)
            {
                isReady = false;
                readyBtn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
                ps.CmdSetPlayerStatusText(PlayerPrefs.GetString("UserName") + " cancel Ready");
                ps.CmdSetupPlayerReadyNum(-1);
            }
            else
            {
                isReady = true;
                readyBtn.GetComponent<Image>().color = new Color(255f, 0f, 0f);
                ps.CmdSetPlayerStatusText(PlayerPrefs.GetString("UserName") + " Ready");
                ps.CmdSetupPlayerReadyNum(1);
                if (ps.isServer)
                {
                   
                    
                    NetworkManager.singleton.ServerChangeScene("waitingHall1");
                }
            }
        }

    }

}

[System.Serializable]
public class Mesage
{
    public string text;
    public TMP_Text textObject;
    public MessageType messageType;

    public enum MessageType
    {
        playerMessage, //暂未使用
        info, //玩家游戏信息， 例如：。。。进入游戏， 。。。退出游戏 ， 。。。是主机， 
        hostMessage, // 主机信息
        myMessage, //自己玩家发送的信息
        otherPlayerMesage //其他玩家发送的信息
    }

}
