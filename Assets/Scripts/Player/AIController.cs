using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AIController : MonoBehaviour
{
    public float rangeDefence = 5f;
    public float speed = 5f;
    public float jumpForce = 10f;
    private GameObject ball;
    private Rigidbody2D rb;
    float horizontal;
    float vertical;
    public Transform defence, checkGround;
    public bool canShoot, canHead, isGrounded;
    public GameObject shootEffect;
    public LayerMask groundLayer;
    public Animator anim;

    GameObject player;
    // Start is called before the first frame update
    void Awake()
    {

    }
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canShoot = false;
        isGrounded = true;
        player = GameObject.FindGameObjectWithTag("LeftPlayer");
        if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
        {
            Debug.Log("AI");
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        anim.SetFloat("Speed", move.magnitude);
        if (!GameController.instance.endMatch && !GameController.instance.isScored)
        {
            Move();
            Shoot();
            Jump();
        }
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, 0.2f, groundLayer);
    }

    public void Move()
    {
        if (Mathf.Abs(ball.transform.position.x - transform.position.x) < rangeDefence && transform.position.x > ball.transform.position.x)
        {
            if (Mathf.Abs(ball.transform.position.x - transform.position.x) <= Mathf.Abs(ball.transform.position.x - player.transform.position.x) && ball.transform.position.y < -0.5f)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                if (ball.transform.position.x > transform.position.x && ball.transform.position.y < -0.5f)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else if (ball.transform.position.y >= -0.5f && defence.position.x <= transform.position.x)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
            }
        }
        else
        {
            if (transform.position.x > defence.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else if (transform.position.x <= ball.transform.position.x)
            {
                if (isGrounded)
                {
                    rb.velocity = new Vector2(speed, 7);
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    void Shoot()
    {

        if (canShoot)
        {
            anim.SetTrigger("Kick");
            shootEffect.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250, 300));
            StartCoroutine(ExecuteAfterTime(0.1f));
        }
    }

    void Jump()
    {
        if (canHead && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        shootEffect.SetActive(false);
    }
}
