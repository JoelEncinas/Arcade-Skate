using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PauseButton pauseButton;

    public void Awake()
    {
        // reset physics 
        Physics2D.gravity = new Vector2(0, -9.8f);

        // makes screen not shut off if idle
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}