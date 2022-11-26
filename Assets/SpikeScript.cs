using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player") {
            float hit_angle = (Mathf.Rad2Deg * Mathf.Atan2(collision.contacts[0].normal.y, collision.contacts[0].normal.x)) + 90.0f;
            //Check if spike is hit sufficiently close to facing direction
            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, hit_angle)) < 30.0f) {
                collision.gameObject.GetComponent<PlayerScript>().TakeDamage();
            }
            
        }
    }
}
