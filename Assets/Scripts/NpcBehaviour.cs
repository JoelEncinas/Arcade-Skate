using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 direction = Vector2.zero;
    public bool canMove = false; // standing by default

    private void Awake()
    {
        moveSpeed = Random.Range(1f, 3f);
    }

    private void Update()
    {
        if(canMove)
            transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
