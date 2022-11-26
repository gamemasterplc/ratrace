using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlockScript : MonoBehaviour
{
    [Header("Inspector-Set Value:")]
    public GameObject growth_powerup;
    public GameObject fire_powerup;

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
                //Mark block as hit
                hit = true;
                //Set item position to top of block
                Vector3 item_pos = transform.position;
                item_pos.y -= 0.75f;
                //Spawn item
                GameObject temp;
                if (GameManager.instance.power_level == 0) {
                    //Spawn growth powerup
                    temp = Instantiate<GameObject>(growth_powerup);
                } else {
                    //Spawn fire powerup
                    temp = Instantiate<GameObject>(fire_powerup);
                }
                temp.transform.position = item_pos;
            }
            //Do fall when player hits block
            player.StartFall();
        }
    }
}
