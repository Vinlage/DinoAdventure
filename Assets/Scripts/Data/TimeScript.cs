using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText = default;

    private static string timeFormat;
    private static float timeElapsed;
    private bool timeCounting;

    private void OnEnable()
    {
        timeCounting = true;
        timeElapsed = 0;
        timeText.text = "Tempo: 00:00";
    }

    void OnDisable()
    {
        timeCounting = false;
    }

    private void Update()
    {
        if (timeCounting)
        {
            TimeCount();
        }
    }

    public void TimeCount()
    {
        timeElapsed += Time.deltaTime;
        timeText.text = "Tempo: " + FormatTime(timeElapsed);
    }

    public static float GetTime()
    {
        return timeElapsed;
    }

    public static string FormatTime(float time)
    {
        string minutes = Mathf.Floor((time) / 60).ToString("00");
        string seconds = Mathf.Floor((time) % 60).ToString("00");

        return string.Format("{0}:{1}", minutes, seconds);
    }

    public static string GetFormattedTime()
    {
        return FormatTime(timeElapsed);
    }
}
