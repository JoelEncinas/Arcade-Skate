using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> gatesList;
    [SerializeField] private GameObject player;
    private bool zone2, zone3 = false;

    private void Awake()
    {
        Instantiate(gatesList[0], new Vector3(50f, -0.3f, 0f), Quaternion.identity);
    }

    void Update()
    {
        if (player.transform.position.x >= 700f && !zone3)
        {
            Instantiate(gatesList[2], new Vector3(800f, -0.3f, 0f), Quaternion.identity);
            zone3 = !zone3;
        }


        if (player.transform.position.x >= 150f && !zone2)
        {
            Instantiate(gatesList[1], new Vector3(200f, -0.3f, 0f), Quaternion.identity);
            zone2 = !zone2;
        } 
    }
}
