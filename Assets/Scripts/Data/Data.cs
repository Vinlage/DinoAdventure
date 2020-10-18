using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Data : MonoBehaviour
{

    public static Data instance;

    [SerializeField]
    private TextMeshProUGUI coinText = default;
    [SerializeField]
    private TextMeshProUGUI coinTotalText = default;

    private int coins;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        InitializeData();
    }

    public void InitializeData()
    {
        coins = 0;
        coinText.text = coins.ToString();
        coinTotalText.text = GetTotalCoinAmount().ToString();
    }

    public void CollectCoin()
    {
        SetCoinAmount(GetTotalCoinAmount()+1);
        coins += 1;
        coinText.text = coins.ToString();
    }

    public void SetBestTime(float time)
    {
        if(time < GetBestTime() || GetBestTime() == -1)
        {
            PlayerPrefs.SetFloat("Time", time);
        }
    }

    public float GetBestTime()
    {
        return PlayerPrefs.GetFloat("Time", -1);
    }

    public void SetCoinAmount(int amount)
    {
        PlayerPrefs.SetInt("CoinAmount", amount);
        coinTotalText.text = GetTotalCoinAmount().ToString();
    }

    public int GetTotalCoinAmount()
    {
        return PlayerPrefs.GetInt("CoinAmount", 0);
    }

    public int GetCoins()
    {
        return coins;
    }
}
