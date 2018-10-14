using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool grounded;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    public Transform groundCheck;

    private ScoreManager theScoreManager;

    public float speed = 8f;
    public float jumpForce = 10f;
    public GameObject mainCamera;

    public float cameraOffsetX;


    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        theScoreManager = FindObjectOfType<ScoreManager>();
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    // use FixedUpdate for physics calculations
    void FixedUpdate ()
    {
        // have camera follow player
        mainCamera.transform.position = new Vector3(transform.position.x + cameraOffsetX, mainCamera.transform.position.y, mainCamera.transform.position.z); 

        // constant force added to player
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        print(grounded);
        // player jump
        if (grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }

	}
    
    // check if player is grounded or if player hits an obstalce
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // stop movement if player hits obstacle and restart scene
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            theScoreManager.scoreIncrease = false;
            theScoreManager.scoreCount = 0;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
            Debug.Log("Obstacle Hit");
            Invoke("Restart", 1f);
        }
    }

    void Restart()
    {
        rb2d.constraints = RigidbodyConstraints2D.None;
        theScoreManager.scoreIncrease = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
