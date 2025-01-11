using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioController audioController;

    public UnityEvent onBadMove, onGoodMove, onAlreadyPlayed;
    public UnityEvent readyToPlay;

    public Game currentGame;

    public static GameManager instance;

    public CityDataBase cityDataBase;

    public static bool youWin = false;
    public static string city;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void LaunchGame()
    {
        Debug.Log($"Je suis appelé");

        CityInfos cityInfos = cityDataBase.GetRandomCity();

        currentGame = new Game(cityInfos);

        currentGame.playedLetters.Clear();

        readyToPlay.Invoke();
    }


    public void OnLetterPlayed(string userInput)
    {
        if (string.IsNullOrEmpty(userInput)) return;

        userInput = userInput.ToUpper();

        if (userInput == currentGame.cityToGo.ToUpper())
        {
            youWin = true;

            LoadGameOverScene();
        }

        if (currentGame.playedLetters.Contains(userInput))
        {
            onAlreadyPlayed.Invoke();
            return;
        }

        currentGame.playedLetters.Add(userInput);

        if (currentGame.AllLettersGuessed)
        {
            youWin = true;

            LoadGameOverScene();
        }

        if (IsGoodMove(userInput))
        {
            currentGame.life--;
            onGoodMove.Invoke();

        }
        else
        {
            currentGame.life--;

            if (currentGame.life <= 0)
            {
                LoadGameOverScene();
            }
            else
            {
                onBadMove.Invoke();
            }
        }

        readyToPlay.Invoke();
    }


    bool IsGoodMove(string letter)
    {
        if (currentGame.cityToGo.ToUpper().Contains(letter.ToUpper())) return true;

        return false;
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    // private IEnumerator CheckScene()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     // Debug.Log("Scene active : " + SceneManager.GetActiveScene().name);
    // }


}
