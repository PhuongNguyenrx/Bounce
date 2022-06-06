using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BallController ballController;

    GameObject currentPlatform;

    [SerializeField]
    float platformDist;

    [SerializeField]
    GameObject helixPlatformPrefab;

    int platformCount=1;
    int platformTotal;

    public List<Level> allLevels = new List<Level>();
    Level level;
    //private List<GameObject> spawnedPlatforms = new List<GameObject>();
    int score=-1;
    [SerializeField]
    GameObject loseScreen;
    [SerializeField]
    GameObject winScreen;
    [SerializeField]
    TMP_Text scoreText;
    int currentLevel = 0;

    public static GameManager singleton;

    void Awake()
    {
        if (singleton == null) 
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        LoadLevel(currentLevel);
    }

    public void NextPlatform()
    {
        if (platformCount > platformTotal)
        {
            winScreen.SetActive(true);
            return;
        }
        if (currentPlatform != null)
        {
            Destroy(currentPlatform);
        }


        score++;
        scoreText.text = score.ToString();

        GameObject helixPlatform = Instantiate(helixPlatformPrefab, Vector3.up * -score * platformDist , Quaternion.identity);

        int PartsToDisable = 12 - level.platforms[platformCount].helixPartCount;
        List<GameObject> disabledParts = new List<GameObject>();
        while (disabledParts.Count < PartsToDisable)
        {
            GameObject randomPart = helixPlatform.transform.GetChild(Random.Range(0, helixPlatform.transform.childCount)).gameObject;
            if (!disabledParts.Contains(randomPart))
            {
                randomPart.SetActive(false);
                disabledParts.Add(randomPart);
            }
        }

        List<GameObject> deathParts = new List<GameObject>();
        while(deathParts.Count < level.platforms[platformCount].deathPartCount)
        {
            GameObject randomPart = helixPlatform.transform.GetChild(Random.Range(0, helixPlatform.transform.childCount)).gameObject;
            if (!deathParts.Contains(randomPart))
            {
                randomPart.gameObject.AddComponent<DeathPart>();
                deathParts.Add(randomPart);
            }
        }
        platformCount++;
        helixPlatform.name = "HelixPlatform " + platformCount;
        currentPlatform= helixPlatform;
    }

    public void GameOver()
    {
        loseScreen.SetActive(true);
    }
    public void RestartLevel()
    {
        score = -1;
        loseScreen.SetActive(false);
        ballController.ResetPosition();
        LoadLevel(currentLevel);
    }

    public void NextLevel()
    {
        winScreen.SetActive(false);
        currentLevel++;
        LoadLevel(currentLevel);
    }

    public  void LoadLevel( int levelNumber)
    {
        level = allLevels[Mathf.Clamp(levelNumber, 0, allLevels.Count - 1)];
        if (level == null)
        {
            return;
        }
        Camera.main.backgroundColor = allLevels[levelNumber].backgroundColor;
        ballController.GetComponent<Renderer>().material.color = allLevels[levelNumber].ballColor;
        platformTotal = level.platforms.Count-1;
        platformCount = 0;
        NextPlatform();
    }
}
