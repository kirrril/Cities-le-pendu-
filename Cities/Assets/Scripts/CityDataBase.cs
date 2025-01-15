using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CityDataBase", menuName = "ScriptableObject/City Data Base")]
public class CityDataBase : ScriptableObject
{
    [SerializeField]
    List<CityInfos> citiesInfos;


    public CityInfos GetRandomCity()
    {

        int cityIndex = UnityEngine.Random.Range(0, citiesInfos.Count);

        return citiesInfos[cityIndex];
    }
}

[System.Serializable]
public class CityInfos
{
    public string name;

    public string imageUri;
}