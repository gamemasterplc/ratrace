using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float max_velocity = 4.5f;

    private float timer;
    private Rigidbody rb;

    void Awake()
    {
        //Setup velocity
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(max_velocity, 0, 0);
    }

    void Start()
    {
        //Setup timer for destroying fireball
        timer = 5.0f;
    }

    private void Update()
    {
        //Update timer
        timer -= Time.deltaTime;
        if(timer < 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Reflect fireball velocity around Y axis
        Vector3 new_vel = collision.relativeVelocity;
        new_vel.x = -new_vel.x;
        rb.velocity = new_vel;
    }
}
