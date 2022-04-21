using UnityEngine;

public class CoinMove : MonoBehaviour
{
    private float speed = 15f;
    private Vector2 target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerCoinCollider").transform.position;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
}
