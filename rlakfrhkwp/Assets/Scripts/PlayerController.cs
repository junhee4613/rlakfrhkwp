using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int hp = 3;              //���
    public float moveSpeed;         //�̵� �ӵ�
    public float addSpeed;          //�޸���
    public float jumpForce;         //���� ��
    public int jumpCount;           //2�� ���� ����
    public int vt;                  //�˹� ���� ����
    bool jumpBoll = true;           //1�� ���� �ִ�
    bool run = false;               //�̵� �ִ�
    bool jumpBoll2 = false;         //2�� ���� �ִ�
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
        //�ν����� â�� �ӵ��� ǥ������
        velTest = rb.velocity;
        //�˹� ���� ����
        if(sr.flipX != true)
        {
            vt = -1;
        }
        else
        {
            vt = 1;

        }
        //�⺻ �̵���
        if (Input.anyKey)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        }
       
        if (rb.velocity.x > 0)
        {
            
            sr.flipX = false;
            //�뽬
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity += Vector2.right * addSpeed;
            }
            if (rb.velocity.y == 0)
            {
                run = true;
            }
        }
        if (rb.velocity.x < 0)
        {
            vt = 1;
            sr.flipX = true;
            //�뽬
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity -= Vector2.left * -addSpeed;
            }
            if (rb.velocity.y == 0)
            {
                run = true;
            } 

        }
        //2�� ����
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
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

        
        an.SetBool("run", run);
        an.SetBool("jump2", jumpBoll2);
        run = false;
        
    }
    
    //���� ����� ��
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.tag == "Monster")
        {
            rb.velocity = new Vector2(3, 3).normalized * vt * 5;
//            
            if(hp > 0)
            {
                an.SetTrigger("hurt");
                hp--;
            }
            
            if (hp <= 0)
            {
                this.Die();

            }
        }
        if (other.gameObject.tag == "Ground")
        {
            rb.velocity = Vector2.zero; //�˹� ���ϰ� �� �� �̲��� ����
            jumpCount = 0;
            an.SetBool("Ground", jumpBoll);
            
        }
        jumpBoll = true;

    }
    //���� ���� ��
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            if (!Input.anyKey)
            {
                rb.velocity = Vector2.zero;
            }
            jumpBoll2 = false;                      //2�� ���� �����ϰ� �ϴ� �� false�� �ٲ㼭 �ִϸ��̼��� 1�� ���� �Ѿ�� �� ����
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
        //���� ����
        if(other.gameObject.tag == "Score")
        {

        }
    }
    public void Die()
    {
        an.SetTrigger("Die");
    }
    
}
