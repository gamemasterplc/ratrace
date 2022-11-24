using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlockScript : MonoBehaviour
{
    private bool hit;

    void Start()
    {
        hit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player" && !hit && collision.contacts[0].normal.y > 0.5f) {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            GetComponent<Renderer>().material.color = new Color(1.0f, 0.65f, 0.0f);
            if (player.GetPowerLevel() == 0) {
                //Spawn growth powerup
            } else {
                //Spawn fire powerup
            }
            player.StartFall();
            hit = true;
        }
    }
}
