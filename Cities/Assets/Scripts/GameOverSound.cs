using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script pour gérer le son de la scène GameOver
public class GameOverSound : MonoBehaviour
{
    // Références aux audio sources de la scène
    [SerializeField]
    private AudioSource outdoorSound;
    [SerializeField]
    private AudioSource handBrakeSound;
    [SerializeField]
    private AudioSource hornSound;
    [SerializeField]
    private AudioSource startSound;


    // Jouer une suite sonore au chargement de la scène
    void Start()
    {
        StartCoroutine(ArrivalSound());
    }


    // La suite sonore d'arrivée, la même pour la victoire et la défaite
    private IEnumerator ArrivalSound()
    {   
        handBrakeSound.Play();
        yield return new WaitForSeconds(0.01f);
        outdoorSound.loop = true;
        outdoorSound.Play();
        yield return new WaitForSeconds(1);
        hornSound.Play();
        yield return new WaitForSeconds(0.5f);
        hornSound.Play();
        yield return new WaitForSeconds(0.5f);
        hornSound.Play();
    }


    // Son du bouton de démarrage
    public void StartSound()
    {
        startSound.Play();
    }

}
