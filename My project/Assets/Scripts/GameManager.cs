using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform topPlatform;
    [SerializeField]
    private Transform bottomPlatform;
    [SerializeField]
    private int platformCount;
    private float platformDist;
    [SerializeField]
    private GameObject helixPlatformPrefab;
    private int disabledHelixPart;

    void Awake()
    {
        Transform helix = GameObject.FindWithTag("Helix").transform;
        platformDist = (topPlatform.position.y - bottomPlatform.position.y) / platformCount;
        Debug.Log(bottomPlatform.position.y);
        for (int i = 0; i < platformCount; i++)
        {
            GameObject helixPlatform = Instantiate(helixPlatformPrefab, topPlatform.position - Vector3.up * i*platformDist, Quaternion.identity,helix);
            helixPlatform.name = "HelixPlatform " + i;
        }
    }
}
