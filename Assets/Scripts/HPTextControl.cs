using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPTextControl : MonoBehaviour
{
    public TMP_Text healthText;
    public PlayerMotor player;

    // Update is called once per frame
    void Update()
    {
        if(player.health >= 0)
            healthText.SetText(player.health.ToString() + "/" + player.maxHealth.ToString());
        else
            healthText.SetText("0/" + player.maxHealth.ToString());
    }
}
