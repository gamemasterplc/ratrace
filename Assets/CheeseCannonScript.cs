using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseCannonScript : MonoBehaviour
{
    [Header("Inspector-Set Value:")]
    public GameObject cheese_object;
    public float max_shoot_timer = 2.0f;
    private float shoot_timer;

    void Start()
    {
        shoot_timer = max_shoot_timer;
    }

    void Update()
    {
        shoot_timer -= Time.deltaTime;
        if(shoot_timer < 0) {
            Vector3 cheese_pos = transform.position;
            cheese_pos.x -= 1.1f;
            cheese_pos.y += 0.625f;
            GameObject temp = Instantiate<GameObject>(cheese_object);
            temp.transform.position = cheese_pos;
            shoot_timer = max_shoot_timer;
        }
    }
}
