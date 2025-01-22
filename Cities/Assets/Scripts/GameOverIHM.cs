using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;


// Script pour gérer l'interface graphique de la scène GameOver
public class GameOverIHM : MonoBehaviour
{
    // Références aux éléments de l'interface utilisateur pour afficher l'image et le texte de fin de jeu
    public Image arrivalCityImg;
    public TMP_Text gameOverText;

    // Texture temporaire utilisée pour stocker l'image téléchargée avant de la convertir en sprite
    [SerializeField]
    private Texture2D texture;

    // En fonction de l'état de la partie fini, gagnée ou pas, affiche, au moment du chargement de la scène, texgte et image
    void Start()
    {
        // Si la partie est gagnée
        if (GameManager.instance.currentGame.youWin)
        {
            // Lancer la coroutine de téléchargement de l'image de la ville d'arrivée
            StartCoroutine(GetArrivalImage(SetArrivalImg()));

            // Afficher le message de victoire
            gameOverText.text = $"BRAVO !\nVOUS ÊTES ARRIVÉ\nÀ {GameManager.instance.currentGame.cityInfos.name.ToUpper()} !\n/ / / / / / / / / / / / / / / / / / /\nCHOISIR UNE NOUVELLE DESTINATION ?";

        }
        // Si la partie n'est pas gagnée
        else
        {
            // Lancer la coroutine de téléchargement de l'image de panne sèche
            StartCoroutine(GetNowhereImage());

            // Afficher le message de défaite
            gameOverText.text = $"PLUS DE CARBURANT,\nVOUS AVEZ PERDU ...\n/ / / / / / / / / / / / / / / / / / /\nCHOISIR UNE NOUVELLE DESTINATION ?";
        }

    }

    // Coroutine de téléchargement de l'image de la panne sèche
    IEnumerator GetNowhereImage()
    {
        // Télécharger une image depuis l'URL spécifiée
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("https://images.caradisiac.com/images/5/8/7/9/205879/S0-il-s-envole-sur-l-autoroute-detruit-son-audi-a1-et-s-en-sort-indemne-781830.jpg"))
        {
            // Attendre la fin de la requête web
            yield return webRequest.SendWebRequest();

            // Récupérer l'image sous forme de texture
            texture = DownloadHandlerTexture.GetContent(webRequest);
            // Convertir la texture en sprite
            arrivalCityImg.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    // Coroutine de téléchargement de l'image de la ville d'arrivée
    IEnumerator GetArrivalImage(string uri)
    {
        // Télécharger une image depuis l'URL spécifiée dans CityDateBase
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri))
        {
            // Attendre la fin de la requête web
            yield return webRequest.SendWebRequest();
            // Récupérer l'image sous forme de texture
            texture = DownloadHandlerTexture.GetContent(webRequest);
            // Convertir la texture en sprite
            arrivalCityImg.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    // Fonction qui retourne URI de l'image de la ville devinée
    public string SetArrivalImg()
    {
        return GameManager.instance.currentGame.cityInfos.imageUri;
    }

}
