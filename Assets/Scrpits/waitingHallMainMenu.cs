using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class waitingHallMainMenu : MonoBehaviour
{

    public Image mask;
    private float alpha;
    public GameObject maskObject;
    public string mapName = "map1";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerButton()
    {

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
