using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private float moveSpeed = 4f;
    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        if (ObjectsDB.zone3Check)
            moveSpeed = Random.Range(4, 10);
    }

    private void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
        transform.Rotate(Vector3.forward, 360f * Time.deltaTime / 1);
    }
}
