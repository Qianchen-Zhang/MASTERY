using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.UI;

public class waintingHallPlayerScrpit : NetworkBehaviour
{

    [SyncVar]
    private string hostName;

    public static event Action<waintingHallPlayerScrpit, string> OnMessage;
    //public static event Action<waintingHallPlayerScrpit, string> OnPlayerSetup;

    public lobbyWindow lbwindow;

    public string userName;

    [SyncVar]
    public bool position1, position2, position3, position4;

    [SyncVar(hook = nameof(setupPlayer))]
    public string player1Name, player2Name, player3Name, player4Name;

    [SyncVar(hook = nameof(setupPlayerImg))]
    public int player1Img, player2Img, player3Img, player4Img;

    


    private void Awake()
    {
        //playerName = PlayerPrefs.GetString("UserName");
        lbwindow = FindObjectOfType<lobbyWindow>();
        userName = PlayerPrefs.GetString("UserName");
        player1Name = userName;
        position1 = true;
        position2 = false;
        position3 = false;
        position4 = false;
        player1Img = 1;
        player2Img = 0;
        player3Img = 0;
        player4Img = 0;
    }

    //改变玩家名字
    [Command]
    public void CmdSetupPlayer(string str)
    {
        //if (str.Trim() != "")
        //RpcShowName(str.Trim());
        if (!isLocalPlayer)
        {
            if (position1)
            {
                if (position2)
                {
                    if (position3)
                    {
                        if (position4)
                        {
                            Debug.Log("This room is full !");
                        }
                        else
                        {
                            position4 = true;
                            player4Name = str;
                        }
                    }
                    else
                    {
                        position3 = true;
                        player3Name = str;
                    }
                }
                else
                {
                    position2 = true;
                    player2Name = str;
                }
            }
            else
            {
                position1 = true;
                player1Name = str;
            }
        }
    }

    [Command]
    public void CmdSetupPlayerImg(string str,int i)
    {
        if (!isLocalPlayer)
        {
            if (str.Equals(player1Name))
            {
                player1Img = i;
            }
            if (str.Equals(player2Name))
            {
                player2Img = i;
            }
            if (str.Equals(player3Name))
            {
                player3Img = i;
            }
            if (str.Equals(player4Name))
            {
                player4Img = i;
            }
        }
       
    }

    

    [Command]
    public void DeletePlayer(string str)
    {
        if (str.Equals(player1Name))
        {
            player1Name = "";
            position1 = false;
            player1Img = 0;
        }
        if (str.Equals(player2Name))
        {
            player2Name = "";
            position2 = false;
            player2Img = 0;
        }
        if (str.Equals(player3Name))
        {
            player3Name = "";
            position3 = false;
            player3Img = 0;
        }
        if (str.Equals(player4Name))
        {
            player4Name = "";
            position4 = false;
            player4Img = 0;
        }

    }

    public void setupPlayer(string oldStr, string newStr)
    {
        lbwindow.player1.text = player1Name;
        lbwindow.player2.text = player2Name;
        lbwindow.player3.text = player3Name;
        lbwindow.player4.text = player4Name;
    }

    public void setupPlayerImg(int oldInt, int newInt)
    {
        lbwindow.player1Btn.GetComponent<Image>().sprite = lbwindow.img[player1Img];
        lbwindow.player2Btn.GetComponent<Image>().sprite = lbwindow.img[player2Img];
        lbwindow.player3Btn.GetComponent<Image>().sprite = lbwindow.img[player3Img];
        lbwindow.player4Btn.GetComponent<Image>().sprite = lbwindow.img[player4Img];

        

    }


    [ClientRpc]
    public void RpcShowName(string str)
    {
        //OnPlayerSetup?.Invoke(this, str);
        
    }



    //玩家发送消息
    [Command]
    public void CmdSendPlayerMessage(string str)
    {
        if(str.Trim() != "")
        RpcShowMessage(str.Trim());
    }

    [ClientRpc]
    public void RpcShowMessage(string str)
    {
        OnMessage?.Invoke(this,str);
    }

    
    


    public override void OnStartServer()
    {
        base.OnStartServer();
        hostName = PlayerPrefs.GetString("UserName");
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        lbwindow.SendMessageToChat("[" + System.DateTime.Now + "] " + hostName
            + " is host", Message.MessageType.info);
        lbwindow.SendMessageToChat("[" + System.DateTime.Now + "] " + userName
            + " join the game", Message.MessageType.info);
        CmdSendPlayerMessage("[" + System.DateTime.Now + "] " + userName
            + " join the game");        
        CmdSetupPlayer(userName);
        CmdSetupPlayerImg(userName,1);
    }


    public override void OnStopClient()
    {
        //lbwindow.SendMessageToChat("[" + System.DateTime.Now + "] " + userName
        //  + " leave the game",Message.MessageType.info);
        //DeletePlayer(userName);  
    }

    




}
