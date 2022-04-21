using UnityEngine;

public class ImprovedJump : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float fallMultiplier = 2.5f;
    private float gravityCache;
    private Player player;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        gravityCache = Physics2D.gravity.y * (fallMultiplier - 1);
    }

    private void Update()
    {
        // checks if player is falling and increases gravity for crisper jump curve
        // 2/3 of jump is up and 1/3 is down
        if(rigidbody2d.velocity.y < 0 && player.gravityEnabled)
            rigidbody2d.velocity += Vector2.up * gravityCache * Time.deltaTime;

        if (rigidbody2d.velocity.y > 0 && !player.gravityEnabled)
            rigidbody2d.velocity += Vector2.down * gravityCache * Time.deltaTime;
    }
}
