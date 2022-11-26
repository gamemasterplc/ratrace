using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseScript : MonoBehaviour
{
    private Rigidbody rb;
    private float move_timer;
    private bool move_right;
    public float move_length = 2.0f;
    public float mouse_speed = 2.5f;

    public int max_health = 1;
    public int health = 1;

    void Start()
    {
        //Initialize parameters
        rb = GetComponent<Rigidbody>();
        health = max_health;
        move_timer = move_length/2;
        move_right = false;
    }

    void Update()
    {
        Vector3 vel = rb.velocity;
        //Check if move in this direction has expired
        move_timer -= Time.deltaTime;
        if(move_timer < 0) {
            //Start move in opposite direction
            move_right = !move_right;
            move_timer = move_length;
        }
        //Set velocity depending on move direction
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
            if (collision.contacts[0].normal.y < -0.5f) {
                //Player hit top of mouse
                if (health == 1) {
                    if(max_health == 3) {
                        //Player beat boss mouse
                        GameManager.instance.AdvanceLevel();
                    }
                    //Remove mouse
                    Destroy(gameObject);
                } else {
                    //Decrease mouse health
                    health--;
                }
                //Player will fall after hitting head
                player.StartFall();
            } else {
                //Do damage since player did not hit head
                player.TakeDamage();
            }
        }
    }
}
