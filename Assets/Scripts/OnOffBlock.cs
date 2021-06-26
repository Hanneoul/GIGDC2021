using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffBlock : MonoBehaviour
{
    public float on_time;
    public float off_time;

    float delayTime;

    public GameObject onoff_block;


    void Start()
    {

    }

    void Update()
    {
        delayTime += Time.deltaTime;
        
        if (delayTime < on_time) //나타남
        {
            onoff_block.SetActive(true);
        }
        if (delayTime >= on_time)  //사라짐
        {
            onoff_block.SetActive(false);
            if (delayTime >= on_time + off_time) 
            {
                delayTime = 0;
            }
        }

    }

    

    


}
