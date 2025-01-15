using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource eventSound;
    [SerializeField]
    private AudioSource musicSound;
    [SerializeField]
    private AudioSource outdoorSound;

    [SerializeField]
    private AudioClip start;
    [SerializeField]
    private AudioClip handBrake;
    [SerializeField]
    private AudioClip horn;
    [SerializeField]
    private AudioClip music;


    public IEnumerator ArrivalSound()
    {
        eventSound.clip = handBrake;
        eventSound.Play();
        yield return new WaitForSeconds(0.5f);
        eventSound.clip = horn;
        eventSound.Play();
        yield return new WaitForSeconds(0.5f);
        eventSound.clip = horn;
        eventSound.Play();
        yield return new WaitForSeconds(0.5f);
        eventSound.clip = horn;
        eventSound.Play();
    }


    public void StartSound()
    {
        eventSound.clip = start;
        eventSound.Play();
    }

}
