using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BallController ballController;

    GameObject currentPlatform;

    [SerializeField]
    float platformDist;

    [SerializeField]
    GameObject helixPlatformPrefab;

    int platformCount;

    [SerializeField]
    int platformTotal;

    int score;


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
        currentPlatform = Instantiate(helixPlatformPrefab, Vector3.zero, Quaternion.identity);
    }

    public void NextPlatform()
    {
        if (platformCount >= platformTotal)
        {
            NextLevel();
            return;
        }
        currentPlatform.SetActive(false);
        score++;
        platformCount++;
        Debug.Log(score);
        GameObject helixPlatform = Instantiate(helixPlatformPrefab, Vector3.up * -score * platformDist , Quaternion.identity);
        helixPlatform.name = "HelixPlatform " + platformCount;
        currentPlatform= helixPlatform;
    }

    public void NextLevel()
    {

    }

    public void RestartLevel()
    {
        score = 0;
        ballController.ResetPosition();
    }
}
