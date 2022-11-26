using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumberScript : MonoBehaviour
{
    void Update()
    {
        //Change label to relevant level
        GetComponent<Text>().text = "Level " + (GameManager.instance.level + 1).ToString();
    }
}
