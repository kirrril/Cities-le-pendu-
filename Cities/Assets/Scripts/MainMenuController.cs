using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }


    public void Quit()
    {
        #if UNITY_EDITOR

        EditorApplication.isPlaying = false;

        #elif UNITY_STANDALONE
        
        Application.Quit();

        #endif
    }

}
