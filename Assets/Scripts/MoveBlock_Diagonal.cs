using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock_Diagonal : MonoBehaviour
{
    public float diagonal_xy_speed;
    public float max_X;
    public float max_Y;

    bool ismove_diagonal = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (ismove_diagonal)
        {
            transform.Translate(new Vector3(diagonal_xy_speed, diagonal_xy_speed, 0) * Time.deltaTime);
            if (this.transform.position.x > max_X && this.transform.position.y>max_Y)
            {
                ismove_diagonal = false;
            }
        }
        else
        {
            transform.Translate(new Vector3(-diagonal_xy_speed, -diagonal_xy_speed, 0) * Time.deltaTime);
            if (this.transform.position.x < -max_X && this.transform.position.y < -max_Y)
            {
                ismove_diagonal = true;
            }
        }
    }
}
