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
    public TMP_InputField inputField;
    public TMP_Text alreadyPlayed;
    public TMP_Text cityToGo;
    public TMP_Text comment;
    public Image fuelTank;
    public Image dashedLine;


    public static IHM instance;

    public GameManager gameManager;
    public Game currentGame;

    public List<Sprite> fuelTankLevel;
    public List<Sprite> dashedLineSprites;


    public void GameSet()
    {
        FocusInputField();
        comment.text = $"ENTREZ UNE LETTRE OU LE NOM DE LA VILLE EN ENTIER";
    }


    void Awake()
    {
        instance = this;
    }



    void Start()
    {
        OnStart();
    }


    public void OnStart()
    {
        GameSet();
        UpdateIHM();
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

        if (GameManager.instance.currentGame.youWin)
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

        cityToGo.text = result;
    }


    public void GetInput()
    {
        StartCoroutine(DashedLine());
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

        alreadyPlayed.text = $"LETTRES DÉJÀ JOUÉES :\n{result}";
    }



    void DisplayFuelTank()
    {
        int index = GameManager.instance.currentGame.life;
        fuelTank.sprite = fuelTankLevel[index];
    }


    public void UpdateIHM()
    {
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


    public void GoAhead()
    {
        StartCoroutine(DashedLine());
    }


    IEnumerator DashedLine()
    {
        dashedLine.sprite = dashedLineSprites[0];
        yield return new WaitForSeconds(0.3f);
        dashedLine.sprite = dashedLineSprites[1];
        yield return new WaitForSeconds(0.3f);
        dashedLine.sprite = dashedLineSprites[2];
        yield return new WaitForSeconds(0.3f);
        dashedLine.sprite = dashedLineSprites[0];
        yield return new WaitForSeconds(0.3f);
        dashedLine.sprite = dashedLineSprites[1];
        yield return new WaitForSeconds(0.3f);
    }


}
