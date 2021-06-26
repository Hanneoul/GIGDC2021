using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBlock : MonoBehaviour
{
    public float deltaTime;
    public float hideTime;
    public float appearTime;
    public bool SetActive;

    BoxCollider2D box;

    public GameObject hideBlock1;
    public GameObject hideBlock2;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        deltaTime += Time.deltaTime;
        if (SetActive == true)
        {
            if (deltaTime >= hideTime)
            {
                deltaTime = 0;
                hideBlock1.SetActive(false);
                hideBlock2.SetActive(false);
                SetActive = false;
                box.enabled = false;
            }
        }
        if (SetActive == false)
        {
            if (deltaTime >= appearTime)
            {
                deltaTime = 0;
                hideBlock1.SetActive(true);
                hideBlock2.SetActive(true);
                box.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("사라지는 블럭 밟음");
            SetActive = true;
        }
    }
}
