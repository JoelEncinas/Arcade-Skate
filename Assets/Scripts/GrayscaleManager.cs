using UnityEngine;
using UnityEngine.UI;

public class GrayscaleManager : MonoBehaviour
{
    [SerializeField] private DataManagerScript dataManagerScript;
    [SerializeField] private GameObject stripes;

    // Buttons
    public Button colorBtn;
    public Button noColorBtn;

    // start so game loads or btns won't update
    private void Start()
    {
        // load data
        if (dataManagerScript.graphicModeSaved == 1)
        {
            GrayscaleSettings();
            DisableNoColorButton();
        }
        if (dataManagerScript.graphicModeSaved == 0)
        {
            ColorSettings();
            DisableColorButton();
        }
    }

    // called on grayscale btn
    public void EnableGrayscale()
    {
        FindObjectOfType<AudioManager>().Play("ColorChange");
        GrayscaleSettings();
        DisableNoColorButton();
        EnableColorButton();
        dataManagerScript.SaveGraphicsMode(1);
    }

    // called on color btn
    public void DisableGrayscale()
    {
        FindObjectOfType<AudioManager>().Play("ColorChange");
        ColorSettings();
        DisableColorButton();
        EnableNoColorButton();
        dataManagerScript.SaveGraphicsMode(0);
    }

    public void GrayscaleSettings()
    {
        stripes.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void ColorSettings()
    {
        stripes.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void EnableColorButton()
    {
        colorBtn.gameObject.SetActive(true);
    }

    public void DisableColorButton()
    {
        colorBtn.gameObject.SetActive(false);
    }

    public void EnableNoColorButton()
    {
        noColorBtn.gameObject.SetActive(true);
    }

    public void DisableNoColorButton()
    {
        noColorBtn.gameObject.SetActive(false);
    }
}
