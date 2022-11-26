using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarterScript : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.level = 0;
        GameManager.instance.power_level = 0;
        GameManager.instance.num_coins = 6;
        SceneManager.LoadScene("intermission");
    }
}
