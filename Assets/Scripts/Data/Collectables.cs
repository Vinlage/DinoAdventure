using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Collectables : MonoBehaviour
{
    public static Collectables instance;

    [SerializeField]
    private TextMeshProUGUI coinText = default;
    [SerializeField]
    private TextMeshProUGUI coinTotalText = default;
    [SerializeField]
    private AudioSource coinAudioSource;

    public int coins {get; private set;} 

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start() 
    {
        InitializeData();
    }

    public void InitializeData()
    {
        coins = 0;
        coinText.text = coins.ToString();
        SetTotalCoinsText();
    }

    public void SetTotalCoinsText()
    {
        coinTotalText.text = Data.instance.GetTotalCoinAmount().ToString();
    }

    public void CollectCoin()
    {
        Data.instance.SetCoinAmount(Data.instance.GetTotalCoinAmount()+1);
        coins += 1;
        coinText.text = coins.ToString();
    }

    public void PlayCoinSound()
    {
        coinAudioSource.Play();
    }
}
