using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Mirror;

public class waitingHallMainMenu : MonoBehaviour
{

    public Image mask;
    private float alpha;
    public GameObject maskObject;
    public TMP_Text player1Name, player2Name, player3Name, player4Name;
    public GameObject player1, player2, player3, player4;
    public int playerPosition;
    public string mapName = "map1";
    public waintingHallPlayerScrpit whPs;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
        resetPlayerButtonName();
        player1Name.text = PlayerPrefs.GetString("UserName");
        playerPosition = 1;
        PlayerPrefs.SetInt("userPosition1", 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    
    public void Player1Button()
    {
        whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        if (whPs.player1Img > 0 && whPs.isLocalPlayer)
        {
            if (whPs.player1Img < 4)
            {
                //int i = whPs.player1Img+1;
                //whPs.CmdSetupPlayerImg("ishost", i);
                whPs.player1Img++;
            }
            else
            {
                //whPs.CmdSetupPlayerImg("ishost", 1);
                whPs.player1Img = 1;
            }
        }

    }
    public void Player2Button()
    {
        whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        if (whPs.player2Img > 0 && whPs.isLocalPlayer)
        {
            if (whPs.player2Img < 4)
            {
                int i = whPs.player2Img + 1;
                whPs.CmdSetupPlayerImg(PlayerPrefs.GetString("UserName"), i);
            }
            else
            {
                whPs.CmdSetupPlayerImg(PlayerPrefs.GetString("UserName"), 1);
            }
        }
    }
    public void Player3Button()
    {
        whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        if (whPs.player3Img > 0 && whPs.isLocalPlayer)
        {
            if (whPs.player3Img < 4)
            {
                int i = whPs.player3Img + 1;
                whPs.CmdSetupPlayerImg(PlayerPrefs.GetString("UserName"), i);
            }
            else
            {
                whPs.CmdSetupPlayerImg(PlayerPrefs.GetString("UserName"), 1);
            }
        }
    }
    public void Player4Button()
    {
        whPs = NetworkClient.connection.identity.GetComponent<waintingHallPlayerScrpit>();
        if (whPs.player4Img > 0 && whPs.isLocalPlayer)
        {
            if (whPs.player4Img < 4)
            {
                int i = whPs.player4Img + 1;
                whPs.CmdSetupPlayerImg(PlayerPrefs.GetString("UserName"), i);
            }
            else
            {
                whPs.CmdSetupPlayerImg(PlayerPrefs.GetString("UserName"), 1);
            }
        }
    }

    public void resetPlayerButtonName()
    {
        switch (playerPosition)
        {
            case 0:
                player1Name.text = "";
                player2Name.text = "";
                player3Name.text = "";
                player4Name.text = "";
                break;
            case 1:
                player1Name.text = "";
                break;
            case 2:
                player2Name.text = "";
                break;
            case 3:
                player3Name.text = "";
                break;
            case 4:
                player4Name.text = "";
                break;
            default:
                break;
        }
    }

    public void startButton()
    {
        StartCoroutine(FadeOut(mapName));
    }

    public void quitButton()
    {
        StartCoroutine(FadeOut());
    }


    IEnumerator FadeIn()
    {
        alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            mask.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);
        }
        maskObject.SetActive(false);
    }


    IEnumerator FadeOut(string sceneName)
    {
        maskObject.SetActive(true);
        alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            mask.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeOut()
    {
        maskObject.SetActive(true);
        alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            mask.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                                Application.Quit();
     
        #endif

    }
}
