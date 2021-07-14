using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerScrpit : NetworkBehaviour
{

    public lobbyWindow lw;

    public override void OnStartLocalPlayer()
    {
        lw.ps = this;
        base.OnStartLocalPlayer();

        //…„œÒª˙”ÎPlayer∞Û∂®
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = Vector3.zero;
    }

    private void Awake()
    {
        lw = FindObjectOfType<lobbyWindow>();
    }

    [Command]
    public void CmdSetupPlayer()
    {
        lw.otherMsg = lw.messageL
        
    }

    [Command]
    public void CmdSendPlayerMessage()
    {
        lw.SendMessageToChat("[" + System.DateTime.Now + "] " + PlayerPrefs.GetString("UserName") + ": "
                + lw.chatMessage.text, Message.MessageType.info);
    }
}
