using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermissionWaitScript : MonoBehaviour
{
    float time;

    void Start()
    {
        //Set wait timer
        time = 1.5f;
    }

    void Update()
    {
        //Start level after wait time expires
        time -= Time.deltaTime;
        if(time < 0) {
            GameManager.instance.StartLevel();
        }
    }
}
