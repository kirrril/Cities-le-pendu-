using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetImage : MonoBehaviour
{
    public Image arrivalCityImg;
    public IHM_double ihmDouble;


    [SerializeField]
    private Texture2D texture;
    
    void Start()
    {
        
    }

    IEnumerator GetArrivalImage(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri))
        {
            Debug.Log($"Receivied URI: {uri}");

            yield return webRequest.SendWebRequest();

            Debug.Log($"Received: {webRequest.downloadHandler}");

            texture = DownloadHandlerTexture.GetContent(webRequest);

            arrivalCityImg.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }


}
