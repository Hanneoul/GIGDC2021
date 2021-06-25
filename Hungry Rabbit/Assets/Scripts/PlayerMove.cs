using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{


    public AudioClip audioJump;
    public AudioClip audioClear;
    public AudioClip audioDie;

    AudioSource audioSou;

    public float maxSpeed;
    public float jumpPower;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
   
    Animator anime;

    public bool JumpYN = false;
    public int Jumpint = 0;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        audioSou = GetComponent<AudioSource>();
    }
     void Update()
    {

        //점프
        if (Input.GetButtonDown("Jump") && GameManager.Instance.jumpYN == false)
        {
            //normalized벡터크기를 1로 만든 상태 단위 벡터
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anime.SetBool("isJumping", true);
            audioSou.clip = audioJump;
            audioSou.Play();
            GameManager.Instance.jumpCnt++;
            if (GameManager.Instance.jumpCnt >= 2)
            {
                GameManager.Instance.jumpYN = true;
            }
        }
        //스피드 제어
        if (Input.GetButtonUp("Horizontal"))
        {
            //normalized벡터크기를 1로 만든 상태 단위 벡터
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //방향전환
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
       
        
        //Animation
        if(Mathf.Abs(rigid.velocity.x) <0.3)
        {
            anime.SetBool("isWalking", false);          
        }
        else
            anime.SetBool("isWalking", true);
    }

    void FixedUpdate()
    {
        //캐릭 움직임 속도
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        //최대 속도
        if (rigid.velocity.x > maxSpeed)    // 오른쪽 속도 제어
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1))    // 왼쪽 속도 제어
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        //바닥체크
        if(rigid.velocity.y <0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.85f);
                anime.SetBool("isJumping", false);
                JumpYN = false;
                Jumpint = 0;
            }
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
     if(collision.gameObject.tag == "Enemy")// 적 
        {
            Debug.Log("플레이어:  아얏!");
            audioSou.clip = audioDie;
            audioSou.Play();

            transform.position = new Vector3(0, 0, -1);
        }
     if (collision.gameObject.tag == "Finish")// 피니쉬
        {
            Debug.Log("피니쉬");
            audioSou.clip = audioClear;
            audioSou.Play();
            Invoke("ChangeSc", 2f);

        }
     if(collision.gameObject.tag == "Ground")
        {
            anime.SetBool("isJumping", false);
            JumpYN = false;
            Jumpint = 0;
        }
    }

    public void ChangeSc()
    {
        SceneManager.LoadScene("Finsh");
    }
}
