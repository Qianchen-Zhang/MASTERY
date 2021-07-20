using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerScript : NetworkBehaviour
{

    public GameObject floatingInfo;
    public TextMesh nameText;

    private Material playerMaterialClone;

    private sceneScrpit sCript;


    //��Ҫ����ҵ����ֺ���ɫͬ�����������
    //������Ҫһ��ͬ���ֶ�

    [SyncVar(hook = nameof(onPlayerNameChanged))]
    private string playerName;

    [SyncVar(hook = nameof(onPlayerColorChanged))]
    private Color playerColor;

    private void onPlayerNameChanged(string oldStr, string newStr)
    {
        nameText.text = newStr;
    }

    private void onPlayerColorChanged(Color oldCol, Color newCol)
    {
        nameText.color = newCol;

        playerMaterialClone = new Material(GetComponent<Renderer>().material);
        playerMaterialClone.SetColor("_Color", newCol);

        GetComponent<Renderer>().material = playerMaterialClone;
    }

    //��Է����ı�ǣ� ��������Cmd��ͷ
    [Command]
    private void CmdSetupPlayer(string nameValue, Color colorValue)
    {
        playerName = nameValue;
        playerColor = colorValue;
        sCript.statusText = $"{playerName} joined.";

    }

    [Command]
    public void CmdSendPlayerMessage()
    {
        if (sCript)
        {
            sCript.statusText = $"{playerName} says hello {Random.Range(1, 99)}";
        }
    }


    private void Awake()
    {
        sCript = FindObjectOfType<sceneScrpit>();
    }


    public override void OnStartLocalPlayer()
    {
        sCript.ps = this;
        //�������player��
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = Vector3.zero;

        floatingInfo.transform.localPosition = new Vector3(0f, 0f, -.6f);
        floatingInfo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        ChangePlayerNameAndColor();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            //ʹ��������ҵ�����һֱ�����Լ������
            Transform t = Camera.main.transform;
            t.localScale.Set(-1f, 1f, 1f);

            floatingInfo.transform.LookAt(t);

            //floatingInfo.transform.LookAt(Camera.main.transform);




            return;
        }

        var moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110f;
        var moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4.0f;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);


        if (Input.GetKey(KeyCode.C))
        {
            ChangePlayerNameAndColor();
        }

    }

    private void ChangePlayerNameAndColor()
    {
        var tempName = $"Player {Random.Range(1, 999)}";
        var tempColor = new Color
        (
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            1
         );

        CmdSetupPlayer(tempName, tempColor);
    }
}
