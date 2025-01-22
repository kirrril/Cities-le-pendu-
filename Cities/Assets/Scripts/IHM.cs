using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


// Script pour gérer l'interface graphique de la scène GameScene
public class IHM : MonoBehaviour
{
    // Références au game objects UI
    public TMP_InputField inputField;
    public TMP_Text alreadyPlayed;
    public TMP_Text cityToGo;
    public TMP_Text comment;
    public Image fuelTank;
    public Image dashedLine;
    public Button radioPlay;
    public Button radioPause;
    public Image radioLCD;
    public Slider radioSlider;
    public Slider radioSliderPlaceholder;

    // Référence au GameManager
    public GameManager gameManager;

    // Listes de sprites pour le niveau du réservoir et pour l'animation MoveForward()
    public List<Sprite> fuelTankLevel;
    public List<Sprite> dashedLineSprites;


    void Start()
    {
        OnStart();
    }

    // Réglage de l'interface graphique au chargement de la GameScene
    public void OnStart()
    {
        UpdateIHM();
        comment.text = $"ENTREZ UNE LETTRE OU LE NOM DE LA VILLE EN ENTIER";
    }


    public void FocusInputField()
    {
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
    }


    // Affichage du nom de la ville en fonction des lettres devinées
    public void DisplayCityName()
    {
        // Variable locale pour former le nom de la ville avec les lettres masquées et déjà devinées
        string result = string.Empty;

        // D'abord, on vérifie si le jeu a déjà été gagné, auquel cas on affiche le nom de la ville en lettres majuscules séparées par espaces, pour une cohérence visuelle avec le nom de la ville masqué affiché avec les underscores séparés par espaces
        if (GameManager.instance.currentGame.youWin)
        {
            foreach (char letter in GameManager.instance.currentGame.cityToGo.ToUpper())
            {
                result += letter + " ";
            }
        }
        else
        {
            // Pour chaque lettre du nom de ville à deviner, on vérifie si elle est présente dans la liste de lettres jouées. Si la lettre est présente, on l'ajoute à la variable locale de résultat, sinon on ajoute au résultat un underscore. Après chaque lettre, on ajoute un espace.
            foreach (char letter in GameManager.instance.currentGame.cityToGo.ToUpper())
            {
                if (GameManager.instance.currentGame.playedLetters.Contains(letter.ToString()))
                {
                    result += letter;
                }
                else
                {
                    result += "_";
                }

                result += " ";
            }
        }

        // Le résultat est affiché dans le champ de texte prévu à l'affichage du nom de la ville
        cityToGo.text = result;
    }


    // Fonction appelée par l'événement OnEndEdit de l'InputField. Si l'input n'est pas nul, il lance la fonction OnLetterPlayed qui prend userInput comme paramètre.
    public void GetInput()
    {
        string userInput = inputField.text;
        if (userInput != "")
        {
            GameManager.instance.OnLetterPlayed(userInput);
        }
    }


    // Les lettres déjà jouées sont affichées séparées de virgule et espace, après un titre DÉJÀ JOUÉ :
    void DisplayPlayedLetters()
    {
        string result = string.Empty;

        foreach (string letter in GameManager.instance.currentGame.playedLetters)
        {
            result += letter + ", ";
        }

        alreadyPlayed.text = $"DÉJÀ JOUÉ :\n{result}";
    }


    // Les sprites qui visualisent le niveau de carburant sont affichés en fonction du nombre de tentatives restantes. Le nombre de vies est transformé en index du sprite choisi dans la liste de sprites.
    void DisplayFuelTank()
    {
        int index = GameManager.instance.currentGame.life;
        fuelTank.sprite = fuelTankLevel[index];
    }


    // Met à jour l'affichage des paramètres consécutifs après tout changement de leur état
    public void UpdateIHM()
    {
        DisplayCityName();
        DisplayPlayedLetters();
        DisplayFuelTank();
        FocusInputField();
    }


    // Affichage du message de lettre déjà jouée. Appelé par un event dans GameManager.
    public void OnAlreadyPlayed()
    {
        comment.text = $"CETTE LETTRE A DÉJÀ ÉTÉ JOUÉE ...\nESSAYEZ UNE AUTRE";
        FocusInputField();
    }

    // Affichage des messages du nom de la ville devinée ou d'une lettre devinée, en fonction de la variable qui marque une partie gagnée. Appelé par un event dans GameManager.
    public void OnGoodMove()
    {
        if (GameManager.instance.currentGame.youWin == true)
        {
            comment.text = $"VOUS AVEZ DEVINÉ LE NOM DE LA VILLE !";
        }
        else
        {
            comment.text = $"BRAVO ! VOUS AVEZ DEVINÉ UNE LETTRE !\nREJOUEZ ...";
        }

    }

    // Affichage du message de lettre qui n'est pas présente dans le nom de la ville à deviner. Appelé par un event dans GameManager.
    public void OnBadMove()
    {
        comment.text = $"CETTE LETTRE N'EST PAS PRÉSENTE DANS LE NOM DE LA VILLE\nREJOUEZ ...";
    }


    // Affichage du message de saisie non-acceptée. Appelé par un event dans GameManager.
    public void OnWrongInput()
    {
        comment.text = $"ENTREZ UNIQUEMENT DES LETTRES !";
    }


    // Animation jouée à chaque entrée utilisateur acceptée
    public void GoAhead()
    {
        StartCoroutine(DashedLine());
    }


    // Coroutine de l'animation jouée à chaque entrée utilisateur acceptée
    public IEnumerator DashedLine()
    {
        for (int i = 0; i < 4; i++)
        {
            dashedLine.sprite = dashedLineSprites[0];
            yield return new WaitForSeconds(0.1f);
            dashedLine.sprite = dashedLineSprites[1];
            yield return new WaitForSeconds(0.1f);
            dashedLine.sprite = dashedLineSprites[2];
            yield return new WaitForSeconds(0.1f);
        }
    }


    // Fonction appelée par l'événement OnClick du bouton RadioPlay. Certains game objects sont activés, d'autres éteints. Simule une autoradio allumée. Remet le focus sur InputField après la manipulation de l'autoradio.
    public void RadioOn()
    {
        radioLCD.gameObject.SetActive(true);
        radioPause.gameObject.SetActive(true);
        radioPlay.gameObject.SetActive(false);
        radioSlider.gameObject.SetActive(true);
        radioSliderPlaceholder.gameObject.SetActive(false);
        FocusInputField();
    }


    // Fonction appelée par l'événement OnClick du bouton RadioPause. Certains game objects sont activés, d'autres éteints. Simule une autoradio éteinte. Remet le focus sur InputField après la manipulation de l'autoradio.
    public void RadioOff()
    {
        radioLCD.gameObject.SetActive(false);
        radioPlay.gameObject.SetActive(true);
        radioPause.gameObject.SetActive(false);
        radioSlider.gameObject.SetActive(false);
        radioSliderPlaceholder.gameObject.SetActive(true);
        FocusInputField();
    }
}
