using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class lobbyWindow : NetworkBehaviour
{
    public int maxMessage = 25;

    public bool selectChatInput = false;

    public TMP_InputField chatMessage;
    //public TMP_Text chatHistory;
    //public Scrollbar scrollbar;
    public GameObject chatPanel, textObject;
    public TMP_Text userName;

    public Color32 playerMessage, info;


    public playerScrpit ps;

    [SyncVar(hook = nameof(OnStatusMsgChanged))]
    public Message otherMsg;

    
    [SerializeField]
    List<Message> messageList = new List<Message>();

    public void Start()
    {
        userName.text = PlayerPrefs.GetString("UserName") + ":";
        SendMessageToChat("[" + System.DateTime.Now + "] " + PlayerPrefs.GetString("UserName") 
            + " join the game",Message.MessageType.playerMessage);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && selectChatInput && chatMessage.text != "")
        {
            SendMessageToChat("[" + System.DateTime.Now +"] " +PlayerPrefs.GetString("UserName") + ": "
                + chatMessage.text,Message.MessageType.info);
            chatMessage.text = "";
            
            Debug.Log(messageList.Count);
        }



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

        Message newMessage = new Message();
        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<TMP_Text>();

        newMessage.textObject.text = newMessage.text;

        newMessage.textObject.faceColor = MessageTypeColor(messageType);

        ps.CmdSendPlayerMessage();

        messageList.Add(newMessage);
    }

    public Color32 MessageTypeColor(Message.MessageType messageType)
    {
        Color32 color = info;

        switch (messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;
           
        }

        return color;
    }

    public void OnStatusMsgChanged()
    {
        SendMessageToChat(otherMsg.text,otherMsg.messageType);
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
        hostMessage
        
    }

}
