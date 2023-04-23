using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if(other.gameObject.tag == "Player" && gameObject.tag == "RedGround")
        {
            playerController.Die();
        }
    }
}
