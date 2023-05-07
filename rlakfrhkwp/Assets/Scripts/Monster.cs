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
        if (hp == 0)
        {
            an.SetTrigger("die");
            Destroy(gameObject, 2f);
            //StartCoroutine(move());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            hp -= 1;
        }
    }
   // public IEnumerator move()
    //{
      // yield return new WaitForSeconds(0.6f);
        //Destroy(this.gameObject);
   // }
}
