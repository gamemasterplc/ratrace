using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    private Rigidbody rb;
    private float move_timer;
    private bool move_right;
    float move_length = 2.0f;
    float mouse_speed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
}
