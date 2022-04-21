using System.Collections.Generic;
using UnityEngine;

public class GrindCheck : MonoBehaviour
{
    private UIManager uIManager;
    private Player player;
    private bool grind = false;
    private List<float> timestamps;
    private List<bool> groundChecks;

    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player>();

        timestamps = new List<float>();
        groundChecks = new List<bool>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timestamps.Add(Time.time);
        if (player.transform.position.y >= -2.4f && player.IsGrounded())
            groundChecks.Add(true);
        else
            groundChecks.Add(false);
    }

    private void Update()
    {
        if(timestamps.Count >= 3 && groundChecks.Count >= 3)
        {
            if ((timestamps[2] - timestamps[0]) >= 0.75f && groundChecks[1] && groundChecks[2])
            {
                if(!grind)
                {
                    uIManager.grindCheck = !uIManager.grindCheck;
                    grind = !grind;
                }
            }
        }
    }

}
