using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player" && collision.contacts[0].normal.y < -0.5f) {
            collision.gameObject.GetComponent<PlayerScript>().TakeDamage();
        }
    }
}
