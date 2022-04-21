using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] public TextMeshProUGUI CoinCounterText;
    [SerializeField] private TextMeshProUGUI DistanceCounterText;

    public int distance;
    public int coinCounter;
    public bool doubleCoinsCheck = false;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void ChangeCoinScore(int coinValue)
    {
        if (doubleCoinsCheck)
            coinCounter += (coinValue * 2);
        else
            coinCounter += coinValue;

        CoinCounterText.text = coinCounter.ToString();
    }

    public void ChangeDistanceScore(int distanceTravelled)
    {
        distance = distanceTravelled;
        DistanceCounterText.text = distanceTravelled.ToString() + " m";
    }
}
