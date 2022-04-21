using System.Collections.Generic;
using UnityEngine;

public class RandomNpcSpawner : MonoBehaviour
{
    private LevelGenerator levelGenerator;
    [SerializeField] private bool canMove = false; // standing by default;
    [SerializeField] private bool direction;
    [SerializeField] private bool isDownside = false;
    [SerializeField] private bool isZone3 = false;
    [SerializeField] private float downsideHeight = 0;

    private void Start()
    {
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        if(!isZone3)
            SpawnNPC(levelGenerator.GetNPCList());
        else
            SpawnNPC(levelGenerator.GetRoboNPCList());
    }

    private void SpawnNPC(List<GameObject> NPCList)
    {
        int random;

        do
        {
            random = Random.Range(0, NPCList.Count);
        } while (random == ObjectsDB.lastNpcSpawned);

        ObjectsDB.lastNpcSpawned = random;
        Quaternion npcAngles;

        if (!isDownside)
        {
            npcAngles = Quaternion.identity;

            if (NPCList[random] != null)
                downsideHeight = NPCList[random].transform.position.y;
            else
            {
                random = 0;
                downsideHeight = 0;
            }
        }
        else
        {
            npcAngles = Quaternion.Euler(0, 0, 180);
            if (NPCList[random] == null)
                return;
            else
                downsideHeight = -NPCList[random].transform.position.y + 0.71f;
        }

        GameObject npc = GameObject.Instantiate(
            NPCList[random], 
            new Vector2(transform.position.x, downsideHeight),
            npcAngles);

        npc.GetComponent<NpcBehaviour>().canMove = canMove;

        if (direction)
            npc.GetComponent<NpcBehaviour>().direction = Vector2.right;
        else
            npc.GetComponent<NpcBehaviour>().direction = Vector2.left;
    }
}
