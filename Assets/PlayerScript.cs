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
    private float min_x, max_x;
    private bool stopped;
    private float invulnerable_timer;
    private int power_level;

    // Start is called before the first frame update
    void Start()
    {
        GameObject level = GameObject.Find("level");
        Renderer[] renderers = level.GetComponentsInChildren<Renderer>();
        Bounds bounds = renderers[0].bounds;
        for(int i=1; i<renderers.Length; i++) {
            bounds.Encapsulate(renderers[i].bounds);
        }
        min_x = bounds.min.x;
        max_x = bounds.max.x;
        rb = GetComponent<Rigidbody>();
        invulnerable_timer = 0;
        power_level = 0;
        stopped = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        Vector3 pos = transform.position;
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
        if(pos.x < min_x + 0.5f) {
            pos.x = min_x + 0.5f;
            vel.x = 0;
        }
        if (pos.x > max_x - 0.5f) {
            pos.x = max_x - 0.5f;
            vel.x = 0;
        }
        //Delete player if they go below camera
        if(pos.y < -9.0f) {
            KillPlayer();
        }
        if(invulnerable_timer > 0) {
            invulnerable_timer -= 0.02f;
            if(GetComponent<Renderer>().enabled) {
                GetComponent<Renderer>().enabled = false;
            } else {
                GetComponent<Renderer>().enabled = true;
            }
        }
        if(invulnerable_timer <= 0) {
            GetComponent<Renderer>().enabled = true;
        }
        transform.position = pos;
        rb.velocity = vel;
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 pos = Camera.main.transform.position;
        float cam_width = Camera.main.orthographicSize * Camera.main.aspect;
        //Make camera follow player
        pos.x = transform.position.x;
        //Do camera clamping
        if (pos.x < min_x + cam_width) {
            pos.x = min_x + cam_width;
        }
        if (pos.x > max_x - cam_width) {
            pos.x = max_x - cam_width;
        }
        Camera.main.transform.position = pos;
    }

    private void StartFall()
    {
        Vector3 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
    }

    private void DamagePlayer()
    {
        if(invulnerable_timer > 0) {
            return;
        }
        if(power_level == 0) {
            Destroy(gameObject);
        } else {
            power_level--;
            invulnerable_timer = 0.5f;
        }
    }

    private void KillPlayer()
    {
        Destroy(gameObject);
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
                DamagePlayer();
            }
        }
        if(collision.gameObject.tag == "spike")
        {
            if(collision.contacts[0].normal.y > 0.01f)
            {
                DamagePlayer();
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
