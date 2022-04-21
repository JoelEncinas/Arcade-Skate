using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private UIManager uIManager;
    private Player player;

    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            if (gameObject.name.Contains("Bench") || 
                gameObject.name.Contains("Streetlight") || 
                gameObject.name.Contains("Car") || 
                gameObject.name.Contains("Handrail") ||
                gameObject.name.Contains("FireHydrant"))
            {
                StartCoroutine("DisableAndEnableColliders");
            }
            else
            {
                player.playerLastVelocity = player.velocity;
                if (!player.gravityEnabled)
                    player.diedInPortal = true;
                if(!player.isInmune)
                    uIManager.HandleDead();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerController"))
            if (gameObject.name.Contains("Streetlight"))
                uIManager.IStreetlightFlashMessage();
    }

    IEnumerator DisableAndEnableColliders()
    {
        float timeDisabled = 4f;
        foreach (BoxCollider2D collider in GetComponents<BoxCollider2D>())
            collider.enabled = false;

        yield return new WaitForSeconds(timeDisabled);

        foreach (BoxCollider2D collider in GetComponents<BoxCollider2D>())
            collider.enabled = true;
    }
}
