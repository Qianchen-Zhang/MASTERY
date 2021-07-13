using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMainMenu : MonoBehaviour
{
    

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
     
        #endif
    }

    public void goToSettingMenu()
    {
        SceneManager.LoadScene("setting");
    }
    

}
