using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


// Script pour gérer le son de la scène GameScene
public class GameSceneSound : MonoBehaviour
{
    // Référence aux script d'interface
    [SerializeField]
    private IHM ihm;

    // Référence à l'audio mixer
    public AudioMixer audioMixer;
    // Référence au slider pour gérer le volume de musique
    public Slider musicVolume;
    
    // Référence au game object contenant l'audio source Gas
    [SerializeField]
    private AudioSource gas;

    // La musique est lancée au chargement de la cène (Play On Awake est coché dans l'inspector). La musique est jouée au volume minimal possible pour simuler le mode Mute.
    void Start()
    {
        MusicMute();
    }

    // Fonction pour définir le paramètre exposé de l'audio mixer MusicVolume à 0dB soit le volume natif du fichier .mp3
    public void MusicUnmute()
    {
        audioMixer.SetFloat("MusicVolume", 0);
    }

    // Fonction pour définir le paramètre exposé de l'audio mixer MusicVolume à -80dB soit prèsque muet
    public void MusicMute()
    {
        audioMixer.SetFloat("MusicVolume", -80);
    }

    // Cette fonction est associé dans l'inspector du game object Slider à l'événement OnValueChanged avec le paramètre float dynamique. Elle relie la valeur du game object Slider (configuré dans l'inspector de -20 à 20) au slider du groupe Music de l'audio mixer. Après la manipulation du slider, le focus est remis sur le champ InputField, pour éviter que la manipulation de l'autoradio interfère avec le déroulement principal du jeu.
    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", sliderValue);
        ihm.FocusInputField();
    }

    // Cette fonction joue le son d'accélérateur. PlayOneShot plutôt que Play() permet de régler le volume directement dans le script.
    public void GasUp()
    {
        gas.PlayOneShot(gas.clip, 0.1f);
    }

}
