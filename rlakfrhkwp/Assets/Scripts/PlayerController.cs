using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool moveBool = true;    //넉백 후 이동기 제어
    public int hp = 3;              //목숨
    public float moveSpeed;         //이동 속도
    public float addSpeed;          //달리기
    public float jumpForce;         //점프 힘
    public int jumpCount;           //2단 점프 조건
    public int vt;                  //넉백 방향 조절
    bool jumpBoll = true;           //1단 점프 애니
    bool jumpBoll2 = false;         //2단 점프 애니
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator an;
    public Vector2 velTest;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //인스펙터 창에 속도를 표시해줌
        velTest = rb.velocity;
        //넉백 방향 조절
        if(sr.flipX != true)
        {
            vt = -1;
        }
        else
        {
            vt = 1;

        }
        //기본 이동기
        if (Input.anyKey && moveBool)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
            an.SetTrigger("runT");

        }
        if (!Input.anyKey)
        {
            an.SetTrigger("idle");
        }
        
        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
            //대쉬
            if (Input.GetKey(KeyCode.LeftShift) && moveBool)
            {
                rb.velocity += Vector2.right * addSpeed;
            }
            if (rb.velocity.y == 0)
            {
                
            }
        }
        if (rb.velocity.x < 0)
        {
            vt = 1;
            sr.flipX = true;
            //대쉬
            if (Input.GetKey(KeyCode.LeftShift) && moveBool)
            {
                rb.velocity -= Vector2.left * -addSpeed;
            }
            if (rb.velocity.y == 0)
            {
                
            } 

        }
        //2단 점프
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && moveBool)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            jumpCount++;
            if (jumpCount == 2)
            {
                jumpBoll2 = true;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = rb.velocity * 0.5f;
        }

        

        an.SetBool("jump2", jumpBoll2);
    }
    
    //몬스터 닿았을 때
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        
        if (other.gameObject.tag == "Ground")
        {
            moveBool = true;
            rb.velocity = Vector2.zero; //넉백 당하고 난 후 미끄럼 방지
            jumpCount = 0;
            an.SetBool("Ground", jumpBoll);
            
        }
        jumpBoll = true;

    }
    //땅에 있을 때
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            if (!Input.anyKey)
            {
                rb.velocity = Vector2.zero;
            }
            jumpBoll2 = false;                      //2단 점프 가능하게 하는 걸 false로 바꿔서 애니메이션이 1단 점프 넘어가는 걸 방지
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            an.SetBool("Ground", !jumpBoll);
            rb.gravityScale = 1;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            moveBool = false;
            rb.velocity = Vector2.zero;
            hp -= 1;

            if (hp > 0)
            {
                an.SetTrigger("hurt");
                rb.velocity = new Vector2(3, 3).normalized * vt * 5;
            }
            else if(hp <= 0)
            {
                
                this.Die();
            }
        }
        //점수 구현
        if (other.gameObject.tag == "Score")
        {

        }
    }
    public void Die()
    {
        moveBool = false;
        an.SetTrigger("Die");
        StartCoroutine(PD());
    }
    IEnumerator PD()
    {
        yield return new WaitForSeconds(an.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
    
}
