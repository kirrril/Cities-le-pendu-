using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

[System.Serializable]
public class Game
{
    public List<string> playedLetters = new List<string> { };
    public string cityToGo = "";

    public int life = 7;

    public string GetRandomCity()
    {
        List<string> cities = new List<string> { "Paris", "Marseille", "Lyon", "Nice", "Toulouse", "Montpellier", "Caen", "Carcassonne", "Limoges", "Bordeaux" };

        // ScriptableObject

        if (cityToGo == "")
        {
            int cityIndex = UnityEngine.Random.Range(0, cities.Count);

            cityToGo = cities[cityIndex];
        }

        return cityToGo;
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