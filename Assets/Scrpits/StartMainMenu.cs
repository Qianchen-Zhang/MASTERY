using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class StartMainMenu : MonoBehaviour
{
    public Image mask;
    private float alpha;
    public GameObject maskObject;
    public GameObject userName;
    public GameObject PassWord;
    public GameObject logMessage;

    public void Start()
    {
        StartCoroutine(FadeIn());
        
    }


    public void StartButton()
    {
        StartCoroutine(FadeOut("waitingHall"));
    }


    public void QuitButton()
    {
        StartCoroutine(FadeOut());
       
    }

    public void SettingButton()
    {      
        StartCoroutine(FadeOut("setting"));        
    }

    public void LogInButton()
    {
        bool userNameFull = false;
        bool passWordFull = false;
        if (!userName.GetComponent<TMP_InputField>().text.Equals(""))
        {
            userNameFull = true;
            PlayerPrefs.SetString("UserName", userName.GetComponent<TMP_InputField>().text);
            Debug.LogWarning("You are input your name!");
        }
        else
        {
            Debug.LogWarning("You are not input your name!");
        }
        if (!PassWord.GetComponent<TMP_InputField>().text.Equals(""))
        {
            passWordFull = true;
            PlayerPrefs.SetString("PassWord", PassWord.GetComponent<TMP_InputField>().text);
        }
        else
        {
            Debug.LogWarning("You are not input your passWord!");
        }

        if (userNameFull && passWordFull)
        {
            StartCoroutine(FadeOut("waitingHall"));
        }



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
        while (alpha<1)
        {
            alpha += Time.deltaTime;
            mask.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        //if (sceneName.Equals("waitingHall"))
        //{
           // NetworkManager.singleton.ServerChangeScene("waitingHall");
        //}
        //else
        //{
            SceneManager.LoadScene(sceneName); 
        //}
        


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
