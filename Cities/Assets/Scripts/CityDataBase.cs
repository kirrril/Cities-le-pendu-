using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Création d'un ScriptableObject et ajout d'une option dans le menu contextuel de l'éditeur Unity pour le créer
[CreateAssetMenu(fileName = "CityDataBase", menuName = "ScriptableObject/City Data Base")]
public class CityDataBase : ScriptableObject
{
    // Liste des informations sur les villes (noms et URI d'images), sérialisée pour être visible et modifiable dans l'inspecteur
    [SerializeField]
    List<CityInfos> citiesInfos;

    // Méthode pour choisir une ville au hasard
    public CityInfos GetRandomCity()
    {
        // Génération d'un index aléatoire dans les limites de la liste
        int cityIndex = UnityEngine.Random.Range(0, citiesInfos.Count);

        // Retourne la ville correspondant à l'index généré
        return citiesInfos[cityIndex];
    }
}

// Classe définissant les informations d'une ville pour la liste
[System.Serializable]
public class CityInfos
{
    public string name;

    public string imageUri;
}