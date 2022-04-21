using System.Collections;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    static public bool paused = false;

    public void PauseGame()
    {
        AudioManager am = FindObjectOfType<AudioManager>();
        GameObject pauseUI = GameObject.Find("PauseUI").gameObject;
        am.Play("Pause");
        paused = !paused;
        Player.pauseCheck = false;

        if(paused)
        {
            Time.timeScale = 0;
            am.Pause("Theme");
            for(int i = 0; i < pauseUI.transform.childCount; i++)
            {
                pauseUI.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
            am.Play("Theme");
            for (int i = 0; i < pauseUI.transform.childCount; i++)
            {
                pauseUI.transform.GetChild(i).gameObject.SetActive(false);
            }
            StartCoroutine(pause());
        }
    }

    // Fixes touch problems
    IEnumerator pause()
    {
        yield return new WaitForSeconds(0.1f);
        Player.pauseCheck = true;
    }
}
