using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 playerOffset = new Vector2(-55f, 0);
    private Vector3 playerOffsetZone3 = new Vector2(-120f, 0);

    private void Update()
    {
        if(player.position.x >= 750f)
            transform.position = new Vector2(player.position.x + playerOffsetZone3.x, transform.position.y);
        else
            transform.position = new Vector2(player.position.x + playerOffset.x, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // NPC tag not needed if npc inside of level
        if (collision.gameObject.CompareTag("LevelPart") ||
            collision.gameObject.CompareTag("NPC") ||
            collision.gameObject.CompareTag("Coin"))
            Destroy(collision.gameObject);
    }
}
