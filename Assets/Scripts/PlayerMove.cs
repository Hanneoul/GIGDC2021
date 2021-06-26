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

    float maxSpeed;
    public float jumpPower;
    public float moveSpeed;
    public float acceleration;
    public float slidePower;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsule;
   
    Animator anime;

    bool live = true;
    bool JumpYN = false;
    bool R_stop = false;
    bool L_stop = false;
    int Jumpint = 0;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        audioSou = GetComponent<AudioSource>();
        capsule = GetComponent<CapsuleCollider2D>();
    }
     void Update()
    {
        if (live)
        {
            //점프
            if (Input.GetKeyDown(KeyCode.Space) && JumpYN == false)
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

            //방향전환
            if (Input.GetButton("Horizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

            //캐릭터 이동
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                anime.SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                anime.SetBool("isWalking", true);
            }
            else
            {
                anime.SetBool("isWalking", false);
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                R_stop = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                L_stop = true;
            }

            if (R_stop)
            {
                moveSpeed -= acceleration * Time.deltaTime;
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if (moveSpeed <= 0.5)
                {
                    R_stop = false;
                    moveSpeed = 0;
                }
            }
            else if (L_stop)
            {
                moveSpeed -= acceleration * Time.deltaTime;
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                if (moveSpeed <= 0.5)
                {
                    L_stop = false;
                    moveSpeed = 0;
                }

            }
            else
            {
                moveSpeed = 5;
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
        if (collision.gameObject.tag == "Platform")// 바닥초기화
        {
            Debug.Log("땅");
            anime.SetBool("isJumping", false);
            JumpYN = false;
            Jumpint = 0;
        }
        if(collision.gameObject.tag=="IceBlock") //미끄러지기 MovePower보다 적게주면 더 미끄러짐
        {
            Debug.Log("아이스블럭");
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                moveSpeed -= slidePower * Time.deltaTime;
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if (moveSpeed <= 0.5)
                {
                    moveSpeed = 0;
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                moveSpeed -= slidePower * Time.deltaTime;
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                if (moveSpeed <= 0.5)
                {
                    moveSpeed = 0;
                }
            }
        }    

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
        if (collision.CompareTag("Platform"))
        {
            Debug.Log("점프 초기화 : 땅을 밟음");
            anime.SetBool("isJumping", false);
            JumpYN = false;
            Jumpint = 0;
        }
    }
}
