using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

[System.Serializable]
public class Game
{
    public List<string> playedLetters = new List<string> { };
    
    public string cityToGo
    {
        get
        {
            return cityInfos.name;
        }
    }

    public CityInfos cityInfos;

    public int life = 7;

    public Game(CityInfos cityInfos)
    {
        this.cityInfos = cityInfos;
    }

    public bool AllLettersGuessed
    {
        get
        {
            foreach (char letter in cityToGo.ToUpper())
            {
                if (!playedLetters.Contains(letter.ToString()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}