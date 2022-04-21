using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private DataManagerScript dataManagerScript;
    [SerializeField] private Animator animatorLoading;
    [SerializeField] private Animator animatorFade;
    private bool loadAnimation = false;
    private AsyncOperation operation;

    private void Awake()
    {
        StartCoroutine(FakeDelay(1f));
    }

    private void Update()
    {
        // rewrite code here
        if (loadAnimation)
        {
            StartCoroutine(LoadSceneAsync(1.4f, 2.4f)); // 0.1f to avoid bug
            loadAnimation = !loadAnimation;
        }
    }

    IEnumerator LoadSceneAsync(float firstAnimation, float secondAnimation)
    {
        yield return new WaitForSeconds(firstAnimation);
        animatorFade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(secondAnimation);
        operation.allowSceneActivation = true;
    }

    IEnumerator FakeDelay(float seconds)
    {
        yield return new WaitForSeconds(0.1f);

        if (dataManagerScript.LoadTutorial() == 1)
            operation = SceneManager.LoadSceneAsync(1);
        else
            operation = SceneManager.LoadSceneAsync(2);

        operation.allowSceneActivation = false;

        yield return new WaitForSeconds(seconds);
        animatorLoading.SetTrigger("FireAnimation");
        loadAnimation = !loadAnimation;
    }
}
