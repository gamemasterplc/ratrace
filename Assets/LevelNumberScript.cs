using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumberScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Level " + (GameManager.instance.level + 1).ToString();
    }
}
