using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// Script pour gérer la logique de la scène MainMenu
public class MainMenuController : MonoBehaviour
{
    // Référence au script qui gère le son de la scène
    public MainMenuSound mainMenuSound;

    // A l'appui du bouton Start, le son de démarrage est jouée et la scène de jeu est chargée
    public void Play()
    {   
        StartCoroutine(PlayAndSound());
    }

    // La coroutine du bouton Start
    IEnumerator PlayAndSound()
    {
        mainMenuSound.StartSound();
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("GameScene");
    }

    // Fonction du bouton Stop qui permet de quitter l'application
    public void Quit()
    {
        // Pour quitter le mode play de l'éditeur Unity
        #if UNITY_EDITOR

        EditorApplication.isPlaying = false;

        // Pour quitter l'application autonome
        #elif UNITY_STANDALONE
        
        Application.Quit();

        #endif
    }

}
