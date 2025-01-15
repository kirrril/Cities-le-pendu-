using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource outdoorSound;
    [SerializeField]
    private AudioSource eventSound;

    [SerializeField]
    private AudioClip exterior;
    [SerializeField]
    private AudioClip door;
    [SerializeField]
    private AudioClip belt;
    [SerializeField]
    private AudioClip start;


    public IEnumerator GetInTheCar()
    {
        outdoorSound.clip = exterior;
        outdoorSound.loop = true;
        outdoorSound.Play();
        yield return new WaitForSeconds(0.1f);
        eventSound.clip = door;
        eventSound.Play();
        outdoorSound.volume = 0.2f;
        yield return new WaitForSeconds(1);
        eventSound.clip = belt;
        eventSound.Play();
    }

    public void StartSound()
    {
        eventSound.clip = start;
        eventSound.Play();
    }

}
