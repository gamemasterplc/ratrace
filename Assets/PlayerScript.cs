using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Inspector-Set Value:")]
    public GameObject fireball_object;
    public float max_move_speed = 2.5f;
    public float jump_speed = 7.0f;
    public float move_accel = 0.2f;
    public float move_decel = 0.4f;
    

    private Rigidbody rb;
    private Color orig_mat_color;
    private float min_x, max_x;
    private float fireball_dir;
    private bool stopped;
    private float invulnerable_timer;

    void Start()
    {
        //Calculate level bounds
        GameObject level = GameObject.Find("level");
        Renderer[] renderers = level.GetComponentsInChildren<Renderer>();
        Bounds bounds = renderers[0].bounds;
        for(int i=1; i<renderers.Length; i++) {
            bounds.Encapsulate(renderers[i].bounds);
        }
        min_x = bounds.min.x;
        max_x = bounds.max.x;
        //Grab rigidbody and original color
        rb = GetComponent<Rigidbody>();
        orig_mat_color = GetComponent<Renderer>().material.color;
        invulnerable_timer = 0; //Player starts out not invulnerable
        fireball_dir = 1; //Set fireballs to spawn to right
        ApplyPowerLevel(GameManager.instance.power_level); //Apply saved power level
        stopped = true; //Player starts stopped
    }

    void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        Vector3 pos = transform.position;
        bool is_left = Input.GetKey(KeyCode.LeftArrow);
        bool is_right = Input.GetKey(KeyCode.RightArrow);
        if(!is_left && !is_right) {
            if(stopped) {
                //Zero out velocity after player stops 
                vel.x = 0;
            } else  {
                //Apply deceleration
                if (vel.x < 0) {
                    vel.x += move_decel;
                    if (vel.x >= 0) {
                        //Player has stopped 
                        vel.x = 0;
                        stopped = true;
                    }
                } else {
                    //Deceleration must be flipped if player is moving right
                    vel.x -= move_decel;
                    if (vel.x <= 0) {
                        //Player has stopped 
                        vel.x = 0;
                        stopped = true;
                    }
                }
            }
        } else {
            //Do acceleration
            if(is_left) {
                fireball_dir = -1;
                vel.x -= move_accel;
                if(vel.x < -max_move_speed) {
                    vel.x = -max_move_speed;
                }
            } else {
                fireball_dir = 1;
                vel.x += move_accel;
                if (vel.x > max_move_speed)  {
                    vel.x = max_move_speed;
                }
            }
        }
        //Check if player is near ground
        if(Physics.Raycast(transform.position, Vector3.down, transform.localScale.y*1.01f) && Input.GetKeyDown(KeyCode.UpArrow)) {
            //Do jump
            vel.y = jump_speed;
        }
        //Make fireball when pressing space at power level 2
        if (GameManager.instance.power_level == 2 && Input.GetKeyDown(KeyCode.Space)) {
            //Create fireball
            GameObject new_object = Instantiate<GameObject>(fireball_object);
            //Setup fireball position
            Vector3 obj_pos = transform.position;
            obj_pos.x += 1.0f * fireball_dir;
            //Setup fireball movement direction
            new_object.GetComponent<FireballScript>().max_velocity *= fireball_dir;
            new_object.transform.position = obj_pos;
        }
        //Bound player to inside of levels
        if (pos.x < min_x + 0.5f) {
            pos.x = min_x + 0.5f;
            vel.x = 0;
        }
        if (pos.x > max_x - 0.5f) {
            pos.x = max_x - 0.5f;
            vel.x = 0;
        }
        //Delete player if they go below camera
        if(pos.y < -9.0f) {
            Kill();
        }
        //Do invulnerability update
        if(invulnerable_timer > 0) {
            invulnerable_timer -= 0.02f;
            //Do invinicibility flashing
            if(GetComponent<Renderer>().enabled) {
                GetComponent<Renderer>().enabled = false;
            } else {
                GetComponent<Renderer>().enabled = true;
            }
        }
        //Reset mercy invinicibility
        if (invulnerable_timer <= 0) {
            GetComponent<Renderer>().enabled = true;
        }
        //Update components
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
        //Update camera position
        Camera.main.transform.position = pos;
    }

    public void StartFall()
    {
        //Zero out y velocity
        Vector3 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
    }

    public void ApplyPowerLevel(int level)
    {
        //Update local and global power level
        GameManager.instance.power_level = level;
        switch(level) {
            case 0:
                //Make player small and original color
                GetComponent<Renderer>().material.color = orig_mat_color;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 1:
                //Make player big and original color
                GetComponent<Renderer>().material.color = orig_mat_color;
                transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                break;

            case 2:
                //Make player big and red
                GetComponent<Renderer>().material.color = Color.red;
                transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                break;
        }
    }

    public void TakeDamage()
    {
        //Do not apply damage when player has invulnerability left
        if(invulnerable_timer > 0) {
            return;
        }
        if(GameManager.instance.power_level == 0) {
            //Kill player when small and taking damager
            Kill();
        } else {
            //Apply damage
            ApplyPowerLevel(0);
            invulnerable_timer = 0.5f;
        }
    }

    private void Kill()
    {
        //End level
        GameManager.instance.LoseCoins();
        Destroy(gameObject);
    }
}
