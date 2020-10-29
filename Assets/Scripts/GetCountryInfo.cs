using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetCountryInfo : MonoBehaviour
{

    private const string URL = "https://corona.lmao.ninja/v2/countries?sort=cases";
    private Text info;
    void Start()
    {
        info = GameObject.Find("CountryInfo").transform.GetComponent<Text>();
        info.text = "";
        StartCoroutine(GetRequest(URL));
    }
    private IEnumerator GetRequest(String url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            var data = JsonUtility.FromJson<RootObject>("{ \"countries\": " + webRequest.downloadHandler.text + "}");
            for(int i = 0; i < data.countries.Count; i++)
            {
                info.text += data.countries[i].country+" ";
            }

        }



    }
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
