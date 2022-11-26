using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    float time_remaining;

    // Start is called before the first frame update
    void Start()
    {
        time_remaining = 200.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(time_remaining < Time.deltaTime) {
            GameManager.instance.LoseCoins();
        }
        time_remaining -= Time.deltaTime;
        GetComponent<Text>().text = "Time: " + ((int)time_remaining).ToString();
    }
}
