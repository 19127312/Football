using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
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
    private GameObject ball;
    public Animator anim;
    public int ShootForce;
    Vector2 rawInput;
    public bool isLeftPlayer;
    public GameObject shootEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        isGrounded = true;
        canShoot = false;
        if (isLeftPlayer)
        {
            ShootForce = 400;
        }
        else
        {
            ShootForce = -400;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        anim.SetFloat("Speed", rawInput.magnitude);
    }

    void OnShoot(InputValue value)
    {
        anim.SetTrigger("Kick");
    }
    private void FixedUpdate()
    {
        movePlayer();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void movePlayer()
    {
        if (rawInput.x < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        if (rawInput.x > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (rawInput.y > 0)
        {
            Jump();
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            shootEffect.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ShootForce, 500));
            StartCoroutine(ExecuteAfterTime(0.1f));
        }

    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        shootEffect.SetActive(false);
    }
}
