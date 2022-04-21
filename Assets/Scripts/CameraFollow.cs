using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player player;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.GetPosition().x + 6f, transform.position.y, transform.position.z);
    }
}
