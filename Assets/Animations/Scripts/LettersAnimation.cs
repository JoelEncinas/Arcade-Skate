using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersAnimation : MonoBehaviour
{
    private List<Animator> _animators;
    private float waitBetween = 0.15f;
    private float waitBetweenAnimation = 1f;

    private void Start()
    {
        _animators = new List<Animator>(GetComponentsInChildren<Animator>());

        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        while (true)
        {
            foreach(var animator in _animators)
            {
                animator.SetTrigger("DoAnimation");
                yield return new WaitForSeconds(waitBetween);
            }

            yield return new WaitForSeconds(waitBetweenAnimation);
        }
    }
}
