using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMove : MonoBehaviour
{
    public float up_time;
    public float down_time;

    float delayTime;

    void Start()
    {
        
    }

    void Update()
    {
        delayTime += Time.deltaTime;

        if (delayTime < up_time) //나타남
        {
            transform.Translate(new Vector3(0, 1.5f, 0) * Time.deltaTime);
        }
        if (delayTime >= down_time)  //사라짐
        {
            transform.Translate(new Vector3(0, -1.5f, 0) * Time.deltaTime);
            if (delayTime >= up_time + down_time)
            {
                delayTime = 0;
            }
        }

    }
}
