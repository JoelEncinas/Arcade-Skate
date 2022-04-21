using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxMultiplier;
    public GameObject ObjectToFollow;
    private float spriteWidth;
    private float startPosition;

    private void Start()
    {
        startPosition = transform.position.x;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = ObjectToFollow.transform.position.x * (1 - parallaxMultiplier);
        float distance = ObjectToFollow.transform.position.x * parallaxMultiplier;
        transform.position = new Vector2(startPosition + distance, transform.position.y);

        if (temp > startPosition + spriteWidth)
            startPosition += spriteWidth * 2;
    }
}
