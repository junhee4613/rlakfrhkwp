using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float addSpeed;
    public float jumpForce;
    public int jumpCount;
    bool jumpBoll = true;
    bool run = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator an;
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
        //기본 이동기
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
            //대쉬
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
            sr.flipX = true;
            //대쉬
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity -= Vector2.left * -addSpeed;
            }
            if (rb.velocity.y == 0)
            {
                run = true;
            }
            

        }
        //2단 점프
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            jumpCount++;
            
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = rb.velocity * 0.5f;
        }

        an.SetBool("Ground", jumpBoll);
        an.SetBool("run", run);
        run = false;
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
        jumpBoll = true;
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {

        jumpBoll = false;
        
    }
    public void Die()
    {
        rb.velocity = Vector2.zero;
    }
}
