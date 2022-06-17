using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Linq;

[Serializable]
public class Coin
{
    [JsonProperty("id")]
    public string Id;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("symbol")]
    public string Symbol;
    [JsonProperty("price")]
    public float Price;
    [JsonProperty("icon")]
    public string IconUrl;

    private Sprite icon;
    private Color[] colors;

    public Sprite Icon => icon;

    public Color[] Colors
    {
        get
        {
            if (colors != null)
            {
                return colors;
            }

            if (icon == null)
            {
                return new Color[] { Color.white, Color.white };
            }

            Texture2D texture = icon.texture;
            Color[] pixels = texture.GetPixels();
            Dictionary<Color, int> colorCount = new Dictionary<Color, int>();
            for (int i = 1; i < pixels.Length; i++)
            {
                Color color = pixels[i];
                if (color.a < 0.9f)
                {
                    pixels[i] = Color.clear;
                    continue;
                };
                color.a = 1;
                bool similar = false;
                foreach (Color c in colorCount.Keys)
                {
                    float diff;
                    if (color.r == color.g && color.g == color.b)
                    {
                        diff = Mathf.Abs(c.r - color.r);
                    }
                    else 
                    {
                        diff = Mathf.Abs(c.r - color.r) + Mathf.Abs(c.g - color.g) + Mathf.Abs(c.b - color.b);
                    }
                    if (diff < 0.5f)
                    {
                        similar = true;
                        colorCount[c]++;
                        break;
                    }
                }
                if (!similar)
                {
                    colorCount.Add(color, 1);
                }
            }
            texture.SetPixelData(pixels, 0);
            colors = colorCount.OrderByDescending(x => x.Value).Select(x => x.Key).ToArray();
            return Colors;
        }
    }

    public void GetIcon(Action<Coin, Sprite> callback)
    {
        if (icon != null)
        {
            callback(this, icon);
            return;
        }
        GlobalObject.Instance.StartCoroutine(LoadImage(this, callback));
    }

    private static IEnumerator LoadImage(Coin coin, Action<Coin, Sprite> callback)
    {
        Debug.Log($"{nameof(LoadImage)}: {CoinIconData.List[coin.Symbol]?.IconURL}");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(CoinIconData.List[coin.Symbol].IconURL);
        yield return www.SendWebRequest();
        while (!www.isDone)
        {
            yield return null;
        }
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            try
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                coin.icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                callback(coin, coin.icon);
            }
            catch
            {
                callback(coin, null);
            }
        }
    }
}

public static class CoinData
{
    public static List<Coin> List = new List<Coin>();

    public static void Load(string json)
    {
        var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, List<Coin>>>(json);
        List = jsonObject["coins"];
        foreach (var coin in List)
        {
            if (!CoinIconData.List.ContainsKey(coin.Symbol))
            {
                Debug.Log($"{nameof(CoinData)}: {coin.Symbol} - No icon found");
                Debug.Log($"Set {nameof(coin.IconUrl)} to {coin.IconUrl}");
                CoinIconData.List.Add(coin.Symbol, new CoinIcon(coin.Symbol, coin.IconUrl));
            }
        }
    }
}
