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

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.coinmarketcap.com/data-api/v3/cryptocurrency/listing?start=1&limit=100&sortBy=market_cap&sortType=desc&convert=KRW");
        
        request.Accept = "application/json";
        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        Debug.Log($"{nameof(DataManager)}: {json}");
        CoinData.Load(json);
    }
}
