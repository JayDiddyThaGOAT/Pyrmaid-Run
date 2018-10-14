using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    public float speed;

    private Transform target;
    private Vector2 distance;

    private Rigidbody2D rb2D;
    private SpriteRenderer sr2D;
  

	// Use this for initialization
	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sr2D = GetComponent<SpriteRenderer>();

        // get a reference to player object
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        distance = target.position - transform.position;
        rb2D.velocity = new Vector2(-distance.normalized.x * speed, rb2D.velocity.y);

        if (rb2D.velocity.x < 0f)
            sr2D.flipX = true;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Animator>().speed = 0f;
        }
    }


}
