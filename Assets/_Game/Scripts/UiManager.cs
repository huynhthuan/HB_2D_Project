using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    // public static UiManager Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             instance = FindObjectOfType<UiManager>();
    //         }

    //         return instance;
    //     }
    // }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    Text coinText;

    public void SetCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
