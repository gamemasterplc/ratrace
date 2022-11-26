using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlockScript : MonoBehaviour
{
    private bool hit;

    void Start()
    {
        //Mark block as not hit
        hit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if player hit block sufficiently vertically
        if (collision.gameObject.tag == "player" && collision.contacts[0].normal.y > 0.5f) {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            if(!hit) {
                //Make block orange
                GetComponent<Renderer>().material.color = new Color(1.0f, 0.65f, 0.0f);
                if (GameManager.instance.power_level == 0) {
                    //Spawn growth powerup
                } else {
                    //Spawn fire powerup
                }
                //Mark block as hit
                hit = true;
            }
            //Do fall when player hits block
            player.StartFall();
        }
    }
}
