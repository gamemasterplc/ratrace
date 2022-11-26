using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterScript : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = "Coins: " + GameManager.instance.num_coins.ToString();
    }
}
