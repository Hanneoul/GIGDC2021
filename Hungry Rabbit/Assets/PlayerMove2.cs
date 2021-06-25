using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class PlayerMove2 : MonoBehaviour

{
    public float moveSpeed = 5f;    //�̵� �ӵ�
    public float jumpSpeed = 5f;    //���� �ӵ�
    public bool isGrounded = false;
    public int jumpCount = 2; //����Ƚ��    2�� 3���� �ٲٸ� 3�� ����
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //������Ʈ�� �ҷ���
        jumpCount = 0;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;    //Ground�� ������ isGround�� true
            jumpCount = 2;          //Ground�� ������ ����Ƚ���� 2�� �ʱ�ȭ��
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        Debug.Log("AA");
        if (gameObject.CompareTag("Ground"))
        {
            Debug.Log("AA\\");
            isGrounded = true;    //Ground�� ������ isGround�� true
            jumpCount = 2;          //Ground�� ������ ����Ƚ���� 2�� �ʱ�ȭ��
        }
    }



    void Update()
    {
        if (isGrounded)
        {
            if (jumpCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))   //�Է�Ű�� ��ȭ��ǥ�� ������
                {
                    rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode2D.Impulse); //���������� �ö󰡰���
                    jumpCount--;    //�����Ҷ� ���� ����Ƚ�� ����
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))    //����ȭ��ǥ �Է½� ������
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))    //������ȭ��ǥ �Է½� ������
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
}