using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMainMenu : MonoBehaviour
{
    public Image mask;
    private float alpha;
    public GameObject maskObject;

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
