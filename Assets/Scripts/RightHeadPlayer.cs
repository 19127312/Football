using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHeadPlayer : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;
    public GameObject headEffect;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        player = GameObject.FindGameObjectWithTag("RightPlayer");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            player.GetComponent<PlayerController>().anim.SetTrigger("Head");
            headEffect.SetActive(true);
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 400));
            StartCoroutine(ExecuteAfterTime(0.1f));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        headEffect.SetActive(false);
    }
}
