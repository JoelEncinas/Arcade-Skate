using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public Button buyBtn;
    public Button equipBtn;

    public void setTitle(string title)
    {
        titleText.text = title;
    }
}
