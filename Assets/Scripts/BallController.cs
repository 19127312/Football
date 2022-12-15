using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    public float speed = 5f;

    public float timeunteleportable = 0.5f;
    bool isUnteleportable;
    float teleportableTimer;

    public bool isLeftPlayer = true;

    AudioPlayerController audioPlayer;

    GameObject leftPlayer;
    GameObject rightPlayer;

    // Start is called before the first frame update
    void Start()
    {
        leftPlayer = GameObject.FindGameObjectWithTag("LeftPlayer");
        rightPlayer = GameObject.FindGameObjectWithTag("RightPlayer");
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = UnityEngine.Input.GetAxis("Horizontal");
        vertical = UnityEngine.Input.GetAxis("Vertical");

        if (isUnteleportable)
        {
            teleportableTimer -= Time.deltaTime;
            if (teleportableTimer < 0)
            {
                isUnteleportable = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += (speed * horizontal * Time.deltaTime);
        position.y += (speed * vertical * Time.deltaTime);
        rb2d.MovePosition(position);
    }

    public void Tele(Vector2 portPosition)
    {
        if (isUnteleportable)
            return;
        isUnteleportable = true;
        teleportableTimer = timeunteleportable;

        transform.position = portPosition;
        audioPlayer.playPortalClip();
    }

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayerController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "LeftPlayer")
        {
            leftPlayer.GetComponent<Player1Controller>().canShoot = true;
        }
        else if (col.gameObject.tag == "RightPlayer")
        {
            rightPlayer.GetComponent<Player1Controller>().canShoot = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "LeftPlayer")
        {
            leftPlayer.GetComponent<Player1Controller>().canShoot = false;
        }
        else if (col.gameObject.tag == "RightPlayer")
        {
            rightPlayer.GetComponent<Player1Controller>().canShoot = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "LeftPlayer")
        {
            isLeftPlayer = true;
        }
        else if (other.gameObject.tag == "RightPlayer")
        {
            isLeftPlayer = false;
        }
    }
}
