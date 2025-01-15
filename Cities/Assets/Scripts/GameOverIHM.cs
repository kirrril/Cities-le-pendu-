using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverIHM : MonoBehaviour
{
    
    public string uriArrivalImage;

    public Image arrivalCityImg;
    public TMP_Text gameOverText;

    [SerializeField]
    private Texture2D texture;
    
    void Start()
    {
        if (GameManager.instance.currentGame.youWin)
        {
            StartCoroutine(GetArrivalImage(SetArrivalImg()));

            Debug.Log($"You Win: {GameManager.instance.currentGame.youWin}");

            gameOverText.text = $"BRAVO ! VOUS ETES ARRIVÉ A {GameManager.instance.currentGame.cityInfos.name.ToUpper()} !\n\nCHOISIR UNE NOUVELLE DESTINATION ?";

        } else
        {
            Debug.Log($"City: {GameManager.instance.currentGame.cityInfos.name}");
            StartCoroutine(GetNowhereImage());
            gameOverText.text = $"PLUS DE CARBURANT,\nVOUS AVEZ PERDU ...\n\nCHOISIR UNE NOUVELLE DESTINATION ?";
        }
        
    }


    IEnumerator GetNowhereImage()
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("https://images.caradisiac.com/images/5/8/7/9/205879/S0-il-s-envole-sur-l-autoroute-detruit-son-audi-a1-et-s-en-sort-indemne-781830.jpg"))
        {
            yield return webRequest.SendWebRequest();

            texture = DownloadHandlerTexture.GetContent(webRequest);

            arrivalCityImg.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }


    IEnumerator GetArrivalImage(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri))
        {
            yield return webRequest.SendWebRequest();

            texture = DownloadHandlerTexture.GetContent(webRequest);

            arrivalCityImg.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    public string SetArrivalImg()
    {
      return GameManager.instance.currentGame.cityInfos.imageUri;
    }

}
