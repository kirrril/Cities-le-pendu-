using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameOverSound gameOverSound;


    void Start()
    {
        StartCoroutine(gameOverSound.ArrivalSound());
    }


    public void PlayAgain()
    {
        StartCoroutine(PlayAndSound());
    }


    IEnumerator PlayAndSound()
    {
        gameOverSound.StartSound();
        yield return new WaitForSeconds(0.7f);
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
