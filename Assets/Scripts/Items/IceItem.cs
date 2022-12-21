using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceItem : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(Freeze());
        }
    }

    IEnumerator Freeze()
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
        audioPlayer.playIceClip();
        float initSpeed = playerScript.speed;
        float initJumpForce = playerScript.jumpForce;

        playerScript.speed = 0;
        playerScript.jumpForce = 0;
        playerScript.freezeEffect.SetActive(true);
        playerScript.isFreezed = true;

        yield return new WaitForSeconds(workingTime);

        playerScript.speed = initSpeed;
        playerScript.jumpForce = initJumpForce;
        playerScript.freezeEffect.SetActive(false);
        playerScript.isFreezed = false;
        // audioPlayer.playSmallBallClip();
    }
}
