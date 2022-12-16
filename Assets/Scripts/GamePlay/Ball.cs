using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    GameObject Lplayer;
    GameObject Rplayer;
    GameObject AI;
    // Start is called before the first frame update
    void Start()
    {
        Lplayer = GameObject.FindGameObjectWithTag("LeftPlayer");
        Rplayer = GameObject.FindGameObjectWithTag("RightPlayer");
        AI = GameObject.FindGameObjectWithTag("AI");
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
    }
}
