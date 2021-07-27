using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.UI;


public class waintingHallPlayerScrpit : NetworkBehaviour
{

    [SyncVar]
    public string hostName;

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

    
    public bool p1Ready, p2Ready, p3Ready, p4Ready;

    


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
                            p4Ready = false;
                        }
                    }
                    else
                    {
                        position3 = true;
                        player3Name = str;
                        p3Ready = false;
                    }
                }
                else
                {
                    position2 = true;
                    player2Name = str;
                    p2Ready = false;
                }
            }
            else
            {
                position1 = true;
                player1Name = str;
                p1Ready = false;
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

        if (str.Equals("ishost"))
        {
            player1Img = i;
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

    //[Command]
    public void CmdReady(string str)
    {
        if (str.Equals(player1Name))
        {
            if(p1Ready == false)
            {
                p1Ready = true;
                lbwindow.startBtn.GetComponent<Image>().color = new Color(255f,0f,0f);
            }
            else
            {
                p1Ready = false;
                lbwindow.startBtn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
            }
           
        }
        if (str.Equals(player2Name))
        {
            if (p2Ready == false)
            {
                p2Ready = true;
                lbwindow.startBtn.GetComponent<Image>().color = new Color(255f, 0f, 0f);
            }
            else
            {
                p2Ready = false;
                lbwindow.startBtn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
            }
        }
        if (str.Equals(player3Name))
        {
            if (p3Ready == false)
            {
                p3Ready = true;
                lbwindow.startBtn.GetComponent<Image>().color = new Color(255f, 0f, 0f);
            }
            else
            {
                p3Ready = false;
                lbwindow.startBtn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
            }
        }
        if (str.Equals(player4Name))
        {
            if (p4Ready == false)
            {
                p4Ready = true;
                lbwindow.startBtn.GetComponent<Image>().color = new Color(255f, 0f, 0f);
            }
            else
            {
                p4Ready = false;
                lbwindow.startBtn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
            }
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
        if(player1Img > 0)
        {
            lbwindow.player1Btn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
        }
        else
        {
            lbwindow.player1Btn.GetComponent<Image>().color = new Color(0f, 0f, 0f ,0f);
        }

        if (player2Img > 0)
        {
            lbwindow.player2Btn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
        }
        else
        {
            lbwindow.player2Btn.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }

        if (player3Img > 0)
        {
            lbwindow.player3Btn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
        }
        else
        {
            lbwindow.player3Btn.GetComponent<Image>().color = new Color(0f,0f,0f,0f);
        }

        if (player4Img > 0)
        {
            lbwindow.player4Btn.GetComponent<Image>().color = new Color(255f, 255f, 255f);
        }
        else
        {
            lbwindow.player4Btn.GetComponent<Image>().color = new Color(0f, 0f, 0f,0f);
        }



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


    [ClientRpc]
    public void RpcGamePrefabs()
    {
        lbwindow.net.playerPrefab = lbwindow.gamePlayerPrefab;
    }
    
    


    public override void OnStartServer()
    {
        base.OnStartServer();
        hostName = PlayerPrefs.GetString("UserName");
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (isLocalPlayer)
        {
            if (!userName.Equals(hostName))
            {
                //waitingHallMainMenu wHMM = FindObjectOfType<waitingHallMainMenu>();
                lbwindow.StartBtn.text = "Ready";
                //lbwindow.startBtn.onClick.RemoveListener(wHMM.startButton);
                lbwindow.startBtn.onClick.AddListener(lbwindow.ReadyBtn);
                

            }
            else
            {
                lbwindow.startBtn.onClick.AddListener(lbwindow.StartButtonFonction);
                
            }
        }
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

    public bool getUserReadyState(string name)
    {
        bool state = false;
        if(name.Equals(player1Name))
        {
            state = p1Ready;
        }
        if (name.Equals(player2Name))
        {
            state = p2Ready;
        }
        if (name.Equals(player3Name))
        {
            state = p3Ready;
        }
        if (name.Equals(player4Name))
        {
            state = p4Ready;
        }
        return state;
    }

    




}
