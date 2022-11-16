using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float max_move_speed = 2.5f;
    public float jump_speed = 7.0f;
    public float move_accel = 0.2f;
    public float move_decel = 0.4f;
    

    private Rigidbody rb;
    private bool stopped;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stopped = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        bool is_left = Input.GetKey(KeyCode.LeftArrow);
        bool is_right = Input.GetKey(KeyCode.RightArrow);
        if(!is_left && !is_right) {
            if(stopped) {
                vel.x = 0;
            } else  {
                if (vel.x < 0) {
                    vel.x += move_decel;
                    if (vel.x >= 0) {
                        vel.x = 0;
                        stopped = true;
                    }
                }
                else
                {
                    vel.x -= move_decel;
                    if (vel.x <= 0) {
                        vel.x = 0;
                        stopped = true;
                    }
                }
            }
        } else {
            if(is_left) {
                vel.x -= move_accel;
                if(vel.x < -max_move_speed) {
                    vel.x = -max_move_speed;
                }
            } else {
                vel.x += move_accel;
                if (vel.x > max_move_speed)  {
                    vel.x = max_move_speed;
                }
            }
        }
        if(Physics.Raycast(transform.position, Vector3.down, 1.01f) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            vel.y = jump_speed;
        }
        rb.velocity = vel;
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 pos = Camera.main.transform.position;
        pos.x = transform.position.x;
        Camera.main.transform.position = pos;
    }

    private void StartFall()
    {
        Vector3 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            //Do not detect collisions that are highly horizontal
            if(collision.contacts[0].normal.y > 0.25f) {
                //Destroy enemy
                Destroy(collision.gameObject);
                //Start falling
                StartFall();
            } else {
                //Destroy player
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.tag == "spike")
        {
            if(collision.contacts[0].normal.y > 0.01f)
            {
                //Destroy player
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.tag == "block")
        {
            //Detect highly downward collisions
            if(collision.contacts[0].normal.y < -0.8f)
            {
                //Destroy brick
                Destroy(collision.gameObject);
                //Start falling
                StartFall();
            }
        }
        if(collision.gameObject.tag == "item_block")
        {
            //Detect highly downward collisions
            if (collision.contacts[0].normal.y < -0.8f)
            {
                //Make block orange
                Renderer renderer_temp = collision.gameObject.GetComponent<Renderer>();
                Color orange = new Color(1f, 0.65f, 0);
                if(renderer_temp.material.color != orange)
                {
                    renderer_temp.material.color = orange;
                }
                //Start falling
                StartFall();
            }
        }
    }
}
