using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject roboBall;
    [SerializeField] private int extraBallHeight = 0;
    [SerializeField] private bool extraBallHeightActive = false;

    private void Awake()
    {
        if(!extraBallHeightActive)
            extraBallHeight = Random.Range(0, 4);
        if(!ObjectsDB.zone3Check)
            Instantiate(ball,
                new Vector3(transform.position.x, ball.transform.position.y - 3.8f + extraBallHeight, transform.position.z),
                Quaternion.identity);
        else
            Instantiate(roboBall,
                new Vector3(transform.position.x, ball.transform.position.y - 3.8f + extraBallHeight, transform.position.z),
                Quaternion.identity);
    }
}
