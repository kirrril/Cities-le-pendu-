using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onBadMove, onGoodMove, onAlreadyPlayed;
    public UnityEvent onGameOver;
    public UnityEvent readyToPlay;
    public UnityEvent onCityGuessed;

    public Game currentGame;

    public static GameManager instance;


    void Awake()
    {
        instance = this;
    }


    public void LaunchGame()
    {
        currentGame = new Game();

        currentGame.cityToGo = currentGame.GetRandomCity();

        currentGame.playedLetters.Clear();

        readyToPlay.Invoke();
    }


    public void OnLetterPlayed(string userInput)
    {
        if (string.IsNullOrEmpty(userInput)) return;

        userInput = userInput.ToUpper();

        if (userInput == currentGame.cityToGo.ToUpper())
        {
            onCityGuessed.Invoke();
            return;
        }

        if (currentGame.playedLetters.Contains(userInput))
        {
            onAlreadyPlayed.Invoke();
            return;
        }

        currentGame.playedLetters.Add(userInput);

        if (currentGame.AllLettersGuessed)
        {
            onCityGuessed.Invoke();
            return;
        }

        if (IsGoodMove(userInput))
        {
            currentGame.life--;
            onGoodMove.Invoke();
            
        } else
        {
            currentGame.life--;

            if (currentGame.life <= 0)
            {
                onGameOver.Invoke();
            } else
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


    public void GameOver()
    {
        onGameOver.Invoke();
    }




}
