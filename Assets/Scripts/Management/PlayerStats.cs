using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static string AmmoType;
    public string startAmmoType = "Metallic";

    public static int TotalAmmo;
    public int startTotalAmmo = 5;

    public static int Lives;
    public int startLives = 3;

    public static float WallHealth;
    public float startWallHealth = 20;

    void Start()
    {
        AmmoType = startAmmoType;
        TotalAmmo = startTotalAmmo;
        Lives = startLives;
        WallHealth = startWallHealth;
    }

}
