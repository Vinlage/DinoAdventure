using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Data : MonoBehaviour
{
    public static Data instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SetBestTime(float time)
    {
        print("time = " + GetBestTime() );
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
        Collectables.instance.SetTotalCoinsText();
    }

    public int GetTotalCoinAmount()
    {
        return PlayerPrefs.GetInt("CoinAmount", 0);
    }
}
