using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

[System.Serializable] /*permet d'afficher le contenu de la classe dans l'inspecteur*/
// Classe Game qui permet de créer une instance de la partie en cours
public class Game
{
    // Liste de lettres déjà jouées
    public List<string> playedLetters = new List<string> { };
    
    // Propriété permettant d'accéder au nom de la ville choisie au hasard
    public string cityToGo
    {
        get
        {
            return cityInfos.name;
        }
    }

    // Variable publique de type CityInfos qui permet de récupérer le membre de la liste CityInfos qui est contenue dans le SriptableObject CityDataBase qui a été choisie au hasard
    public CityInfos cityInfos;

    // Nombre de tentatives au lancement du jeu
    public int life = 7;

    // Variable bool qui marque la partie comme gagnée, (false par défaut)
    public bool youWin;

    // Constructeur de la classe Game, initialisé avec la ville choisie au hasard depuis le ScriptableObject CityDataBase, via la fonction LaunchGame() de GameManager
    public Game(CityInfos cityInfos)
    {
        this.cityInfos = cityInfos;
    }

    // Propriété booléenne vérifiant si toutes les lettres de la ville ont été devinées
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