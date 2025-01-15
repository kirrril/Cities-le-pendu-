using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public MainMenuSound mainMenuSound;


    void Start()
    {
        mainMenuSound.StartCoroutine(mainMenuSound.GetInTheCar());
    }


    public void Play()
    {   
        StartCoroutine(PlayAndSound());
    }


    IEnumerator PlayAndSound()
    {
        mainMenuSound.StartSound();
        yield return new WaitForSeconds(0.9f);
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
