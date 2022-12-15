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
    public Transform defence;
    public bool canShoot;
    public GameObject shootEffect;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rb = GetComponent<Rigidbody2D>();
        canShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    public void Move()
    {
        if (Mathf.Abs(ball.transform.position.x - transform.position.x) < rangeDefence)
        {
            if (ball.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else
        {
            if (transform.position.x > defence.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
            }
        }
    }

    void Shoot()
    {
        if (canShoot)
        {
            shootEffect.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400, 500));
            StartCoroutine(ExecuteAfterTime(0.1f));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        shootEffect.SetActive(false);
    }
}
