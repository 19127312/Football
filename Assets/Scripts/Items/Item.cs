using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update

    protected GameObject Ball;
    protected GameObject Player1;
    protected GameObject Player2;
    protected GameObject Goal1;
    protected GameObject Goal2;
    protected GameObject AI;
    protected bool isAIMode = false;

    protected bool isWorking = false;
    protected float workingTime = 10.0f;

    protected AudioPlayerController audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayerController>();
    }

    void Start()
    {
        Ball = GameObject.Find("Ball");
        Player1 = GameObject.Find("Player 1");
        Player2 = GameObject.Find("Player 2");
        Goal1 = GameObject.Find("LeftGoal");
        Goal2 = GameObject.Find("RightGoal");
        AI = GameObject.Find("AI");
    }

    // Update is called once per frame
    void Update() { }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;

            // enable background
            GameObject backGround = this.gameObject.transform.GetChild(0).gameObject;
            backGround.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void DestroyGameObject()
    {
        if (!isWorking)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyGameObjectWithTime());
        }
    }

    IEnumerator DestroyGameObjectWithTime()
    {
        yield return new WaitForSeconds(workingTime);
        Destroy(gameObject);
    }
}
