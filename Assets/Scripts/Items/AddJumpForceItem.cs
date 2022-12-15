using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddJumpForceItem : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(AddJumpForce());
        }
    }

    IEnumerator AddJumpForce()
    {
        isWorking = true;
        BallController ballScript = Ball.GetComponent<BallController>();
        PlayerController playerScript;
        if (ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();
        }
        else
        {
            playerScript = Player2.GetComponent<PlayerController>();
        }
        // audioPlayer.playLargeBallClip();
        playerScript.jumpForce += 10.0f;
        yield return new WaitForSeconds(workingTime);
        playerScript.jumpForce -= 10.0f;
        // audioPlayer.playSmallBallClip();
    }
}
