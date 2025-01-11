using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource outdoorSound;
    [SerializeField]
    private AudioSource engineSound;
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
    [SerializeField]
    private AudioClip engine;
    [SerializeField]
    private AudioClip gasUp;
    [SerializeField]
    private AudioClip handBrake;
    [SerializeField]
    private AudioClip horn;

    void Start()
    {
        StartCoroutine(GetInTheCar());
    }


    public IEnumerator GetInTheCar()
    {
        outdoorSound.clip = exterior;
        outdoorSound.loop = true;
        outdoorSound.Play();
        yield return new WaitForSeconds(2);
        eventSound.clip = door;
        eventSound.Play();
        outdoorSound.volume = 0.2f;
        yield return new WaitForSeconds(1);
        eventSound.clip = belt;
        eventSound.Play();
    }

    public void SoundStart()
    {
        eventSound.clip = start;
        eventSound.Play();
        engineSound.clip = engine;
        engineSound.loop = true;
        engineSound.Play();
        engineSound.volume = 1.0f;
    }

    public void AccelerateSound()
    {
        eventSound.clip = horn;
        eventSound.volume = 1.0f;
        eventSound.Play();
        // StartCoroutine(GasUp());
    }

    public IEnumerator GasUp()
    {
        eventSound.clip = engine;
        eventSound.volume = 5.0f;
        eventSound.Play();
        
        yield return new WaitForSeconds(1);

        engineSound.volume = 1.0f;
    }

}
