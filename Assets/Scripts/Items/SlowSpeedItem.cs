using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpeedItem : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(SlowSpeed());
        }
    }

    IEnumerator SlowSpeed()
    {
        isWorking = true;
        BallController ballScript = Ball.GetComponent<BallController>();
        Player1Controller playerScript;
        if (!ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<Player1Controller>();
        }
        else
        {
            playerScript = Player2.GetComponent<Player1Controller>();
        }
        // audioPlayer.playLargeBallClip();
        playerScript.speed -= 2.5f;
        yield return new WaitForSeconds(workingTime);
        playerScript.speed += 2.5f;
        // audioPlayer.playSmallBallClip();
    }
}
