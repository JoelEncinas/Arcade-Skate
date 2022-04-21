using TMPro;
using UnityEngine;

public class TricksManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI tricksText;
    private float velocityForBigJump = -16f;

    // Jitters if Update is called
    private void FixedUpdate()
    {
        tricksText.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 2f);
    }

    // Big jump check
    public bool CheckIfJumpWasBigAndWasGrounded()
    {
        return player.GetYVelocity() <= velocityForBigJump && player.IsGrounded();
    }
}
