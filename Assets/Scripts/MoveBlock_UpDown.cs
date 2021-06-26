using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock_UpDown : MonoBehaviour
{

    public float updown_Y_speed;
    public float max_Y;

    bool ismove_updown = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(ismove_updown)
        {
            transform.Translate(new Vector3(0, updown_Y_speed, 0) * Time.deltaTime);
            if (this.transform.position.y > max_Y)
            {
                ismove_updown = false;
            }
        }
        else
        {
            transform.Translate(new Vector3(0, -updown_Y_speed, 0)* Time.deltaTime);
            if (this.transform.position.y < -max_Y) 
            {
                ismove_updown = true;
            }
        }

    }
}
