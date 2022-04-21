using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 30f;
    [SerializeField] private Transform fallbackLevel;
    [SerializeField] private List<Transform> zone1;
    [SerializeField] private List<Transform> zone2;
    [SerializeField] private List<Transform> zone3;
    [SerializeField] private Player player;
    private int lastRandomLevel = -1;
    private int newRandomLevel;

    // change this value if more NPCs added to resources folder
    private int numberOfAssets = 12;
    private List<GameObject> NPCList;
    private List<GameObject> RoboNPCs;

    private Vector2 lastEndPosition;

    private void Awake()
    {
        lastRandomLevel = 0;
        // first end position is start level position end
        lastEndPosition = new Vector2(50, 0);
        GenerateNPCList();
    }

    private void Update()
    {
        if(Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            // spawn another level part when player near endPosition of level
            SpawnLevelPart();
    }

    private void SpawnLevelPart()
    {
        Transform levelPart = fallbackLevel;

        if (player.transform.position.x >= 750f)
        {
            do
                newRandomLevel = Random.Range(0, zone3.Count);
            while (newRandomLevel == lastRandomLevel);
            
            levelPart = zone3[newRandomLevel];
            lastRandomLevel = newRandomLevel;
        }

        // 150 diff as each level is 50f 
        // last zone1 level has to spawn at 150f
        if (player.transform.position.x >= 150f && player.transform.position.x < 750f)
        {
            do
            {
                newRandomLevel = Random.Range(0, zone2.Count);
            } while (newRandomLevel == lastRandomLevel);

            levelPart = zone2[newRandomLevel];
            lastRandomLevel = newRandomLevel;
        }

        if (player.transform.position.x < 150f)
        {
            do
            {
                newRandomLevel = Random.Range(0, zone1.Count);
            } while (newRandomLevel == lastRandomLevel);

            levelPart = zone1[newRandomLevel];
            lastRandomLevel = newRandomLevel;
        }

        Transform levelPartTransform = Instantiate(levelPart, lastEndPosition, Quaternion.identity);
        
        if(player.transform.position.x <= 750f)
            lastEndPosition += new Vector2(50, 0);
        if (player.transform.position.x > 750f)
            lastEndPosition += new Vector2(100, 0);
    }

    // NPCS
    private void GenerateNPCList()
    {
        NPCList = new List<GameObject>();

        for (int i = 1; i < numberOfAssets; i++)
            NPCList.Add(Resources.Load("Npcs/Person " + i) as GameObject);

        RoboNPCs = new List<GameObject>();

        for (int i = 1; i < numberOfAssets; i++)
            RoboNPCs.Add(Resources.Load("_Npcs/Robot " + i) as GameObject);
    }

    public List<GameObject> GetNPCList()
    {
        return NPCList;
    }

    public List<GameObject> GetRoboNPCList()
    {
        return RoboNPCs;
    }
}
