using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private GameObject powerup;
    [SerializeField] private float extraPowerupHeight;

    private void Awake()
    {
        GeneratePowerup();

        Instantiate(powerup,
            new Vector3(transform.position.x, powerup.transform.position.y - 3.8f + extraPowerupHeight, transform.position.z),
            Quaternion.identity);
    }

    private void GeneratePowerup()
    {
        int random = Random.Range(0, ObjectsDB.staticPowerupsList.Count);
        powerup = ObjectsDB.staticPowerupsList[random];
    }
}
