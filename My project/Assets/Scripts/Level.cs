using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlatformManager
{
    [Range(1,11)]
    public int helixPartCount;

    public int deathPartCount;

}
[CreateAssetMenu(fileName="NewLevel")]
public class Level: ScriptableObject
{
    public Color backgroundColor = Color.white;
    public Color ballColor = Color.white;
    public List<PlatformManager> platforms = new List<PlatformManager>();
}