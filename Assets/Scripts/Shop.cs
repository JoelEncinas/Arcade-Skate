using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private int coinsBalance;
    [SerializeField] private TextMeshProUGUI coinsBalanceText;

    [SerializeField] private DataManagerScript dataManagerScript;
    [SerializeField] private Player player;

    // shop items ui
    public GameObject shopItem;
    [SerializeField] private List<GameObject> shopItemsList;
    [SerializeField] private GameObject itemsContainer;
    [SerializeField] private GameObject itemMessageBG;

    // items db
    public Item item;
    [SerializeField] private List<Item> itemDB;
    [SerializeField] private List<Sprite> skinsList;

    // shop initialization
    public void LoadShop()
    {
        // load coins into the shop before creating items
        ShowCoinBalance();

        GenerateItemDB();
        GenerateShopItems();
    }

    public void GenerateShopItems()
    {
        int i = 0;
        Button button;

        foreach (Item item in itemDB)
        {
            // --- SHOP ITEM STRUCTURE --- 
            // Item 
            //     - Title
            //     - Description
            //     - Price
            //     - Buy
            //     - Equip

            shopItemsList.Add(Instantiate(shopItem, itemsContainer.transform));
            itemsContainer.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Title;
            itemsContainer.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Description;
            itemsContainer.transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Price.ToString();
            itemsContainer.transform.GetChild(i).GetChild(4).GetComponent<Button>().onClick.AddListener(() => LoadSkinAndSave(item.Title));
            itemsContainer.transform.GetChild(0).GetChild(5).gameObject.SetActive(false); // default skin 
            itemsContainer.transform.GetChild(i).GetChild(6).GetChild(0).GetComponent<Image>().sprite = skinsList[i];

            if (dataManagerScript.IsSkinOwned(i) || item.Price == 0)
            {
                itemsContainer.transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
                itemsContainer.transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
                itemsContainer.transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                itemsContainer.transform.GetChild(i).GetChild(5).gameObject.SetActive(false); 
            }
            else
            {
                button = itemsContainer.transform.GetChild(i).GetChild(3).gameObject.GetComponent<Button>();
                button.onClick.AddListener(() => BuyItem(item.Price, item.Title));
            }

            i++;
        }
    }

    public void GenerateItemDB()
    {
        // --- item DB ---
        itemDB = new List<Item>();
        itemDB.Add(new Item(0, "Classic Hummin", "Original and the best, a true classic.", 0));
        itemDB.Add(new Item(1, "Zombie", "The zombie apocalypse is finally here!", 999));
        itemDB.Add(new Item(2, "Tony Hawk", "The legend himself! The skater of the people.", 2000));
        itemDB.Add(new Item(3, "Bart", "El Barto was here.", 4500));
        itemDB.Add(new Item(4, "Real G", "Get out there and show the streets what you are made of!", 5000));
        itemDB.Add(new Item(5, "Ghost", "What's on the other side of life? Find out with this spooky look.", 7500));
        itemDB.Add(new Item(6, "Negative Hummin", "In light there is dark and in dark there is light.", 10000));
        itemDB.Add(new Item(7, "Golden Hummin", "Is it gold in here? Ore is it just me?", 99999));
    }

    public void BuyItem(int price, string title)
    {
        if(price <= coinsBalance)
        {
            FindObjectOfType<AudioManager>().Play("BuyItem");
            IDisplayMessage("Item bought successfully!");

            dataManagerScript.SubtractCoins(price);
            dataManagerScript.LoadGame(); // refresh cache memory

            Transform itemTransform = itemsContainer.transform.GetChild(SearchItem(title));
            itemTransform.GetChild(2).gameObject.SetActive(false);
            itemTransform.GetChild(3).gameObject.SetActive(false);
            itemTransform.GetChild(4).gameObject.SetActive(true);
            itemTransform.GetChild(5).gameObject.SetActive(false);

            dataManagerScript.SaveSkin(SearchItem(title));
        }
        else
        {
            // display message
            FindObjectOfType<AudioManager>().Play("NoCoins");
            IDisplayMessage("Not enough coins!");
        }

        ShowCoinBalance();
    }

    public void IDisplayMessage(string message)
    {
        StartCoroutine(DisplayMessage(message));
    }

    IEnumerator DisplayMessage(string message)
    {
        itemMessageBG.SetActive(true); 
        // first child is ui blocker
        itemMessageBG.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = message;
        yield return new WaitForSeconds(1f);
        itemMessageBG.SetActive(false);
    }

    public int SearchItem(string title)
    {
        foreach (Item item in itemDB)
        {
            if (item.Title.Contains(title))
                return item.ID;
        }

        return -1;
    }

    public void ShowCoinBalance()
    {
        coinsBalance = dataManagerScript.GetCoinsSaved();
        coinsBalanceText.text = coinsBalance.ToString();
    }

    public void LoadSkinAndSave(string skinName)
    {
        FindObjectOfType<AudioManager>().Play("Play");
        player.LoadSkin(SearchItem(skinName));
        dataManagerScript.SaveLastSkinUsed(SearchItem(skinName));
    }
}
