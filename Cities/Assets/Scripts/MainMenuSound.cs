using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Script pour gérer le son de la scène MainMenu
public class MainMenuSound : MonoBehaviour
{
    // Références aux audio sources de la scène
    [SerializeField]
    private AudioSource outdoorSound;
    [SerializeField]
    private AudioSource doorSound;
    [SerializeField]
    private AudioSource beltSound;
    [SerializeField]
    private AudioSource startSound;

    // La suite sonore de la scène est jouée à son chargement
    void Start()
    {
        StartCoroutine(GetInCar());
    }

    // La coroutine de la suite sonore de la scène
    IEnumerator GetInCar()
    {
        outdoorSound.loop = true;
        outdoorSound.Play();
        yield return new WaitForSeconds(1.0f);
        doorSound.Play();
        outdoorSound.volume = 0.1f;
        yield return new WaitForSeconds(2.0f);
        beltSound.Play();
    }

    // Le son de démarrage est joué en appuyant le bouton Start
    public void StartSound()
    {
        startSound.Play();
    }

}
