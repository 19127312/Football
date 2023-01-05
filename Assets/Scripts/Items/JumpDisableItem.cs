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
        Ball ballScript = Ball.GetComponent<Ball>();
        PlayerController playerScript;
        AIController aiScript;
        audioPlayer.playJumpDisableClip();
        if (!ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();
            float initjumpForce = playerScript.jumpForce;
            playerScript.bubbleGumEffect.SetActive(true);
            playerScript.jumpForce = 0f;
            yield return new WaitForSeconds(workingTime);
            playerScript.jumpForce = initjumpForce;
            playerScript.bubbleGumEffect.SetActive(false);
        }
        else
        {
            if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
            {
                aiScript = AI.GetComponent<AIController>();
                float initjumpForce = aiScript.jumpForce;
                aiScript.bubbleGumEffect.SetActive(true);
                aiScript.jumpForce = 0f;
                aiScript.isJumpDisabled = true;
                yield return new WaitForSeconds(workingTime);
                aiScript.jumpForce = initjumpForce;
                aiScript.isJumpDisabled = false;
                aiScript.bubbleGumEffect.SetActive(false);
            }
            else
            {
                playerScript = Player2.GetComponent<PlayerController>();
                float initjumpForce = playerScript.jumpForce;
                playerScript.bubbleGumEffect.SetActive(true);
                playerScript.jumpForce = 0f;
                yield return new WaitForSeconds(workingTime);
                playerScript.jumpForce = initjumpForce;
                playerScript.bubbleGumEffect.SetActive(false);
            }
        }
    }
}
