using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class IHM_double : MonoBehaviour
{
  public Image roadImage;
  public TMP_InputField inputField;
  public Button startButton;
  public TMP_Text alreadyPlayed;
  public TMP_Text wordToGuess;
  public TMP_Text comment;
  public Image fuelTank;

  public GameObject panel;
  public Image arrivalCityImg;
  public Button redemarrer;
  public Button quitter;
  public TMP_Text mainMenuText;


  public static IHM_double instance;

  public GameManager gameManager;
  public Game currentGame;

  public List<Sprite> fuelTankLevel;


  void SetLoadedScene()
  {
    if (SceneManager.GetActiveScene().name == "GameScene")
    {
      roadImage.gameObject.SetActive(true);
      inputField.gameObject.SetActive(true);
      startButton.gameObject.SetActive(true);
      alreadyPlayed.gameObject.SetActive(true);
      wordToGuess.gameObject.SetActive(true);
      comment.gameObject.SetActive(true);
      fuelTank.gameObject.SetActive(true);
    }
    else if (SceneManager.GetActiveScene().name == "MainMenu")
    {
      panel.SetActive(true);
      arrivalCityImg.gameObject.SetActive(true);
      redemarrer.gameObject.SetActive(true);
      quitter.gameObject.SetActive(true);
      mainMenuText.gameObject.SetActive(true);

      if (GameManager.youWin)
      {
        mainMenuText.text = $"Bravo ! Vous avez deviné {GameManager.city}";
      }
      else
      {
        mainMenuText.text = $"Il ne vous reste plus de carburant, vous avez perdu ...";
      }
    }
  }



  private void DesactivateAllObjects()
  {
    roadImage.gameObject.SetActive(false);
    inputField.gameObject.SetActive(false);
    startButton.gameObject.SetActive(false);
    alreadyPlayed.gameObject.SetActive(false);
    wordToGuess.gameObject.SetActive(false);
    comment.gameObject.SetActive(false);
    fuelTank.gameObject.SetActive(false);

    panel.SetActive(false);
    arrivalCityImg.gameObject.SetActive(false);
    redemarrer.gameObject.SetActive(false);
    quitter.gameObject.SetActive(false);
    mainMenuText.gameObject.SetActive(false);
  }


  private void Init()
  {
    roadImage.gameObject.SetActive(true);
    inputField.gameObject.SetActive(false);
    startButton.gameObject.SetActive(true);
    alreadyPlayed.gameObject.SetActive(true);
    alreadyPlayed.text = $"SUITE A UNE SOIRÉE TRÈS ARROSÉE, VOUS NE VOUS SOUVENEZ PLUS DU LIEU DE VOTRE RÉSIDENCE ...";
    wordToGuess.gameObject.SetActive(true);
    wordToGuess.text = $"LE RÉSERVOIR DE VOTRE VÉHICULE EST PLEIN";
    comment.gameObject.SetActive(true);
    comment.text = $"MAIS IL FAUT SE RAPPELER DU NOM DE LA VILLE ...";
    fuelTank.gameObject.SetActive(false);
  }


  public void GameSet()
  {
    roadImage.gameObject.SetActive(true);
    inputField.gameObject.SetActive(true);
    FocusInputField();
    startButton.gameObject.SetActive(false);
    alreadyPlayed.gameObject.SetActive(true);
    wordToGuess.gameObject.SetActive(true);
    comment.gameObject.SetActive(true);
    comment.text = $"ENTREZ UNE LETTRE OU LE NOM DE LA VILLE EN ENTIER";
    fuelTank.gameObject.SetActive(true);
  }


  void Awake()
  {
    instance = this;
  }



  void Start()
  {

    Init();

  }


  public void OnStart()
  {
    GameSet();
    UpdateIhm();
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


  public void OnGoodMove()
  {
    comment.text = $"BRAVO ! VOUS AVEZ DEVINE UNE LETTRE !\nREJOUEZ ...";
  }


  public void OnBadMove()
  {
    comment.text = $"CETTE LETTRE N'EST PAS PRESENTE DANS LE NOM DE LA VILLE\nREJOUEZ ...";
  }







}
