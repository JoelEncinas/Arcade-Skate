using System.Collections;
using UnityEngine;

public class DestroyAfterComplete : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyAfertSeconds(2));
    }

    IEnumerator DestroyAfertSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
