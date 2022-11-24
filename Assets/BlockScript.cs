using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public bool is_coin_block = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "player" && collision.contacts[0].normal.y > 0.5f) {
            if(is_coin_block) {
                //Add coin to coin counter
            }
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerScript>().StartFall();
        }
    }
}
