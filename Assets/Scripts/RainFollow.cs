using UnityEngine;

public class RainFollow : MonoBehaviour
{
    [SerializeField] private Player player;

    private void LateUpdate()
    {
        transform.position = new Vector2(player.GetPosition().x + 10f, transform.position.y);
    }
}
