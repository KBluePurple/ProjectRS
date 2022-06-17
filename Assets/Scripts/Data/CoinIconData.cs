using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using UnityEngine;

public class CoinIcon
{
    [JsonProperty("symbol")]
    public string Symbol;
    [JsonProperty("img_url")]
    public string IconURL;

    public CoinIcon(string symbol, string iconURL)
    {
        Symbol = symbol;
        IconURL = iconURL;
    }
}

public class CoinIconData
{
    public static int ImageResolution = 128;
    public static Dictionary<string, CoinIcon> List = new Dictionary<string, CoinIcon>();
    public static void Load(string json)
    {
        var list = JsonConvert.DeserializeObject<CoinIcon[]>(json);
        foreach (var item in list)
        {
            item.IconURL = item.IconURL.Replace("32", ImageResolution.ToString());
            if (!List.ContainsKey(item.Symbol))
            {
                List.Add(item.Symbol, item);
            }
        }
    }
}
