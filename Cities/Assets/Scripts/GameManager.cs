using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Le script pour gérer la logique du jeu. Autrement dit, pour gérer la GameScene et transmettre les informations sur la partie finie à la scène GameOver.
public class GameManager : MonoBehaviour
{
    // Références aux scripts d'interface et de son de la scène
    [SerializeField]
    private IHM ihm;
    [SerializeField]
    private GameSceneSound gameSceneSound;

    // Déclaration des events permettant appeler des fonctions du script IHM (alternativement, les fonctions auraient pu être appelées directement)
    public UnityEvent onBadMove, onGoodMove, onAlreadyPlayed, onWrongInput, readyToPlay;

    // Déclaration d'une variable pour l'instance de Game créée par la fonction LaunchGame() appelée dans la méthode Start() au chargement de la GameScene
    public Game currentGame;

    // Déclaration d'une variable statique du singleton GameManager
    public static GameManager instance;

    // Référence au Scriptable Object contenant une base de données de villes (noms de villes, adresses de leurs photos, fonction pour choisir une ville au hasard)
    public CityDataBase cityDataBase;


    void Awake()
    {
        // Initialisation du singleton (instance unique du script) qui sera accessible par tous les scripts durant la boucle du jeu grâce à la variable statique le contenant. Au retour dans la scène GameScene, et donc au nouveau chargement de GameManager, la nouvelle instance de variable écrasera l'ancienne. Pas besoin de vérifier l'existance d'une ancienne version du singleton pour le détruire car le singleton n'a pas été rendu persistant avec DontDestroyOnLoad(gameObject).
        instance = this;
    }

    void Start()
    {
        // Initialisation du jeu
        LaunchGame();
    }


    public void LaunchGame()
    {
        // Appel de la foction contenu dans le scriptable object qui choisit une ville au hasard
        CityInfos cityInfos = cityDataBase.GetRandomCity();

        // Création d'un objet Game à partir de la ville choisie au hasard
        currentGame = new Game(cityInfos);

        // Remise à zéro de la liste de lettres déjà jouées pour le cas où le jeu est rejoué depuis la scène GameOver
        currentGame.playedLetters.Clear();
    }


    // Fonction principale du jeu qui analyse la saisie du joueur dans le InputField et définie si le jeu est gagné ou perdu. La fonction est appelée par la fonction GetInput() du script IHM qui, à son tour, est appelée par l'événement OnEndEdit du game object InputField.
    public void OnLetterPlayed(string userInput)
    {
        // Absence de réaction à l'appui de la touche entrée sans modifier le contenu de l'InputField. InputField reste focus.
        if (string.IsNullOrEmpty(userInput)) return;

        // Vérification si la saisie contient uniquement des lettres
        foreach (char letter in userInput)
        {
            if (!char.IsLetter(letter))
            {
                // Message si saisie autre que des lettres
                onWrongInput.Invoke();
                // Remettre le focus sur InputField
                readyToPlay.Invoke();
                return;
            }
        }

        // Convertir la saisie en majuscules pour comparer avec le nom de la ville en majuscules. Tout est en majuscules comme sur les panneaux d'autoroute.
        userInput = userInput.ToUpper();

        // Si le joueur a rentré le nom de la ville en entier, il gagne
        if (userInput == currentGame.cityToGo.ToUpper())
        {
            // Animation à chaque entrée valide
            MoveForward();
            // Mise à jour de l'affichage du nom de la ville, de la liste des lettres déjà jouées, du carburant restant et focus sur InputField
            readyToPlay.Invoke();
            // Variable booléenne pour marquer une partie gagnée. Contenu dans l'objet Game et est accessible de l'extérieur du script grâce au singleton GameManager.
            currentGame.youWin = true;
        }

        // Si la même lettre est jouée une deuxième fois
        if (currentGame.playedLetters.Contains(userInput))
        {
            // Afficher le message de lettre déjà jouée
            onAlreadyPlayed.Invoke();
            return;
        }

        // Ajouter la lettre jouée à la liste de lettres déjà jouées
        currentGame.playedLetters.Add(userInput);

        // Vérifier si toutes les lettres ont été devinées grâce à une fonction de la classe Game à laquelle on accède via la propriété AllLettersGuessed
        if (currentGame.AllLettersGuessed)
        {
            MoveForward();
            readyToPlay.Invoke();
            // Si toutes les lettres sont devinées, on gagne
            currentGame.youWin = true;
        }

        // Si la lettre est présente dans le nom de la ville
        if (IsGoodMove(userInput))
        {
            MoveForward();

            // Enlever un point de vie
            currentGame.life--;
            // Afficher le message de lettre devinée
            onGoodMove.Invoke();

        }
        else
        {
            MoveForward();

            // Enlever un point de vie
            currentGame.life--;
            // Afficher le message de lettre qui n'est pas présente dans le nom de la ville
            onBadMove.Invoke();
        }

        // A chaque lettre jouée, mettre à jour de l'affichage du nom de la ville, de la liste des lettres déjà jouées, du carburant restant et mettre le focus sur InputField
        readyToPlay.Invoke();

        // Si le jeu est gagné ou s'il ne reste plus de tentatives, charger la scène GameOver
        if (currentGame.youWin || currentGame.life <= 0)
        {
            StartCoroutine(LoadGameOverScene());
        }
    }


    // Fonction avec le type de retour bool pour vérifier si la lettre est présente dans le nom de la ville. Quand elle est appellée, le paramètre prend la valeur de userInput.
    bool IsGoodMove(string letter)
    {
        if (currentGame.cityToGo.ToUpper().Contains(letter.ToUpper())) return true;

        return false;
    }


    // Animation et son qui sont joués à chaque saisie joueur valide unique
    void MoveForward()
    {
        StartCoroutine(ihm.DashedLine());
        gameSceneSound.GasUp();
    }


    // Une coroutine qui attend que l'animation d'entrée valide soit jouée avant de charger la scène GameOver
    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("GameOver");
    }


    // Quitter l'application à l'appui du bouton StopButton
    public void Quit()
    {
        // Pour la Play mode de l'éditeur Unity
#if UNITY_EDITOR

        EditorApplication.isPlaying = false;

        // Pour l'application autonome
#elif UNITY_STANDALONE
        
        Application.Quit();

#endif
    }
}
