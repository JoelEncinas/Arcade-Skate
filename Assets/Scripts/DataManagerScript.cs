using System.Collections.Generic;
using UnityEngine;

public class DataManagerScript : MonoBehaviour
{
	// --- Cache memory data ---
	public int coinsSaved;
	public int distanceSaved;
	public List<int> skinsSaved;
	public int lastSkinUsedSaved;
	public int graphicModeSaved; // no color [1] - color [0] as default save is 0

	// stats
	public int totalDistanceSaved;
	public int totalCoinsSaved;
	public int totalTricksSaved;
	public int totalGravitySwapsSaved;
	public int totalDeathsSaved;

	private int noOfSkins = 8;

	// first time
	public int tutorial;

	private void Awake()
    {
		// --- Cheats ---
		//ResetData();
		//SaveCoins(99999);

		skinsSaved = GetSkinsSaved();

		LoadGame();
	}

	public void SaveStats(int distance, int coins, int tricks, int gravitySwaps)
    {
		PlayerPrefs.SetInt("TotalDistanceSaved", totalDistanceSaved + distance);
		PlayerPrefs.SetInt("TotalCoinsSaved", totalCoinsSaved + coins);
		PlayerPrefs.SetInt("TotalTricksSaved", totalTricksSaved + tricks);
		PlayerPrefs.SetInt("TotalGravitySwapsSaved", totalGravitySwapsSaved + gravitySwaps);
		PlayerPrefs.SetInt("TotalDeathsSaved", totalDeathsSaved + 1);
		PlayerPrefs.Save();
	}

    public void SaveCoins(int coins)
	{
		coinsSaved += coins;

		PlayerPrefs.SetInt("CoinsSaved", coinsSaved);
		PlayerPrefs.Save();
	}

	public void SubtractCoins(int coins)
    {
		coinsSaved -= coins;
		PlayerPrefs.SetInt("CoinsSaved", coinsSaved);
		PlayerPrefs.Save();
	}

	// 0 - skin not adquired
	// 1 - skin adquired
	public void SaveSkin(int skinID)
    {
		PlayerPrefs.SetInt("SkinSaved" + skinID, 1);
		LoadGame(); // reload cache
    }

	public bool CheckForHighScoreAndSave(int distance)
    {
		if (PlayerPrefs.HasKey("SavedDistance"))
		{
			distanceSaved = PlayerPrefs.GetInt("DistanceSaved");
		}

		if(distance > distanceSaved)
        {
			PlayerPrefs.SetInt("DistanceSaved", distance);
			PlayerPrefs.Save();
			return true;
        }

		return false;
	}

	public void LoadGame()
	{
		if (PlayerPrefs.HasKey("CoinsSaved"))
		{
			coinsSaved = PlayerPrefs.GetInt("CoinsSaved");
			distanceSaved = PlayerPrefs.GetInt("DistanceSaved");

			for (int i = 0; i < noOfSkins; i++)
			{
				skinsSaved.Add(PlayerPrefs.GetInt("SkinSaved" + i.ToString()));
			}

			lastSkinUsedSaved = PlayerPrefs.GetInt("LastSkinUsedSaved");
			graphicModeSaved = PlayerPrefs.GetInt("GraphicMode");

			// stats
			totalDistanceSaved = PlayerPrefs.GetInt("TotalDistanceSaved");
			totalCoinsSaved = PlayerPrefs.GetInt("TotalCoinsSaved");
			totalTricksSaved = PlayerPrefs.GetInt("TotalTricksSaved");
			totalGravitySwapsSaved = PlayerPrefs.GetInt("TotalGravitySwapsSaved");
			totalDeathsSaved = PlayerPrefs.GetInt("TotalDeathsSaved");
		}
	}

	// test
	public void ResetData()
	{
		PlayerPrefs.DeleteAll();
	}

	public int GetDistanceSaved()
    {
		return distanceSaved;
    }

	public int GetCoinsSaved()
	{
		return coinsSaved;
	}

	public bool IsSkinOwned(int skinID)
    {
		if(PlayerPrefs.GetInt("SkinSaved" + skinID.ToString()) == 1)
			return true;
        else
        {
			return false;
        }
	}

	private List<int> GetSkinsSaved()
    {
		List<int> skins = new List<int>();
		
		for(int i = 0; i < noOfSkins; i++)
        {
			skins.Add(PlayerPrefs.GetInt("SkinSaved" + i.ToString()));
		}

		return skins;
    }

	public void SaveLastSkinUsed(int skinID)
    {
		PlayerPrefs.SetInt("LastSkinUsedSaved", skinID);
		PlayerPrefs.Save();
	}

	public void SaveGraphicsMode(int graphicMode)
    {
		PlayerPrefs.SetInt("GraphicMode", graphicMode);
		PlayerPrefs.Save();
    }

	public void LoadGraphicsMode()
    {
		graphicModeSaved = PlayerPrefs.GetInt("GraphicMode");
	}

	public List<int> LoadStats()
    {
		// stats
		totalDistanceSaved = PlayerPrefs.GetInt("TotalDistanceSaved");
		totalCoinsSaved = PlayerPrefs.GetInt("TotalCoinsSaved");
		totalTricksSaved = PlayerPrefs.GetInt("TotalTricksSaved");
		totalGravitySwapsSaved = PlayerPrefs.GetInt("TotalGravitySwapsSaved");
		totalDeathsSaved = PlayerPrefs.GetInt("TotalDeathsSaved");

		return new List<int>
		{
			totalDistanceSaved,
			totalCoinsSaved,
			totalTricksSaved,
			totalGravitySwapsSaved,
			totalDeathsSaved
		};
    }

	public void SaveTutorial()
	{
		PlayerPrefs.SetInt("Tutorial", 1);
		PlayerPrefs.Save();
	}

	public int LoadTutorial()
    {
		return PlayerPrefs.GetInt("Tutorial");
	}
}
