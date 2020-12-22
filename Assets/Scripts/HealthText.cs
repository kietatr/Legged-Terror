using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public Player player;
    public Text healthText;

    void Update()
    {
        healthText.text = player.health.ToString();
    }
}
