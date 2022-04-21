using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // --- UI ---
    public TextMeshProUGUI bestDistanceText;
    public Transform bestDistanceSprite;
    [SerializeField] private TextMeshProUGUI initialMessageText;
    [SerializeField] private TextMeshProUGUI gameLogoText;
    [SerializeField] private GameObject deadMenuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject preDeath;
    [SerializeField] private GameObject extraLifeUI;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinsCounterText;
    public TextMeshProUGUI distanceCounterText;
    public TextMeshProUGUI tricksText;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI newRecordText;
    public GameObject buyLifeButton;
    public Button replayButton;
    public Button shopButton;
    private GameObject pauseButton;


    // animations
    [SerializeField] private Animator deadMenuUIAnimator;
    [SerializeField] private Animator extraLifeAnimator;

    // --- SCRIPTS --- 
    [SerializeField] private Shop shop;
    [SerializeField] private Player player;
    [SerializeField] private TricksManager tricksManager;
    [SerializeField] private CameraFollow cameraScript;
    [SerializeField] private DataManagerScript dataManager;
    private TrickMessages trickMessages;

    // --- GAME FLOW
    public bool grindCheck = false;
    public bool hasGameEnded = false;
    public bool highScore = false;
    public bool extraLifeUsed = false;
    public bool trickDone = false;
    public float trickDoneCounter = 0f;

    // tricks coins value
    public int regularTrick = 20;
    public int grindTrick = 30;
    private int lifeValue = 2500;

    // credits
    [SerializeField] private Canvas creditsCanvas;
    [SerializeField] private Button creditsBtn;

    private void Awake()
    {
        pauseButton = gameUI.GetComponentInChildren<Transform>(true).GetChild(0).gameObject;
        trickMessages = GetComponent<TrickMessages>();

        // load data
        dataManager.LoadGame();
        bestDistanceText.text = dataManager.GetDistanceSaved().ToString() + "m";
    }

    private void Update()
    {
        if (player.velocity > 0 && !hasGameEnded)
        {
            IDisableBestDistanceText();
            IDisableInitialMessageText();
            IDisableGameLogo();
            IDisableCredits();
            StartCoroutine(EnableGameUIWithDelay());
        }

        if (tricksManager.CheckIfJumpWasBigAndWasGrounded() && !trickDone)
        {
            trickDone = !trickDone;
            trickDoneCounter = Time.time + 0.3f;
            ScoreManager.instance.ChangeCoinScore(regularTrick);
            StartCoroutine(FlashMessage(trickMessages.GetRandomJumpMessage()));
        }

        if (grindCheck)
        {
            if (player.manualCheck)
            {
                ScoreManager.instance.ChangeCoinScore(grindTrick);
                StartCoroutine(FlashMessage(trickMessages.GetRanndomGrindManualMessage()));
            }

            else
            {
                ScoreManager.instance.ChangeCoinScore(grindTrick);
                StartCoroutine(FlashMessage(trickMessages.GetRandomGrindMessage()));
            }

            grindCheck = false;
        }

        if(trickDoneCounter < Time.time && trickDone)
        {
            trickDone = !trickDone;
            trickDoneCounter = 0f;
        }
    }

    public void IStreetlightFlashMessage()
    {
        ScoreManager.instance.ChangeCoinScore(regularTrick);
        StartCoroutine(FlashMessage(trickMessages.GetRandomStreetlightMessage()));
    }

    IEnumerator DisableBestDistanceText()
    {
        yield return new WaitForSeconds(5);
        bestDistanceText.gameObject.GetComponent<UpdatePosition>().enabled = false;
        bestDistanceText.gameObject.SetActive(false);
    }

    public void IDisableBestDistanceText()
    {
        StartCoroutine(DisableBestDistanceText());
    }

    public void IDisableInitialMessageText()
    {
        StartCoroutine(DisableInitialMessageText());
    }

    public void IDisableGameLogo()
    {
        StartCoroutine(DisableGameLogo());
    }
    public void IDisableCredits()
    {
        StartCoroutine(DisableCredits());
    }

    IEnumerator DisableInitialMessageText()
    {
        yield return new WaitForSeconds(5);
        initialMessageText.gameObject.SetActive(false);
    }

    IEnumerator DisableGameLogo()
    {
        yield return new WaitForSeconds(5);
        gameLogoText.gameObject.SetActive(false);
    }

    IEnumerator DisableCredits()
    {
        yield return new WaitForSeconds(5);
        creditsBtn.gameObject.SetActive(false);
    }

    public void HandleDead()
    {
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Play("Death");

        player.IncreaseOrderInLayer();
        player.DisableColliders();
        player.RotateToZero();
        hasGameEnded = true;

        // enable death animation
        player.DeathAnimation();
        StartCoroutine(DeadCorroutine());
        StartCoroutine(DeathMenuDelay());
    }

    IEnumerator DeathMenuDelay()
    {
        EnablePreDeath();
        DisablePauseButton();
        yield return new WaitForSeconds(2.2f);
        ShowDataDeadUI();
        DisablePreDeath();
        DisableGameUI();
        EnableDeadMenuUI();
    }

    IEnumerator DeadCorroutine()
    {
        yield return new WaitForSeconds(2);

        player.DisableManual();
        player.transform.rotation = Quaternion.Euler(0, 0, 0);

        // leave it as dynamic + freeze in order to avoid velocity warnings
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.velocity = 0;
    }

    public void IExtraLife()
    {
        FindObjectOfType<AudioManager>().Play("Play");
        shopButton.interactable = false;
        replayButton.interactable = false;
        buyLifeButton.GetComponent<Button>().interactable = false;
        extraLifeUsed = !extraLifeUsed;

        StartCoroutine(ExtraLife());
    }

    IEnumerator ExtraLife()
    {
        // 2500 coins cost
        dataManager.SubtractCoins(lifeValue);

        // UI
        deadMenuUIAnimator.SetTrigger("DeadMenuUI");
        extraLifeAnimator.SetTrigger("ExtraLife");      
        EnablePauseButton();
        EnableGameUI();

        // game checks
        hasGameEnded = false;
        player.isInmune = true;
        player.EnableColliders();
        player.DecreaseOrderInLayer();
        player.manualCheck = false;
        player.inputEnabled = false;

        // player position
        if(player.diedInPortal)
        {
            player.transform.position = new Vector3(player.transform.position.x, 3.95f, 0);
            player.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            player.transform.position = new Vector3(player.transform.position.x, -3.3f, 0);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // skin
        player.LoadSkin(dataManager.lastSkinUsedSaved);
        player.halo.enabled = false;

        yield return new WaitForSeconds(1.5f);
        DisableDeadMenuUI();
        shopButton.interactable = true;
        replayButton.interactable = true;
        yield return new WaitForSeconds(1.5f);

        // move
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.inputEnabled = true;
        player.velocity = player.playerLastVelocity;
        FindObjectOfType<AudioManager>().Play("Theme");
    }

    public void SaveData()
    {
        // update save files
        // refresh data stored in game variables
        dataManager.SaveCoins(ScoreManager.instance.coinCounter);
        dataManager.SaveStats(ScoreManager.instance.distance, ScoreManager.instance.coinCounter, player.tricks, player.gravitySwaps);
        dataManager.LoadGame(); 
    }

    public void ShowDataDeadUI()
    {
        highScore = dataManager.CheckForHighScoreAndSave(ScoreManager.instance.distance);
    }

    public void ReloadGame()
    {
        SaveData();

        AudioManager am = FindObjectOfType<AudioManager>();
        am.Stop("Waiting");
        am.Play("Play");

        Time.timeScale = 1;
        SceneManager.LoadScene("Game");

        // load data
        dataManager.LoadGame();

        if (!am.IsSourcePlaying("Theme"))
            am.Play("Theme");
    }

    public void ShowShop()
    {
        FindObjectOfType<AudioManager>().Play("Play");
        SaveData();
        DisableDeadMenuUI();
        EnableShopUI();
        shop.LoadShop();
        LoadStats(dataManager.LoadStats());

        cameraScript.enabled = false;
        player.ShowPlayerInShop();
        FindObjectOfType<AudioManager>().Play("Waiting");
    }

    public void UpdateDistancePosition()
    {
        bestDistanceText.transform.position = bestDistanceSprite.position;
    }

    public void UpdateInitialMessagePosition()
    {
        Vector2 offset = new Vector2(6f, -1f);
        initialMessageText.transform.position = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
    }

    public void UpdateGameLogoPosition()
    {
        Vector2 offset = new Vector2(6f, 1f);
        gameLogoText.transform.position = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
    }

    public void UpdateCreditsPosition()
    {
        Vector2 offset = new Vector2(-1.5f, 2f);
        creditsBtn.gameObject.transform.position = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
    }

    IEnumerator FlashMessage(string message)
    {
        tricksText.text = message;
        EnableTricksText();
        yield return new WaitForSeconds(1f);
        tricksText.text = "";
        DisableTricksText();
    }

    IEnumerator EnableGameUIWithDelay()
    {
        yield return new WaitForSeconds(2);
        pauseButton.gameObject.SetActive(true);
        coinsCounterText.gameObject.SetActive(true);
        distanceCounterText.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void LoadStats(List<int> stats)
    {
        statsText.text = " ----- STATS -----\n" +
            "\nDISTANCE: " + stats[0] +
            "\nCOINS: " + stats[1] +
            "\nTRICKS: " + stats[2] +
            "\nPORTALS ENTERED: " + stats[3] +
            "\nDEATHS: " + stats[4];
    }

    public void ShowCredits()
    {
        AudioManager am = FindObjectOfType<AudioManager>();
        am.Play("Play");
        am.Stop("Theme");
        player.creditsActive = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        creditsCanvas.gameObject.SetActive(true);
    }

    public void BackToGame()
    {
        ReloadGame();
    }

    // UI Enable/Disable
    public void EnablePreDeath()
    {
        preDeath.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DisablePreDeath()
    {
        preDeath.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void DisableShopUI()
    {
        shopUI.SetActive(false);
    }

    public void EnableShopUI()
    {
        shopUI.transform.gameObject.SetActive(true);
        shopUI.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DisableDeadMenuUI()
    {
        deadMenuUI.SetActive(false);
    }

    public void EnableDeadMenuUI()
    {
        // check if new record
        if (highScore)
            newRecordText.gameObject.SetActive(true);

        coinsText.text = ScoreManager.instance.coinCounter.ToString();
        distanceText.text = ScoreManager.instance.distance.ToString() + "m";

        if (extraLifeUsed)
            extraLifeUI.SetActive(false);
        else
            extraLifeUI.SetActive(true);

        if(dataManager.GetCoinsSaved() < lifeValue)
        {
            buyLifeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "NO COINS";
            buyLifeButton.GetComponent<Button>().interactable = false;
        }

        deadMenuUI.SetActive(true);
    }

    public void DisableGameUI()
    {
        gameUI.SetActive(false);
    }

    public void EnableGameUI()
    {
        gameUI.SetActive(true);
    }

    public void EnableTricksText()
    {
        tricksText.gameObject.SetActive(true);
    }

    public void DisableTricksText()
    {
        tricksText.gameObject.SetActive(false);
    }

    public void EnablePauseButton()
    {
        pauseButton.gameObject.SetActive(true);
    }

    public void DisablePauseButton()
    {
        pauseButton.gameObject.SetActive(false);
    }
}
