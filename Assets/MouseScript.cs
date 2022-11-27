using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseScript : MonoBehaviour
{
    private Rigidbody rb;
    private float move_timer;
    private bool move_right;
    private float min_x, max_x;
    public float min_move_length = 2.0f;
    public float max_move_length = 3.0f;
    public float mouse_speed = 2.5f;

    public int max_health = 1;
    public int health = 1;

    void Start()
    {
        //Calculate level bounds
        GameObject level = GameObject.Find("level");
        Renderer[] renderers = level.GetComponentsInChildren<Renderer>();
        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) {
            bounds.Encapsulate(renderers[i].bounds);
        }
        min_x = bounds.min.x;
        max_x = bounds.max.x;
        //Initialize parameters
        rb = GetComponent<Rigidbody>();
        health = max_health;
        move_timer = (max_move_length + min_move_length) / 2.0f; //
        move_right = false;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 vel = rb.velocity;
        //Check if move in this direction has expired
        move_timer -= Time.deltaTime;
        if(move_timer < 0) {
            //Start move in opposite direction
            move_right = !move_right;
            move_timer = Random.Range(min_move_length, max_move_length);
        }
        //Set velocity depending on move direction
        if(move_right) {
            vel.x = mouse_speed;
        } else {
            vel.x = -mouse_speed;
        }
        //Stop mouse at level edges (0.5285455f is half of the collider height)
        if (pos.x < min_x+(transform.localScale.x * 0.5285455f)) {
            pos.x = min_x + (transform.localScale.x * 0.5285455f);
        }
        if (pos.x > max_x - (transform.localScale.x * 0.5285455f)) {
            pos.x = max_x - (transform.localScale.x * 0.5285455f);
        }
        transform.position = pos;
        rb.velocity = vel;
    }

    private void DoDamaage()
    {
        //Player hit top of mouse
        if (health == 1) {
            if (max_health == 3) {
                //Player beat boss mouse
                GameManager.instance.AdvanceLevel();
            }
            //Remove mouse
            Destroy(gameObject);
        } else {
            //Decrease mouse health
            health--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player") {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            if (collision.contacts[0].normal.y < -0.5f) {
                DoDamaage();
                //Player will fall after hitting head
                player.StartFall();
            } else {
                //Do damage since player did not hit head
                player.TakeDamage();
            }
        }
        if(collision.gameObject.tag == "fireball") {
            DoDamaage();
            Destroy(collision.gameObject);
        }
    }
}
