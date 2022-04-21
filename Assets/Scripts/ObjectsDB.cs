using System.Collections.Generic;
using UnityEngine;

public class ObjectsDB : MonoBehaviour
{
    // Objects DB
    // Coins
    [SerializeField] private List<GameObject> coinsList;
    public static List<GameObject> staticCoinsList;

    // PowerUps
    [SerializeField] private List<GameObject> powerupsList;
    public static List<GameObject> staticPowerupsList;

    // Obstacles
    [SerializeField] private List<GameObject> zone1;
    [SerializeField] private List<GameObject> zone2;
    [SerializeField] private List<GameObject> zone3;
    public static List<GameObject> staticZone1;
    public static List<GameObject> staticZone2;
    public static List<GameObject> staticZone3;

    // Counters
    public static int lastNpcSpawned = -1;
    public static int lastObstacleSpawned = -1;
    public static bool zone3Check = false;

    private void Awake()
    {
        staticCoinsList = coinsList;
        staticPowerupsList = powerupsList;
        staticZone1 = zone1;
        staticZone2 = zone2;
        staticZone3 = zone3;
    }
}
