using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    float time_remaining;

    void Start()
    {
        //Reset timer
        time_remaining = 200.0f;
    }

    void Update()
    {
        //Player dies if time expires
        if(time_remaining < Time.deltaTime) {
            GameManager.instance.LoseCoins();
        }
        //Update time and timer label
        time_remaining -= Time.deltaTime;
        GetComponent<Text>().text = "Time: " + ((int)time_remaining).ToString();
    }
}
