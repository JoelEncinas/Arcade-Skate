using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private DataManagerScript dataManagerScript;
    AsyncOperation operation;

    // animations
    [SerializeField] private Animator animatorFade;
    [SerializeField] private Animator animatorTapToContinue;
    [SerializeField] private Animator animatorTitle;

    [SerializeField] private Animator animatorJumpTitle;
    [SerializeField] private Animator animatorJump;
    // [SerializeField] private Animator animatorPlayer;
    // [SerializeField] private Animator animatorBench;

    [SerializeField] private Animator animatorObstaclesTitle;
    [SerializeField] private Animator animatorObstacles;
    [SerializeField] private Animator animatorExclamation;

    [SerializeField] private Animator animatorPowerupsTitle;
    [SerializeField] private Animator animatorPowerups;

    // animation checks
    private bool hasJumpAnimationFinished = false;
    private bool hasObstaclesAnimationFinished = false;
    private bool hasPowerupsAnimationFinished = false;
    private bool disableJump = false;
    private bool disableObstacles = false;
    private bool canLoadGame = true;

    private void Awake()
    {
        TriggerTitle();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Input.touchCount > 0))
        {
            if (hasJumpAnimationFinished && !disableJump)
            {
                // obstacles
                StartCoroutine(CanTap(0));
                StartCoroutine(DesactivateJump(1f));
                disableJump = !disableJump;
                StartCoroutine(ActivateObstacles(4f));
            }

            if (hasObstaclesAnimationFinished && !disableObstacles)
            {
                // powerups
                StartCoroutine(CanTap(0));
                StartCoroutine(DesactivateObstacles(1f));
                disableObstacles = !disableObstacles;
                StartCoroutine(ActivatePowerups(2f));
            }

            if(hasPowerupsAnimationFinished && canLoadGame)
            {
                dataManagerScript.ResetData();
                // load game
                StartCoroutine(FakeDelay(1f));
                canLoadGame = !canLoadGame;
            }
        }
    }

    private void TriggerTitle()
    {
        animatorTitle.SetTrigger("TitleText");
        StartCoroutine(ActivateJump(4f));
    }

    private void TriggerJump()
    {
        animatorJumpTitle.SetTrigger("JumpText");
        animatorJump.SetTrigger("Obstacles");
    }

    IEnumerator ActivateJump(float seconds)
    {
        TriggerJump();
        yield return new WaitForSeconds(seconds);
        StartCoroutine(CanTap(0));
        hasJumpAnimationFinished = !hasJumpAnimationFinished;
    }

    IEnumerator DesactivateJump(float seconds)
    {
        TriggerJump();
        yield return new WaitForSeconds(seconds);
    }

    private void TriggerObstacles()
    {
        animatorObstaclesTitle.SetTrigger("ObstaclesTitle");
        animatorObstacles.SetTrigger("Obstacles");
        animatorExclamation.SetTrigger("Exclamation");
    }

    IEnumerator ActivateObstacles(float seconds)
    {
        TriggerObstacles();
        yield return new WaitForSeconds(seconds);
        StartCoroutine(CanTap(0));
        hasObstaclesAnimationFinished = !hasObstaclesAnimationFinished;
    }

    IEnumerator DesactivateObstacles(float seconds)
    {
        TriggerObstacles();
        yield return new WaitForSeconds(seconds);
    }

    private void TriggerPowerUps()
    {

        animatorPowerupsTitle.SetTrigger("PowerupsTitle");
        animatorPowerups.SetTrigger("Powerups");
    }

    IEnumerator ActivatePowerups(float seconds)
    {
        TriggerPowerUps();
        yield return new WaitForSeconds(seconds);
        StartCoroutine(CanTap(0));
        hasPowerupsAnimationFinished = !hasPowerupsAnimationFinished;
    }

    IEnumerator CanTap(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animatorTapToContinue.SetTrigger("TapToContinue");
    }

    IEnumerator FakeDelay(float seconds)
    {
        yield return new WaitForSeconds(0.1f);

        dataManagerScript.SaveTutorial();
        operation = SceneManager.LoadSceneAsync(0);
        operation.allowSceneActivation = false;

        yield return new WaitForSeconds(seconds);
        animatorFade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(seconds);
        operation.allowSceneActivation = true;
    }
}
