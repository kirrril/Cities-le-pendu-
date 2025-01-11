using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameOverIHM : MonoBehaviour
{
    
    public string uriArrivalImage;

    public Image arrivalCityImg;

    [SerializeField]
    private Texture2D texture;
    
    void Start()
    {
        StartCoroutine(GetArrivalImage(SetArrivalImg()));
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

    public string SetArrivalImg()
    {
        

    //   if (GameManager.instance == null || GameManager.youWin == false)
    //   {
    //     return uriArrivalImage = "https://img4.autodeclics.com/6/2022/03/photo_article/105835/43878/1200-L--233-km-h-la-nuit-sur-l-autoroute-on-retient-notre-souffle.jpg";
    //   }

      return GameManager.instance.currentGame.cityInfos.imageUri;

      
    //   else
    //   {
    //       switch (GameManager.city)
    //       {
    //           case "Paris":
    //               uriArrivalImage = "https://upload.wikimedia.org/wikipedia/commons/4/4b/La_Tour_Eiffel_vue_de_la_Tour_Saint-Jacques%2C_Paris_ao%C3%BBt_2014_%282%29.jpg";
    //               break;

    //           case "Marseille":
    //               uriArrivalImage = "https://www.interregnextmed.eu/wp-content/uploads/2024/12/marseille-5271931_1920.jpg";
    //               break;

    //           case "Lyon":
    //               uriArrivalImage = "https://www.republique-grolee-carnot.com/wp-content/uploads/2022/03/visiter-presquile-lyon.jpg";
    //               break;

    //           case "Nice":
    //               uriArrivalImage = "https://wanderingcarol.com/wp-content/uploads/2022/05/things-to-do-in-nice-france.jpg";
    //               break;

    //           case "Toulouse":
    //               uriArrivalImage = "https://www.hellio.com/hubfs/Blog%20Corporate%20-%20Images/toulouse-garonne-ensoleille-dome.jpeg";
    //               break;

    //           case "Montpellier":
    //               uriArrivalImage = "https://upload.wikimedia.org/wikipedia/commons/8/8a/Place_de_la_Com%C3%A9die_%282377437375%29.jpg";
    //               break;

    //           case "Caen":
    //               uriArrivalImage = "https://campingblonville.fr/wp-content/uploads/2021/05/uar-normandie-tourisme-41.jpg";
    //               break;

    //           case "Carcassonne":
    //               uriArrivalImage = "https://www.tourisme-carcassonne.fr/assets/uploads/2022/09/cite-medieval-carcassonne-vincent-photographie-scaled.jpg";
    //               break;

    //           case "Limoges":
    //               uriArrivalImage = "https://upload.wikimedia.org/wikipedia/commons/d/d7/Cour_du_Temple_-_Limoges.JPG";
    //               break;

    //           case "Bordeaux":
    //               uriArrivalImage = "https://agence-bordeaux.fr/wp-content/uploads/2022/09/parlement-bordeaux-historique.jpg";
    //               break;
    //       }

    //     return uriArrivalImage;
    //   }
  }


    public void Replay()
    {
        GameManager.instance.LaunchGame();
        SceneManager.UnloadSceneAsync("GameOver");
    }


}
