using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighSpeedItem : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(HighSpeed());
        }
    }

    IEnumerator HighSpeed()
    {
        isWorking = true;
        Ball ballScript = Ball.GetComponent<Ball>();
        PlayerController playerScript;
        if (ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();
        }
        else
        {
            playerScript = Player2.GetComponent<PlayerController>();
        }
        audioPlayer.playSpeedClip();
        playerScript.speedLightEffect.SetActive(true);
        playerScript.speed += 2.5f;
        yield return new WaitForSeconds(workingTime);
        playerScript.speed -= 2.5f;
        playerScript.speedLightEffect.SetActive(false);
    }
}
