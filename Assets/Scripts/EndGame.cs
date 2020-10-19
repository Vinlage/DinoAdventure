using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;

public class EndGame : MonoBehaviour
{
    public static EndGame instance;

    [SerializeField]
    private GameObject endScreen = default;
    [SerializeField]
    private TextMeshProUGUI finalMessage = default;
    [SerializeField]
    private TextMeshProUGUI coinsCollected = default;
    [SerializeField]
    private TextMeshProUGUI coinsTotal = default;
    [SerializeField]
    private TextMeshProUGUI time = default;
    [SerializeField]
    private TextMeshProUGUI bestTime = default;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator EndScreenWait(bool wait, bool success)
    {
        if(wait)
        {
            yield return new WaitForSeconds(2);
        }
        else
        {
            yield return null;
        }

        AdsManager.PlayInterstitial();
        Game.instance.DisableAll();

        if(success)
        {
            LevelSuccess();
        }
        else
        {
            LevelFailed();
        }

        coinsTotal.text = "Moedas Totais: " + Data.instance.GetTotalCoinAmount();
        coinsCollected.text = "Moedas Coletadas: " + Data.instance.GetCoins();

        float bestTimeNumber = Data.instance.GetBestTime();
        bestTime.text = bestTimeNumber > 0 ? "Melhor Tempo: " + TimeScript.FormatTime(bestTimeNumber) : "Melhor Tempo: Não Completado!";

        endScreen.SetActive(true);
    }

    public void LevelSuccess()
    {
        finalMessage.text = "Parabens! Level Concluído.";
        time.text = "Tempo: " + TimeScript.GetFormattedTime();
        Data.instance.SetBestTime(TimeScript.GetTime());

        AnalyticsResult analyticsResult = Analytics.CustomEvent("CompletedLevel");
        Debug.Log("CompletedLevel: " + analyticsResult);
    }

    public void LevelFailed()
    {
        finalMessage.text = "Voce Morreu! Tente novamente.";
        time.text = "Tempo: Não Completado!";

        AnalyticsResult analyticsResult = Analytics.CustomEvent("FailedLevel");
        Debug.Log("FailedLevel: " + analyticsResult);
    }
}
