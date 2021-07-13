using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setMenu : MonoBehaviour
{
    public GameObject confirmPromptInterface;
    private bool isChange = false;
    private bool isConfirm ;

   


    public void confirmSet()
    {
        isConfirm = true;
    }


    public void gotoStartMainMenu()
    {
        //if (!isChange) isConfirm = true; //当功能完成时设置

        if(isConfirm) SceneManager.LoadScene("start");
        else confirmPromptInterface.SetActive(true);
    }

    public void yesButton()
    {
        isConfirm = true;
        confirmPromptInterface.SetActive(false);
    }

    public void noButton()
    {
        SceneManager.LoadScene("start");
    }

}
