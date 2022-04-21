using System.Collections.Generic;
using UnityEngine;

public class RandomCoinsSpawner : MonoBehaviour
{
    [SerializeField] private bool randomPosition = true;
    [SerializeField] private bool isDownside = false;
    private int positionOffset; 
    

    private void Start()
    {
        if (randomPosition)
            positionOffset = Random.Range(1, -1);

        if (!isDownside)
            Instantiate(ObjectsDB.staticCoinsList[Random.Range(0, ObjectsDB.staticCoinsList.Count)],
                new Vector3(transform.position.x, transform.position.y + positionOffset, transform.position.z),
                Quaternion.identity);
        else
            Instantiate(ObjectsDB.staticCoinsList[Random.Range(0, ObjectsDB.staticCoinsList.Count)],
                new Vector3(transform.position.x, transform.position.y + positionOffset, transform.position.z),
                Quaternion.Euler(0, 0, 180));
    }
}
