using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class setMenu : MonoBehaviour
{
    public GameObject confirmPromptInterface;
    private bool isChange = false;
    private bool isConfirm ;

    public Image mask;
    private float alpha;
    public GameObject maskObject;


    public void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void confirmSet()
    {
        isConfirm = true;
    }


    public void gotoStartMainMenu()
    {
        //if (!isChange) isConfirm = true; //当功能完成时设置

        if(isConfirm) StartCoroutine(FadeOut("start"));
        else confirmPromptInterface.SetActive(true);
    }

    public void yesButton()
    {
        isConfirm = true;
        confirmPromptInterface.SetActive(false);
    }

    public void noButton()
    {
        StartCoroutine(FadeOut("start"));
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
