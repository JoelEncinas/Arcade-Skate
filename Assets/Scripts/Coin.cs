using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinMove coinMove;
    private int coinValue = 1;

    void Start()
    {
        coinMove = gameObject.GetComponent<CoinMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCoinMagnetCollider"))
            coinMove.enabled = true;

        if (collision.gameObject.CompareTag("CoinCollector") && coinMove.enabled)
        {
            ScoreManager.instance.ChangeCoinScore(coinValue);
            FindObjectOfType<AudioManager>().PlayClipAtPoint("Coin", transform.position);
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("PlayerController"))
        {
            ScoreManager.instance.ChangeCoinScore(coinValue);
            FindObjectOfType<AudioManager>().PlayClipAtPoint("Coin", transform.position);
            Destroy(gameObject);
        }
    }
}

