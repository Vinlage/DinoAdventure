using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private TextMeshProUGUI popupText;

    string gameId = "3868531";
    bool testMode = true;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    // Update is called once per frame
    public static void PlayInterstitial()
    {
        if (Advertisement.IsReady()) {
            Advertisement.Show("video");
        } 
        else {
            Debug.Log("Ads nao esta disponivel");
        }
    }

    public void PlayRewardedVideoAd()
    {
        if (Advertisement.IsReady()) {
            Advertisement.Show("rewardedVideo");
        } 
        else {
            Debug.Log("Ads nao esta disponivel");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        popup.SetActive(true);
        popupText.text = "Erro! Por favor verifique sua conexão!";
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult)
        {
            case ShowResult.Failed:
                popup.SetActive(true);
                popupText.text = "Erro! Por favor verifique sua conexão!";
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:
                if(placementId == "rewardedVideo")
                {
                    Data.instance.SetCoinAmount(Data.instance.GetTotalCoinAmount()+10);
                    popup.SetActive(true);
                    popupText.text = "Parabens! Você ganhou 10 moedas!";
                }
                break;
        }
        //throw new System.NotImplementedException();
    }
}
