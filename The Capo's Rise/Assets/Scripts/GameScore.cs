using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI textLife;


    // Update is called once per frame
    void Update()
    {
        textLife.text = player.life.ToString();
    }
}
