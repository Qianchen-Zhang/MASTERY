using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class sceneScrpit : NetworkBehaviour
{
    public Text canvasStatusText;
    public playerScript ps;


    //����Ҫ�����ı���ǰ����syncvar�ֶ�
    //hook ���ǵ��õĺ�����statusText�д��������ҵ�һЩ��Ϣ
    //���������Ϸ�����к��ȡ�
    //������Щ��Ϣ����ʱ�������statusText�е����ݣ�ͬʱ����hook��ĺ���
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


