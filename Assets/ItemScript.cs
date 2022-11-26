using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public bool is_fireball_powerup;
    public float item_speed = 5.0f;

    private Rigidbody rb;
    private bool move_right;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        move_right = true;
    }

    void Update()
    {
        Vector3 vel = rb.velocity;
        if (!is_fireball_powerup) {
            if (move_right) {
                vel.x = item_speed;
            } else {
                vel.x = -item_speed;
            }
        } else {
            vel.x = 0;
        }
        rb.velocity = vel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall") {
            Vector3 new_pos = collision.contacts[0].point;
            Vector3 offset = collision.contacts[0].normal * 0.5f;
            move_right = !move_right;
            transform.position = new_pos-offset;
        }
        if(collision.gameObject.tag == "player") {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            if(is_fireball_powerup) {
                player.ApplyPowerLevel(2);
            } else {
                player.ApplyPowerLevel(1);
            }
            Destroy(gameObject);
        }
    }
}
