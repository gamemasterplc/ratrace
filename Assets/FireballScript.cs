using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float max_velocity = 4.5f;

    private int bounce_count;
    private Rigidbody rb;

    void Awake()
    {
        //Setup velocity
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(max_velocity, 0, 0);
    }

    void Start()
    {
        bounce_count = 5;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Reflect fireball velocity around Y axis
        Vector3 new_vel = collision.relativeVelocity;
        new_vel.x = -new_vel.x;
        rb.velocity = new_vel;
        //Destroy when ball runs out of bounces
        bounce_count--;
        if(bounce_count == 0) {
            Destroy(gameObject);
        }
    }
}
