using UnityEngine;

public class RandomObstacleSpawner : MonoBehaviour
{
    private Player player;
    private int newRandomObstacle;
    private GameObject obstacle;
    [SerializeField] private float extraObstacleHeight;
    [SerializeField] private bool isDownside = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player>();

        GenerateObstacle();

        if (!isDownside)
            Instantiate(obstacle,
                new Vector3(transform.position.x, obstacle.transform.position.y - 3.8f + extraObstacleHeight, transform.position.z),
                Quaternion.identity);
        if (isDownside)
            Instantiate(obstacle,
                new Vector3(transform.position.x, 4.65f, transform.position.z),
                Quaternion.Euler(0, 0, 180));
    }

    private void GenerateObstacle()
    {
        if (player.transform.position.x >= 750f)
        {
            newRandomObstacle = Random.Range(0, ObjectsDB.staticZone3.Count);
            obstacle = ObjectsDB.staticZone3[newRandomObstacle];
        }

        if (player.transform.position.x >= 150f && player.transform.position.x < 750f)
        {
            do
            {
                newRandomObstacle = Random.Range(0, ObjectsDB.staticZone2.Count);
            } while (newRandomObstacle == ObjectsDB.lastObstacleSpawned);

            obstacle = ObjectsDB.staticZone2[newRandomObstacle];
            newRandomObstacle = ObjectsDB.lastObstacleSpawned;
        }

        if (player.transform.position.x < 150f)
        {
            do
            {
                newRandomObstacle = Random.Range(0, ObjectsDB.staticZone1.Count);
            } while (newRandomObstacle == ObjectsDB.lastObstacleSpawned);

            obstacle = ObjectsDB.staticZone1[newRandomObstacle];
            newRandomObstacle = ObjectsDB.lastObstacleSpawned;
        }
    }
}
