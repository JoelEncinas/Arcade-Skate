using System.Collections;
using UnityEngine;

public class TimedObstacleSlider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            // know aprox if player is grounded
            // player.GetYVelocity() != 0

            StartCoroutine(DisableAndEnableColliders());
        }
    }

    IEnumerator DisableAndEnableColliders()
    {
        float timeDisabled = 4f;
        foreach (BoxCollider2D collider in GetComponents<BoxCollider2D>())
        {
            collider.enabled = false;
        }
        yield return new WaitForSeconds(timeDisabled);
        foreach (BoxCollider2D collider in GetComponents<BoxCollider2D>())
        {
            collider.enabled = true;
        }
    }
}
