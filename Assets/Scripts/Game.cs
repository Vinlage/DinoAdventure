using UnityEngine;
using UnityEngine.Analytics;

public class Game : MonoBehaviour
{
    public static Game instance;

    [SerializeField]
    private Transform playerTransform = default;
    [SerializeField]
    private PlayerController playerController = default;
    [SerializeField]
    private GameObject[] enemiesController = default;
    [SerializeField]
    private GameObject[] platformController = default;
    [SerializeField]
    private GameObject coins = default;
    [SerializeField]
    private GameObject timerController = default;
    [SerializeField]
    private Collectables collectables = default;

    private Vector3 playerSavedPosition = default;
    private Quaternion playerSavedRotation = default;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start() 
    {
        playerSavedPosition = playerTransform.position;
        playerSavedRotation = playerTransform.rotation;
    }

    public void InitializeGame()
    {
        InitializePlayer();
        InitializeEnemies(true);
        InitializePlatforms(true);
        InitializeCoins(true);
        collectables.InitializeData();
        timerController.SetActive(true);

        //criar um evento analytics jogador começou o nivel
        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelStarted");
        Debug.Log("LevelStarted: " + analyticsResult);
    }

    public void InitializePlayer()
    {
        playerTransform.position = playerSavedPosition;
        playerTransform.rotation = playerSavedRotation;
        playerController.enabled = true;
        playerTransform.gameObject.layer = 9;
    }

    public void InitializeEnemies(bool value)
    {
        for(int i=0; i<enemiesController.Length; ++i)
        {
            enemiesController[i].SetActive(value);
        }
    }

    public void InitializePlatforms(bool value)
    {
        for(int i=0; i<platformController.Length; ++i)
        {
            platformController[i].SetActive(value);
        }
    }

    public void InitializeCoins(bool value)
    {
        for(int i=0; i<coins.transform.childCount; ++i)
        {
            coins.transform.GetChild(i).gameObject.SetActive(value);
        }
    }

    public void DisableAll()
    {
        InitializeEnemies(false);
        InitializePlatforms(false);
        InitializeCoins(false);
        timerController.SetActive(false);
    }
}
