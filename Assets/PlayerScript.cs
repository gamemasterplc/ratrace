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
    private Vector3 vel;
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
        if(Input.GetKeyDown(KeyCode.UpArrow))
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
        pos.y = transform.position.y;
        Camera.main.transform.position = pos;
    }
}
