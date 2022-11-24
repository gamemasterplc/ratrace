using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    private Rigidbody rb;
    private float move_timer;
    private bool move_right;
    public float move_length = 2.0f;
    public float mouse_speed = 2.5f;

    public int max_health = 1;
    public int health = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = max_health;
        move_timer = move_length/2;
        move_right = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = rb.velocity;
        move_timer -= Time.deltaTime;
        if(move_timer < 0)
        {
            move_right = !move_right;
            move_timer = move_length;
        }
        if(move_right) {
            vel.x = mouse_speed;
        } else {
            vel.x = -mouse_speed;
        }
        rb.velocity = vel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player") {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            //Do not detect collisions that are highly horizontal
            if (collision.contacts[0].normal.y < -0.25f) {
                if (health == 1) {
                    if(max_health == 3) {
                        //Do level winning here
                        Destroy(gameObject);
                    } else {
                        Destroy(gameObject);
                    }
                } else {
                    health--;
                }
                player.StartFall();
            } else {
                player.TakeDamage();
            }
        }
    }
}
