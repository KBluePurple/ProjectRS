using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScene : MonoBehaviour
{
    // [RuntimeInitializeOnLoadMethod]
    // static void Initialize()
    // {
    //     Debug.Log($"{nameof(UIManager)}: Initialize");

    //     bool isLoaded = false;
    //     for (int i = 0; i < SceneManager.sceneCount; i++)
    //     {
    //         Scene scene = SceneManager.GetSceneAt(i);
    //         for (int j = 0; j < scene.GetRootGameObjects().Length; j++)
    //         {
    //             if (scene.GetRootGameObjects()[j].name == "UI")
    //             {
    //                 isLoaded = true;
    //                 break;
    //             }
    //         }

    //         if (scene.name == "UIScene")
    //         {
    //             isLoaded = true;
    //             break;
    //         }
    //     }

    //     if (!isLoaded)
    //     {
    //         SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    //     }
    //     else
    //     {
    //         SceneManager.UnloadSceneAsync("UIScene");
    //     }

    //     Debug.Log($"{nameof(UIManager)}: Scene loaded");
    // }

    // private void Awake()
    // {
    //     DontDestroyOnLoad(gameObject);
    //     SceneManager.UnloadSceneAsync("UIScene");
    // }
}
