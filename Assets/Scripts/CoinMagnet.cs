using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "PlayerController")
        {
            player.ActivateMagnetCorroutine();
            FindObjectOfType<AudioManager>().PlayClipAtPoint("Magnet", transform.position);
            if (transform.childCount == 1)
                Destroy(transform.GetChild(0).gameObject);
        }
    }
}
