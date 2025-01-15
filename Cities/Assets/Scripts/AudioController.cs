using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource outdoorSound;
    [SerializeField]
    private AudioSource engineSound;
    [SerializeField]
    private AudioSource eventSound;
    [SerializeField]
    private AudioSource musicSound;

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
    private AudioClip handBrake;
    [SerializeField]
    private AudioClip horn;
    [SerializeField]
    private AudioClip music;
    

    public static AudioController instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
        
    }


    void Start()
    {
        StartCoroutine(GetInTheCar());
    }


    public IEnumerator GetInTheCar()
    {
        outdoorSound.clip = exterior;
        outdoorSound.loop = true;
        outdoorSound.Play();
        yield return new WaitForSeconds(1);
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
        // engineSound.clip = engine;
        // engineSound.loop = true;
        
        // engineSound.volume = 1.0f;
    }

    public void EngineSound()
    {
        engineSound.clip = engine;
        engineSound.Play();
    }

    public void AccelerateSound()
    {
        // eventSound.clip = horn;
        // eventSound.volume = 1.0f;
        // eventSound.Play();
    }

    // public IEnumerator GasUp()
    // {
    //     eventSound.clip = engine;
    //     eventSound.volume = 5.0f;
    //     eventSound.Play();

    //     yield return new WaitForSeconds(1);

    //     engineSound.volume = 1.0f;
    // }

}
