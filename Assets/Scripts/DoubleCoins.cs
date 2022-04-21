using UnityEngine;

public class DoubleCoins : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerController")
        {
            player.ActivateDoubleCoinsCorroutine();
            FindObjectOfType<AudioManager>().PlayClipAtPoint("DoubleCoins", transform.position);
            if(transform.childCount == 1)
                Destroy(transform.GetChild(0).gameObject);
        }
    }
}
