using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    private float moveSpeed = 2f;
    public GameObject targetPosition;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(originalPosition, targetPosition.transform.position, 
            Mathf.PingPong(Time.deltaTime * moveSpeed, 1f));
    }
}
