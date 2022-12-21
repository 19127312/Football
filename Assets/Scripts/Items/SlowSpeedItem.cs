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
        Ball ballScript = Ball.GetComponent<Ball>();
        PlayerController playerScript;
        if (!ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();
        }
        else
        {
            playerScript = Player2.GetComponent<PlayerController>();
        }
        // audioPlayer.playLargeBallClip();
        audioPlayer.playSlowSpeedClip();
        playerScript.blackLightningEffect.SetActive(true);
        playerScript.speed -= 2.5f;
        yield return new WaitForSeconds(workingTime);
        playerScript.speed += 2.5f;
        playerScript.blackLightningEffect.SetActive(false);
    }
}
