using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    GameObject Lplayer;
    GameObject Rplayer;
    GameObject AI;

    public Rigidbody2D rb;

    public float timeunteleportable = 0.5f;
    bool isUnteleportable;
    float teleportableTimer;

    public bool isLeftPlayer = true;

    AudioPlayerController audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Lplayer = GameObject.FindGameObjectWithTag("LeftPlayer");
        Rplayer = GameObject.FindGameObjectWithTag("RightPlayer");
        AI = GameObject.FindGameObjectWithTag("AI");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUnteleportable)
        {
            teleportableTimer -= Time.deltaTime;
            if (teleportableTimer < 0)
            {
                isUnteleportable = false;
            }
        }
    }
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayerController>();
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
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "RightPlayer")
        {
            Rplayer.GetComponent<PlayerController>().canShoot = true;
        }
        if (col.gameObject.tag == "LeftPlayer")
        {
            Lplayer.GetComponent<PlayerController>().canShoot = true;
        }
        if (col.gameObject.tag == "AI")
        {
            AI.GetComponent<AIController>().canShoot = true;
        }
        if (col.gameObject.tag == "AIHead")
        {
            AI.GetComponent<AIController>().canHead = true;
        }
        if (col.gameObject.tag == "RightGoal")
        {
            if (!GameController.instance.isScored && !GameController.instance.endMatch)
            {
                audioPlayer.playScoreClip();
                GameController.instance.goalLeftCount++;
                GameController.instance.isScored = true;
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                GameController.instance.ContinueGame();
            }
        }
        if (col.gameObject.tag == "LeftGoal")
        {
            if (!GameController.instance.isScored && !GameController.instance.endMatch)
            {
                audioPlayer.playScoreClip();
                GameController.instance.goalRightCount++;
                GameController.instance.isScored = true;
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                GameController.instance.ContinueGame();
            }
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "RightPlayer")
        {
            Rplayer.GetComponent<PlayerController>().canShoot = false;
        }
        if (col.gameObject.tag == "LeftPlayer")
        {
            Lplayer.GetComponent<PlayerController>().canShoot = false;
        }
        if (col.gameObject.tag == "AI")
        {
            AI.GetComponent<AIController>().canShoot = false;
        }
        if (col.gameObject.tag == "AIHead")
        {
            AI.GetComponent<AIController>().canHead = false;
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        audioPlayer.playBallHit();
        if (other.gameObject.tag == "LeftPlayer")
        {
            isLeftPlayer = true;
        }
        else if (other.gameObject.tag == "RightPlayer" || other.gameObject.tag == "AI")
        {
            isLeftPlayer = false;
        }
    }
}
