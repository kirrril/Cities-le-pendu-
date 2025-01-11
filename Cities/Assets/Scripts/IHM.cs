using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class IHM : MonoBehaviour
{
    public Image road;
    public TMP_InputField inputField;
    public Button demarrer;
    public TMP_Text alreadyPlayed;
    public TMP_Text wordToGuess;
    public TMP_Text comment;
    public Image fuelTank;


    public GameObject panel;
    public Image arrivalCityImg;
    public Button redemarrer;
    public Button quitter;
    public TMP_Text mainMenuText;

    public static IHM instance;

    public GameManager gameManager;
    public Game currentGame;

    public List<Sprite> fuelTankLevel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Init();
    }


    public void OnStart()
    {
        GameSet();
    }


    public void Init()
    {
        road.gameObject.SetActive(true);
        inputField.gameObject.SetActive(false);
        demarrer.gameObject.SetActive(true);
        alreadyPlayed.gameObject.SetActive(true);
        alreadyPlayed.text = $"SUITE A UNE SOIRÉE TRÈS ARROSÉE, VOUS NE VOUS SOUVENEZ PLUS DU LIEU DE VOTRE RÉSIDENCE ...";
        wordToGuess.gameObject.SetActive(true);
        wordToGuess.text = $"LE RÉSERVOIR DE VOTRE VÉHICULE EST PLEIN";
        comment.gameObject.SetActive(true);
        comment.text = $"MAIS IL FAUT SE RAPPELER DU NOM DE LA VILLE ...";
        fuelTank.gameObject.SetActive(false);

        arrivalCityImg.gameObject.SetActive(false);
        redemarrer.gameObject.SetActive(false);
        quitter.gameObject.SetActive(false);
        mainMenuText.gameObject.SetActive(false);
    }


    public void GameSet()
    {
        road.gameObject.SetActive(true);
        inputField.gameObject.SetActive(true);
        FocusInputField();
        demarrer.gameObject.SetActive(false);
        alreadyPlayed.gameObject.SetActive(true);
        wordToGuess.gameObject.SetActive(true);
        comment.gameObject.SetActive(true);
        comment.text = $"ENTREZ UNE LETTRE OU LE NOM DE LA VILLE EN ENTIER";
        fuelTank.gameObject.SetActive(true);

        arrivalCityImg.gameObject.SetActive(false);
        redemarrer.gameObject.SetActive(false);
        quitter.gameObject.SetActive(false);
        mainMenuText.gameObject.SetActive(false);
    }


    public void SetMainMenu()
    {
        road.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
        demarrer.gameObject.SetActive(false);
        alreadyPlayed.gameObject.SetActive(false);
        wordToGuess.gameObject.SetActive(false);
        comment.gameObject.SetActive(false);
        fuelTank.gameObject.SetActive(false);

        panel.SetActive(true);
        arrivalCityImg.gameObject.SetActive(true);
        redemarrer.gameObject.SetActive(true);
        quitter.gameObject.SetActive(true);
        mainMenuText.gameObject.SetActive(true);
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

        if (GameManager.youWin)
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
        GameManager.youWin = false;
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



    public void GameOver()
    {
        SceneManager.LoadScene("MainMenu");

        CheckLoadedScene();

        StartCoroutine(WaitToBeLoaded());
    }

    private IEnumerator WaitToBeLoaded()
    {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Debug de vérification dans la coroutine");

        yield return new WaitForSeconds(0.5f);

        Debug.Log(SceneManager.GetActiveScene().name);

        yield return new WaitForSeconds(0.5f);

        CheckLoadedScene();

        SetMainMenu();
    }


    private void CheckLoadedScene()
    {
        Debug.Log("Debug dans la méthode CheckLoadedScene");

        Debug.Log(SceneManager.GetActiveScene().name);
    }

    // public void SetArrivalImg()
    // {
    //     if (youWin == false)
    //     {
    //         // arrivalCityImg.sprite = nightHighwaySprite;
    //     }
    //     else
    //     {
    //         switch (GameManager.instance.currentGame.cityToGo)
    //         {
    //             // case "Paris":
    //             //     arrivalCityImg.sprite = parisSprite;
    //             //     break;

    //             // case "Marseille":
    //             //     arrivalCityImg.sprite = marseilleSprite;
    //             //     break;

    //             // case "Lyon":
    //             //     arrivalCityImg.sprite = lyonSprite;
    //             //     break;

    //             // case "Nice":
    //             //     arrivalCityImg.sprite = niceSprite;
    //             //     break;

    //             // case "Toulouse":
    //             //     arrivalCityImg.sprite = toulouseSprite;
    //             //     break;

    //             // case "Montpellier":
    //             //     arrivalCityImg.sprite = montpellierSprite;
    //             //     break;

    //             // case "Caen":
    //             //     arrivalCityImg.sprite = caenSprite;
    //             //     break;

    //             // case "Carcassonne":
    //             //     arrivalCityImg.sprite = carcassonneSprite;
    //             //     break;

    //             // case "Limoges":
    //             //     arrivalCityImg.sprite = limogesSprite;
    //             //     break;

    //             // case "Bordeaux":
    //             //     arrivalCityImg.sprite = bordeauxSprite;
    //             //     break;
    //         }
    //     }
    // }

}
