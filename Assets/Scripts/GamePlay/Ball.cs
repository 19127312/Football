using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    GameObject Lplayer;
    GameObject Rplayer;
    GameObject AI;
    public GameObject goal;
    public Rigidbody2D rb;
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
        if (col.gameObject.tag == "RightGoal")
        {
            if (!GameController.instance.isScored && !GameController.instance.endMatch)
            {
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
                GameController.instance.goalRightCount++;
                GameController.instance.isScored = true;
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                GameController.instance.ContinueGame();
            }
        }
    }
}
