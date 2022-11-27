using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    void Start()
    {
        //Move cheese at 9 units/second
        GetComponent<Rigidbody>().velocity = new Vector3(-9.0f, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "player") {
            //Take damage if player hits cheese
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            player.TakeDamage();
        }
        Destroy(gameObject);
    }
}
