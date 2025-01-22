using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script pour gérer la logique de la scène GameOver
public class GameOverController : MonoBehaviour
{
    // Référence au script qui gère le son de la scène
    public GameOverSound gameOverSound;

    // Recharge la scène du jeu après avoir joué le son de démarrage. Est appelé par l'event OnClick du bouton Start.
    public void PlayAgain()
    {
        StartCoroutine(PlayAndSound());
    }

    // Coroutine pour charger la scène du jeu
    IEnumerator PlayAndSound()
    {
        gameOverSound.StartSound();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("GameScene");
    }

    // Quitte l'application à l'appui sur le bouton Stop
    public void Quit()
    {
        // Pour quitter la play mode de l'éditeur Unity
    #if UNITY_EDITOR

        EditorApplication.isPlaying = false;

        // Pour quitter l'application autonome
    #elif UNITY_STANDALONE
        
        Application.Quit();

    #endif
    }

}
