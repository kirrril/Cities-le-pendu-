using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource engineSound;
    [SerializeField]
    private AudioSource eventSound;
    [SerializeField]
    private AudioSource musicSound;

    [SerializeField]
    private AudioClip engine;
    [SerializeField]
    private AudioClip gas;
    [SerializeField]
    private AudioClip horn;
    [SerializeField]
    private AudioClip music;
    

    public void EngineSound()
    {
        engineSound.clip = engine;
        engineSound.loop = true;
        engineSound.volume = 1;
        engineSound.Play();
    }

    public void AccelerateSound()
    {
        eventSound.clip = gas;
        engineSound.loop = false;
        engineSound.pitch = 1;
        eventSound.volume = 0.5f;
        eventSound.Play();
        EngineSound();
    }

}
