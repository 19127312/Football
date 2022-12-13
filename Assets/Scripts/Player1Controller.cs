using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player1Controller : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    public float speed = 5f;
    public float jumpForce = 10f;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool canShoot;
    private GameObject _ball;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        canShoot = false;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        movePlayer();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void movePlayer()
    {
        if (Input.GetKey(KeyCode.A))
            rb.velocity = new Vector2(speed * -1, rb.velocity.y);
        if (Input.GetKey(KeyCode.D))
            rb.velocity = new Vector2(speed, rb.velocity.y);
        if (Input.GetKey(KeyCode.W))
        {
            if (isGrounded)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetTrigger("Kick");
        }
    }

    public void Shoot()
    {


        if (canShoot)
        {
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(150, 100));
        }

    }
}
