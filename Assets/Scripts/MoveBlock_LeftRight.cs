using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock_LeftRight : MonoBehaviour
{
    public float leftright_x_speed;
    public float max_X;

    bool ismove_leftright = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(ismove_leftright)
        {
            transform.Translate(new Vector3(leftright_x_speed, 0, 0) * Time.deltaTime);
            if (this.transform.position.x > max_X)
            {
                ismove_leftright = false;
            }
        }
        else
        {
            transform.Translate(new Vector3(-leftright_x_speed, 0, 0) * Time.deltaTime);
            if (this.transform.position.x < -max_X)
            {
                ismove_leftright = true;
            }
        }
    }
    
}
