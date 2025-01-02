using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class IHM : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button demarrer;
    public TMP_Text alreadyPlayed;
    public TMP_Text wordToGuess;
    public TMP_Text comment;
    public TMP_Text gameOverText;
    public Image fuelTank;
    public Image arrivalCityImg;
    public Button redemarrer;

    public Sprite parisSprite;
    public Sprite marseilleSprite;
    public Sprite lyonSprite;
    public Sprite niceSprite;
    public Sprite toulouseSprite;
    public Sprite montpellierSprite;
    public Sprite caenSprite;
    public Sprite carcassonneSprite;
    public Sprite limogesSprite;
    public Sprite bordeauxSprite;
    public Sprite nightHighwaySprite;

    public GameManager gameManager;
    public Game currentGame;

    public List<Sprite> fuelTankLevel;

    public bool youWin;


    [SerializeField]
    GameObject GameOver;

    void Start()
    {
        Init();
    }


    public void OnStart()
    {
        GameSet();
        SetGameOverActive(false);
    }


    public void Init()
    {
        inputField.gameObject.SetActive(false);
        demarrer.gameObject.SetActive(true);
        alreadyPlayed.enabled = true;
        alreadyPlayed.text = $"SUITE A UNE SOIRÉE TRÈS ARROSÉE, VOUS NE VOUS SOUVENEZ PLUS DU LIEU DE VOTRE RÉSIDENCE";
        wordToGuess.gameObject.SetActive(true);
        wordToGuess.alignment = TextAlignmentOptions.Center;
        wordToGuess.text = $"LE RÉSERVOIR DE VOTRE VÉHICULE EST PLEIN";
        wordToGuess.color = Color.white;
        comment.gameObject.SetActive(true);
        comment.text = $"MAIS IL FAUT SE RAPPELER DU NOM DE LA VILLE";
        fuelTank.gameObject.SetActive(false);
        SetGameOverActive(false);
    }


    public void GameSet()
    {
        inputField.gameObject.SetActive(true);
        FocusInputField();
        demarrer.gameObject.SetActive(false);
        alreadyPlayed.gameObject.SetActive(true);
        wordToGuess.gameObject.SetActive(true);
        wordToGuess.color = Color.white;
        comment.gameObject.SetActive(true);
        comment.text = $"ENTREZ UNE LETTRE OU LE NOM DE LA VILLE EN ENTIER";
        fuelTank.gameObject.SetActive(true);
    }


    public void FocusInputField()
    {
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
    }


    void DisplayCityName()
    {
        string result = string.Empty;

        if (youWin)
        {
            foreach (string letter in GameManager.instance.currentGame.playedLetters)
            {
                if (letter == GameManager.instance.currentGame.cityToGo.ToUpper())
                {
                    result = GameManager.instance.currentGame.cityToGo.ToUpper();
                }
            }
        }
        else
        {
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

        wordToGuess.text = result;
    }


    public void GetInput()
    {
        string userInput = inputField.text;
        GameManager.instance.OnLetterPlayed(userInput);
    }


    void DisplayPlayedLetters()
    {
        string result = string.Empty;

        foreach (string letter in GameManager.instance.currentGame.playedLetters)
        {
            result += letter + ", ";
        }

        alreadyPlayed.text = $"LETTRES DEJA JOUEES :\n{result}";
    }


    void DisplayFuelTank()
    {
        int index = GameManager.instance.currentGame.life;
        fuelTank.sprite = fuelTankLevel[index];
    }


    public void UpdateIhm()
    {
        youWin = false;
        DisplayCityName();
        DisplayPlayedLetters();
        DisplayFuelTank();
        FocusInputField();
    }


    public void OnAlreadyPlayed()
    {
        comment.text = $"CETTE LETTRE A DEJA ETE JOUEE ...\nESSAYEZ UNE AUTRE";
        FocusInputField();
    }


    public void OnGoodMove(string lettre)
    {
        comment.text = $"BRAVO ! VOUS AVEZ DEVINE UNE LETTRE !\nREJOUEZ ...";
    }


    public void OnBadMove(string lettre)
    {
        comment.text = $"CETTE LETTRE N'EST PAS PRESENTE DANS LE NOM DE LA VILLE\nREJOUEZ ...";
    }


    public void CityGuessed()
    {
        youWin = true;

        OnGameOver($"BIENVENU À\n{GameManager.instance.currentGame.cityToGo.ToUpper()} !");
    }


    public void YouLoose()
    {
        OnGameOver($"Plus de carburant ...\nVous avez perdu !");
    }


    void SetGameOverActive(bool value)
    {
        GameOver.SetActive(value);
    }


    public void OnGameOver(string message)
    {
        inputField.gameObject.SetActive(false);
        demarrer.gameObject.SetActive(false);
        redemarrer.gameObject.SetActive(false);
        alreadyPlayed.gameObject.SetActive(false);
        wordToGuess.gameObject.SetActive(false);
        comment.gameObject.SetActive(false);
        fuelTank.gameObject.SetActive(false);
        SetArrivalImg();

        SetGameOverActive(true);
        gameOverText.text = message;

        StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(2);

        gameOverText.text = $"Rejouer ?";
        demarrer.gameObject.SetActive(true);
    }


    public void OnRestart()
    {
        youWin = false;
        OnStart();
    }


    public void SetArrivalImg()
    {
        if (youWin == false)
        {
            arrivalCityImg.sprite = nightHighwaySprite;
        }
        else
        {
            switch (GameManager.instance.currentGame.cityToGo)
            {
                case "Paris":
                    arrivalCityImg.sprite = parisSprite;
                    break;

                case "Marseille":
                    arrivalCityImg.sprite = marseilleSprite;
                    break;

                case "Lyon":
                    arrivalCityImg.sprite = lyonSprite;
                    break;

                case "Nice":
                    arrivalCityImg.sprite = niceSprite;
                    break;

                case "Toulouse":
                    arrivalCityImg.sprite = toulouseSprite;
                    break;

                case "Montpellier":
                    arrivalCityImg.sprite = montpellierSprite;
                    break;

                case "Caen":
                    arrivalCityImg.sprite = caenSprite;
                    break;

                case "Carcassonne":
                    arrivalCityImg.sprite = carcassonneSprite;
                    break;

                case "Limoges":
                    arrivalCityImg.sprite = limogesSprite;
                    break;

                case "Bordeaux":
                    arrivalCityImg.sprite = bordeauxSprite;
                    break;
            }
        }
    }
}
