using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; } //Actual singleton instance
    //Variables
    [System.NonSerialized] public int level;
    [System.NonSerialized] public int power_level;
    [System.NonSerialized] public int num_coins;

    public void Awake()
    {
        if(instance != null && instance != this) {
            //Destroy other rogue instances
            Destroy(this);
        } else {
            //Setup singleton
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void StartLevel()
    {
        //Load correct scene based on current level
        switch(level) {
            case 0:
                SceneManager.LoadScene("level_1");
                break;

            case 1:
                SceneManager.LoadScene("level_2");
                break;

            case 2:
                SceneManager.LoadScene("level_3");
                break;
        }
    }

    public void AdvanceLevel()
    {
        //Load intermission with next level
        GameManager.instance.level++;
        SceneManager.LoadScene("intermission");
    }

    public void LoseCoins()
    {
        //Get rid of some coins
        GameManager.instance.num_coins -= 2;
        if (GameManager.instance.num_coins < 0) {
            //Load game over screen if player has run out of coins
            SceneManager.LoadScene("game_over");
        } else {
            //Restart level through intermission
            SceneManager.LoadScene("intermission");
        }
    }
}
