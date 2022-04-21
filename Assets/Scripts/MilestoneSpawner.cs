using UnityEngine;

public class MilestoneSpawner : MonoBehaviour
{
    private DataManagerScript dataManagerScript;
    private Player player;
    private Vector2 milestone;
    private GameObject milestoneMarker;
    private float minimumDistance = 100f;
    private float offset = 30f;

    // awake bugs the data load
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player>();
        dataManagerScript = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManagerScript>();
        milestoneMarker = Resources.Load("MilestoneNPC") as GameObject;
        milestone = new Vector2(dataManagerScript.GetDistanceSaved(), -3.54f);
    }

    private void LateUpdate()
    {
        if(dataManagerScript.GetDistanceSaved() >= minimumDistance)
            if(player.GetPosition().x >= dataManagerScript.GetDistanceSaved() - offset)
            {
                GameObject.Instantiate(milestoneMarker, milestone, Quaternion.identity);
                Destroy(this.gameObject);
            }
    }
}
