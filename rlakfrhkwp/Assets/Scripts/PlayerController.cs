using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private int jumpCount;
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
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if(rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            jumpCount++;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            
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
        Destroy(gameObject);
    }
}
