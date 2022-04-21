using System.Collections.Generic;
using UnityEngine;

public class TrickMessages : MonoBehaviour
{
    private List<string> jumpMessages;
    private List<string> grindMessages;
    private List<string> grindManualMessages;
    private List<string> streetlightMessages;

    private string lineJump = "\n+ ";
    private int trickValue;

    UIManager uIManager;
    Player player;

    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player>();

        // --- TRICK MESSAGES DB ---
        jumpMessages = new List<string>() { "YEAH!", "BIG JUMP", "COOL" };
        grindMessages = new List<string>() { "CLEAN 50-50", "NASTY 50-50" };
        grindManualMessages = new List<string>() { "TOO HOOT!", "STEEZY" };
        streetlightMessages = new List<string>() { "TING!", "CLINK" };
    }

    public string GetRandomJumpMessage()
    {
        player.IncreaseTricks();
        FindObjectOfType<AudioManager>().Play("Trick");
        trickValue = player.manualCheck ? uIManager.regularTrick * 2 : uIManager.regularTrick;
        return jumpMessages[Random.Range(0, jumpMessages.Count)] + lineJump + trickValue; 
    }

    public string GetRandomGrindMessage()
    {
        player.IncreaseTricks();
        FindObjectOfType<AudioManager>().Play("Trick");
        trickValue = player.manualCheck ? uIManager.grindTrick * 2 : uIManager.grindTrick;
        return grindMessages[Random.Range(0, grindMessages.Count)] + lineJump + trickValue;
    }

    public string GetRanndomGrindManualMessage()
    {
        player.IncreaseTricks();
        FindObjectOfType<AudioManager>().Play("Trick");
        trickValue = player.manualCheck ? uIManager.grindTrick * 2 : uIManager.grindTrick;
        return grindManualMessages[Random.Range(0, grindManualMessages.Count)] + lineJump + trickValue;
    }

    public string GetRandomStreetlightMessage()
    {
        player.IncreaseTricks();
        FindObjectOfType<AudioManager>().Play("Trick");
        trickValue = player.manualCheck ? uIManager.regularTrick * 2 : uIManager.regularTrick;
        return streetlightMessages[Random.Range(0, streetlightMessages.Count)] + lineJump +trickValue;
    }
}
