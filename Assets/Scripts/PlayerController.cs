using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private AudioSource audi;
    
    private bool grounded;

    private ScoreManager theScoreManager;

    public float speed = 8f;
    public float jumpForce = 10f;
    public GameObject mainCamera;

    private float cameraOffsetX;


    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audi = GetComponent<AudioSource>();

        theScoreManager = FindObjectOfType<ScoreManager>();

        cameraOffsetX = -transform.position.x;
	}

    private void Update()
    {
        if(transform.position.y <= Camera.main.ViewportToWorldPoint(new Vector3(0, -1f, 10)).y)
            Die();
    }

    // use FixedUpdate for physics calculations
    void FixedUpdate ()
    {
        // have camera follow player
        mainCamera.transform.position = new Vector3(transform.position.x + cameraOffsetX, mainCamera.transform.position.y, mainCamera.transform.position.z); 

        // constant force added to player
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

        // player jump
        if (grounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            if (collision.gameObject.tag == "Mummy")
            {
                FindObjectOfType<AudioManager>().Play("whymummies");
                Die(2.5f);
            }
            else
                Die();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
            grounded = false;
    }

    public void Die(float delay = 1f)
    {
        // stop movement if player hits obstacle and restart scene
        rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<Animator>().speed = 0f;

        Invoke("Restart", delay);
    }

    void Restart()
    {
        rb2d.constraints = RigidbodyConstraints2D.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
