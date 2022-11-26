using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndWaitScript : MonoBehaviour
{
    float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0) {
            SceneManager.LoadScene("title");
        }
    }
}
