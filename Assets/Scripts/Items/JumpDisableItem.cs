using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDisableItem : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(DisableJump());
        }
    }

    IEnumerator DisableJump()
    {
        isWorking = true;
        BallController ballScript = Ball.GetComponent<BallController>();
        PlayerController playerScript;
        if (!ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();
        }
        else
        {
            playerScript = Player2.GetComponent<PlayerController>();
        }
        audioPlayer.playJumpDisableClip();
        float initjumpForce = playerScript.jumpForce;
        playerScript.bubbleGumEffect.SetActive(true);
        playerScript.jumpForce = 0f;
        yield return new WaitForSeconds(workingTime);
        playerScript.jumpForce = initjumpForce;
        playerScript.bubbleGumEffect.SetActive(false);
    }
}
