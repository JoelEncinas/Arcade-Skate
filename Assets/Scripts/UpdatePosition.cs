using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    private UIManager uIManager;

    private void Awake()
    {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        uIManager.UpdateDistancePosition();
        uIManager.UpdateInitialMessagePosition();
        uIManager.UpdateGameLogoPosition();
        uIManager.UpdateCreditsPosition();
    }
}
