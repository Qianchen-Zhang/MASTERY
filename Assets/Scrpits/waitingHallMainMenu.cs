using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class waitingHallMainMenu : MonoBehaviour
{

    public Image mask;
    private float alpha;
    public GameObject maskObject;
    public TMP_Text player1Name, player2Name, player3Name, player4Name;
    public GameObject player1, player2, player3, player4;
    public int playerPosition;
    public string mapName = "map1";
    

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
        resetPlayerButtonName();
        player1Name.text = PlayerPrefs.GetString("UserName");
        playerPosition = 1;
        PlayerPrefs.SetInt("userPosition1", 1);
    }
    public void Player2Button()
    {
        resetPlayerButtonName();
        player2Name.text = PlayerPrefs.GetString("UserName");
        playerPosition = 2;
        PlayerPrefs.SetInt("userPosition2", 2);
    }
    public void Player3Button()
    {
        resetPlayerButtonName();
        player3Name.text = PlayerPrefs.GetString("UserName");
        playerPosition = 3;
        PlayerPrefs.SetInt("userPosition3", 3);
    }
    public void Player4Button()
    {
        resetPlayerButtonName();
        player4Name.text = PlayerPrefs.GetString("UserName");
        playerPosition = 4;
        PlayerPrefs.SetInt("userPosition4", 4);
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
