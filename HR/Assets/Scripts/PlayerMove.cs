using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{


    public AudioClip audioJump;
    public AudioClip audioClear;
    public AudioClip audioDie;

    CapsuleCollider2D capsule;
    AudioSource audioSou;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
   
    Animator anime;

    public float maxSpeed;
    public float jumpPower;

    bool live = true;
    bool JumpYN = false;
    int Jumpint = 0;


    void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        audioSou = GetComponent<AudioSource>();
    }
     void Update()
    {
        if (live)
        {
            //점프
            if (Input.GetButtonDown("Jump") && JumpYN == false)
            {
                //normalized벡터크기를 1로 만든 상태 단위 벡터
                //rigid.velocity = new Vector2(0,0);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                anime.SetBool("isJumping", true);
                audioSou.clip = audioJump;
                audioSou.Play();
                Jumpint++;
                if (Jumpint >= 2)
                {
                    JumpYN = true;
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
            if (Mathf.Abs(rigid.velocity.x) < 0.3)
            {
                anime.SetBool("isWalking", false);
            }
            else
                anime.SetBool("isWalking", true);

        }
    }

    void FixedUpdate()
    {
        if (live)
        {
            //캐릭 움직임 속도
            float h = Input.GetAxisRaw("Horizontal");

            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            //최대 속도
            if (rigid.velocity.x > maxSpeed)    // 오른쪽 속도 제어
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * (-1))    // 왼쪽 속도 제어
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


            //바닥체크
            /*
            if(rigid.velocity.y <0)
            {
                Debug.DrawRay(rigid.position, Vector3.down, Color.green);

                RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2, LayerMask.GetMask("Platform"));
                if (rayHit.collider != null)
                {
                    if (rayHit.distance < 2.8f);
                    anime.SetBool("isJumping", false);
                    JumpYN = false;
                    Jumpint = 0;
                }
            }
            */
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {


     if(collision.gameObject.tag == "Enemy")// 적 
        {
            Debug.Log("플레이어:  아얏!");
            audioSou.clip = audioDie;
            audioSou.Play();
            anime.SetBool("isDie", true);
            capsule.enabled = false;
            rigid.gravityScale = 0;
            live = false;
            Invoke("Die", 1.5f);

        }
     if (collision.gameObject.tag == "Finish")// 피니쉬
        {
            Debug.Log("피니쉬");
            audioSou.clip = audioClear;
            audioSou.Play();
            Invoke("ChangeSc", 2f);

        }
        /*if (collision.gameObject.tag == "Platform")// 바닥초기화
        {
            Debug.Log("땅");
            anime.SetBool("isJumping", false);
            JumpYN = false;
            Jumpint = 0;
        }*/

    }
    public void ChangeSc()
    {
        SceneManager.LoadScene("Finsh");
    }

    public void Die()
    {
        live = true;
        anime.SetBool("isDie", false);
        SceneManager.LoadScene("SampleScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Platform"))
        {
            Debug.Log("점프 초기화 : 땅을 밟음");
            anime.SetBool("isJumping", false);
            JumpYN = false;
            Jumpint = 0;
        }
    }
}
