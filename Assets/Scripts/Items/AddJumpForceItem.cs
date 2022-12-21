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
        audioPlayer.playHighJumpClip();
        playerScript.rocketEffect.SetActive(true);
        playerScript.jumpForce += 5.0f;
        yield return new WaitForSeconds(workingTime);
        playerScript.jumpForce -= 5.0f;
        playerScript.rocketEffect.SetActive(false);
    }
}
