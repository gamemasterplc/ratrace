using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [System.NonSerialized] public int level;
    [System.NonSerialized] public int power_level;
    [System.NonSerialized] public int num_coins;

    public void Awake()
    {
        if(instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void StartLevel()
    {
        switch(level) {
            case 0:
                SceneManager.LoadScene("level_1");
                break;

            case 1:
                SceneManager.LoadScene("level_2");
                break;
        }
    }

    public void AdvanceLevel()
    {
        GameManager.instance.level++;
        SceneManager.LoadScene("intermission");
    }

    public void LoseCoins()
    {
        GameManager.instance.num_coins -= 2;
        if (GameManager.instance.num_coins < 0) {
            SceneManager.LoadScene("game_over");
        } else {
            SceneManager.LoadScene("intermission");
        }
    }
}
