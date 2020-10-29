using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetCountryInfo : MonoBehaviour
{

    private const string URL = "https://corona.lmao.ninja/v2/countries?sort=cases";
    private const string popURL = "https://disease.sh/v3/covid-19/countries/austria?yesterday=false&twoDaysAgo=false&strict=false&allowNull=true";
    private Text info;
    private Text detailedInfo;
    void Start()
    {
        info = GameObject.Find("CountryInfo").transform.GetComponent<Text>();
        detailedInfo = GameObject.Find("DetailedCountryInfo").transform.GetComponent<Text>();
        detailedInfo.text = "";
        info.text = "";
        StartCoroutine(GetRequest(URL));
        StartCoroutine(GetPopulationRequest(popURL));
    }
    private IEnumerator GetPopulationRequest(String url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            var data = JsonUtility.FromJson<DetailedCountryInf>(webRequest.downloadHandler.text);
            detailedInfo.text += "Population: " + data.population;
        }
    }
    private IEnumerator GetRequest(String url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            var data = JsonUtility.FromJson<RootObject>("{ \"countries\": " + webRequest.downloadHandler.text + "}");           
            for(int i = 0; i < data.countries.Count; i++)
            {
                if (data.countries[i].country.Equals("Austria"))
                    info.text += "Country: " + data.countries[i].country + "\n" +
                             "Cases: " + data.countries[i].cases + "\n" +
                             "todayCases: " + data.countries[i].todayCases + "\n" +
                             "Deaths: " + data.countries[i].deaths + "\n" +
                             "TodayDeaths: " + data.countries[i].todayDeaths + "\n" +
                             "Recovered: " + data.countries[i].recovered + "\n" +
                             "Active: " + data.countries[i].active + "\n" +
                             "Critical: " + data.countries[i].critical + "\n";
            }

        }



    }
}
[System.Serializable]
public class DetailedCountryInf
{
    public object updated;
    public string country;
    public CountryInfo countryInformation;
    public int cases;
    public int todayCases;
    public int deaths;
    public int todayDeaths;
    public int recovered;
    public int active;
    public int critical;
    public double? casesPerOneMillion;
    public double? deathsPerOneMillion;
    public int tests;
    public int testPerMillion;
    public int population;
    public int continent;
    public int oneCasePerPeople;
    public int oneDeathPerPeople;
    public int activePerOneMillion;
    public int recoveredPerOneMillion;
    public int criticalPerOneMillion;
}
[System.Serializable]
    public class CountryInfo
    {
        public int? _id;
        public string iso2;
        public string iso3;
        public double lat;
        public double @long;
        public string flag;
    }

    [System.Serializable]
    public class UserObject
    {
        public string country;
        public CountryInfo countryInformation;
        public int cases;
        public int todayCases;
        public int deaths;
        public int todayDeaths;
        public int recovered;
        public int active;
        public int critical;
        public double? casesPerOneMillion;
        public double? deathsPerOneMillion;
        public object updated;
    }

    [System.Serializable]
    public class RootObject
    {
        public List<UserObject> countries;
    }
