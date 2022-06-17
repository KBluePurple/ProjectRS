using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        Debug.Log($"{nameof(DataManager)}: Initialize");

        FileInfo fileInfo = new FileInfo(Application.dataPath + "/Resources/CoinIconData.json");
        if (fileInfo.Exists)
        {
            string jsonFile = File.ReadAllText(Application.dataPath + "/Resources/CoinIconData.json");
            CoinIconData.Load(jsonFile);
        }

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.coinstats.app/public/v1/coins?skip=0&limit=100&currency=USDT");
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        CoinData.Load(json);
    }
}
