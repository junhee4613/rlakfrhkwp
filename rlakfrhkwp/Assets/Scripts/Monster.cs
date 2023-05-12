using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hp;
    private Animator an;
    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp == 0)
        {
            die();
            //Destroy(gameObject, 0.49f);
        }
    }
    private void die()
    {
        an.SetTrigger("die");
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        hp -= 1;
    //    }
    //}

}
