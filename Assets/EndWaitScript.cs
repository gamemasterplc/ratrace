using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndWaitScript : MonoBehaviour
{
    float time;

    void Start()
    {
        //Set wait timer
        time = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Start title screen after wait time expires
        time -= Time.deltaTime;
        if(time < 0) {
            SceneManager.LoadScene("title");
        }
    }
}
