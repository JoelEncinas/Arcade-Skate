using UnityEngine;

public class TimedObstacle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer pivotLight;
    private BoxCollider2D boxCollider2D;
    private float timerBaseTime = 2f;
    private float timer;
    private bool isTrigger;
    private bool colorTracker = true;

    private Color32 green = new Color32(17, 150, 0, 110);
    private Color32 red = new Color32(150, 0, 5, 110);

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        pivotLight = transform.GetChild(2).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        ChangeLights(green);
        boxCollider2D.enabled = false;
        timer = timerBaseTime;
    }

    private void Update()
    {
        ChangeTimer();
        if (colorTracker)
            ChangeLights(green);
        else
            ChangeLights(red);
    }

    private void ChangeIsTrigger()
    {
        isTrigger = !isTrigger;
        colorTracker = !colorTracker;
        boxCollider2D.enabled = !boxCollider2D.enabled;
    }

    private void ChangeTimer()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            ChangeIsTrigger();
            timer = timerBaseTime;
        }
    }

    private void ChangeLights(Color32 color)
    {
        spriteRenderer.color = color;
        pivotLight.color = color;
    }
}
