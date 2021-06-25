using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInit : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("°³¾¾¹ß..");
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("¾¾¹ß..");
        }
    }
}
