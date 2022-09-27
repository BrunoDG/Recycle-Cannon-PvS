using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int OrganicAmmo;
    public static int PlasticAmmo;
    public static int MetallicAmmo;

    public int startOrganicAmmo = 0;
    public int startPlasticAmmo = 0;
    public int startMetallicAmmo = 0;

    public static int Lives;
    public int startLives = 3;

    public static int WallHealth;
    public int startWallHealth = 20;

    void Start()
    {
        OrganicAmmo = startOrganicAmmo;
        PlasticAmmo = startPlasticAmmo;
        MetallicAmmo = startMetallicAmmo;
        Lives = startLives;
        WallHealth = startWallHealth;
    }

}
