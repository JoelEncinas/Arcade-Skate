using UnityEngine;

public class Stabilicer : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            if(player.manualCheck)
                player.DisableManual();
        }
    }
}
