using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMonster : MonoBehaviour
{
    public float moveSpeed;
    public int moveTime;
    
    private void Awake()
    {
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MD());
        gameObject.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if (transform.position.x < -moveTime)
        {
            gameObject.transform.position = new Vector2(moveTime, transform.position.y);
        }

    }
    IEnumerator MD()
    {
        moveTime = Random.Range(15, 30);
        yield return new WaitForSeconds(5f);
        
    }
}
