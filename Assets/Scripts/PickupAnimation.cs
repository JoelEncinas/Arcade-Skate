using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimation : MonoBehaviour
{
    private float moveSpeed = 2f;

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 90f, 0),
            Mathf.PingPong(Time.time * moveSpeed, 1f));
    }
}
