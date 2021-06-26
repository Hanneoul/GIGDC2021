using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public float speed;
    bool detect;
    void Start()
    {

    }

    void Update()
    {
        if(detect)
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("°¨Áö");
            detect = true;
        }
        if(collision.CompareTag("Out"))
        {
            gameObject.SetActive(false);
        }
    }
}
