using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermissionWaitScript : MonoBehaviour
{
    float time;

    void Start()
    {
        time = 1.5f;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0) {
            GameManager.instance.StartLevel();

        }
    }
}
