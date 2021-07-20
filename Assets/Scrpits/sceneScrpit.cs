using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class sceneScrpit : NetworkBehaviour
{
    public Text canvasStatusText;
    public playerScript ps;


    //在需要联网的变量前调用syncvar字段
    //hook 后是调用的函数。statusText中储存的是玩家的一些信息
    //比如进入游戏，打招呼等。
    //当有这些消息传入时，会更新statusText中的内容，同时调用hook后的函数
    [SyncVar(hook = nameof(OnstatusTextChanged))]
    public string statusText;

    private void OnstatusTextChanged(string oldStr, string newStr)
    {
        canvasStatusText.text = statusText;
    }

    public void ButtonSendMessage()
    {
        if(ps != null)
        {
            ps.CmdSendPlayerMessage();
        }
    }

}


