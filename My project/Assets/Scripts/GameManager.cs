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
    int highscore;
    [SerializeField]
    GameObject startScreen;
    [SerializeField]
    GameObject loseScreen;
    [SerializeField]
    GameObject winScreen;
    [SerializeField]
    TMP_Text scoreText;
    [SerializeField]
    TMP_Text highscoreText;
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
        Time.timeScale = 0;
        highscoreText.text = "Highscore: 0";
        
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startScreen.SetActive(false);
        LoadLevel(currentLevel);
    }
    public void NextPlatform()
    {
        //if no more platform in that level -> win
        if (platformCount > platformTotal)
        {
            winScreen.SetActive(true);
            return;
        }
        //destroy previous platform
        if (currentPlatform != null)
        {
            Destroy(currentPlatform);
        }


        score++;
        scoreText.text = score.ToString();

        GameObject helixPlatform = Instantiate(helixPlatformPrefab, Vector3.up * -score * platformDist , Quaternion.identity);

        //play Audio + change pitch
        AudioSource audio = helixPlatform.GetComponent<AudioSource>();
        audio.pitch = Mathf.Pow(2, level.platforms[platformCount].pitch / 12);
        audio.Play();

        //make empty holes
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

        //make death parts
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
        if (score < highscore)
            return;    
        highscore = score;
        highscoreText.text = "Highscore: " + highscore;
    }
    public void RestartLevel()
    {
        ballController.losePos = ballController.gameObject.transform.position; 
        score = -1;
        loseScreen.SetActive(false);
        ballController.ResetPosition();
        LoadLevel(0);
    }

    public void NextLevel()
    {
        winScreen.SetActive(false);
        currentLevel++;
        LoadLevel(currentLevel);
    }

    public  void LoadLevel( int levelNumber)
    {
        //make sure current level doesnt exceed premade level count
        if (levelNumber > allLevels.Count - 1)
        {
            //go back to first level
            currentLevel = 0;
            level = allLevels[0];
        }
        else
        {
            level = allLevels[levelNumber];
        }
        Camera.main.backgroundColor = level.backgroundColor;
        platformTotal = level.platforms.Count-1;
        platformCount = 0;
        NextPlatform();
    }
}
