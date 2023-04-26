using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float addSpeed;
    public float jumpForce;
    public int jumpCount;
    bool jumpBoll;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //�⺻ �̵���
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if(rb.velocity.x > 0)
        {
            sr.flipX = false;
            //�뽬
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity += Vector2.right * addSpeed;
            }
        }
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
            //�뽬
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity -= Vector2.left * -addSpeed;
            }
        }
        //2�� ����
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {

            rb.velocity += Vector2.up * jumpForce;
            jumpCount++;
            jumpBoll = false;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpBoll = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount == 1 && jumpBoll)
        {
            rb.velocity = Vector2.zero;
            rb.velocity += Vector2.up * jumpForce;
            jumpCount++;
        }
        


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
    }
    public void Die()
    {
        rb.velocity = Vector2.zero;
    }
}
