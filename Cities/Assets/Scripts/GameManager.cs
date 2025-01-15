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
    private GameSceneSound gameSceneSound;

    public UnityEvent onBadMove, onGoodMove, onAlreadyPlayed;
    public UnityEvent readyToPlay;

    public Game currentGame;
    

    public static GameManager instance;

    public CityDataBase cityDataBase;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LaunchGame();
        gameSceneSound.EngineSound();
    }


    public void LaunchGame()
    {
        CityInfos cityInfos = cityDataBase.GetRandomCity();

        currentGame = new Game(cityInfos);

        currentGame.playedLetters.Clear();
    }


    public void OnLetterPlayed(string userInput)
    {
        gameSceneSound.AccelerateSound();

        if (string.IsNullOrEmpty(userInput)) return;

        userInput = userInput.ToUpper();

        if (userInput == currentGame.cityToGo.ToUpper())
        {
            currentGame.youWin = true;
        }

        if (currentGame.playedLetters.Contains(userInput))
        {
            onAlreadyPlayed.Invoke();
            return;
        }

        currentGame.playedLetters.Add(userInput);

        if (currentGame.AllLettersGuessed)
        {
            currentGame.youWin = true;
        }

        if (IsGoodMove(userInput))
        {
            currentGame.life--;
            onGoodMove.Invoke();

        }
        else
        {
            currentGame.life--;
            onBadMove.Invoke();
        }

        if (currentGame.youWin || currentGame.life <= 0)
        {
            StartCoroutine(LoadGameOverScene());
        }

        readyToPlay.Invoke();
    }


    bool IsGoodMove(string letter)
    {
        if (currentGame.cityToGo.ToUpper().Contains(letter.ToUpper())) return true;

        return false;
    }

    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
    }
}
