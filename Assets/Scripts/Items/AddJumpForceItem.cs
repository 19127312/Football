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
        AIController aiScript;
        audioPlayer.playHighJumpClip();
        if (ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();
            playerScript.rocketEffect.SetActive(true);
            playerScript.jumpForce += 5.0f;
            yield return new WaitForSeconds(workingTime);
            playerScript.jumpForce -= 5.0f;
            playerScript.rocketEffect.SetActive(false);
        }
        else
        {
            if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
            {
                aiScript = AI.GetComponent<AIController>();
                aiScript.rocketEffect.SetActive(true);
                aiScript.jumpForce += 5.0f;
                yield return new WaitForSeconds(workingTime);
                aiScript.jumpForce -= 5.0f;
                aiScript.rocketEffect.SetActive(false);
            }
            else
            {
                playerScript = Player2.GetComponent<PlayerController>();
                playerScript.rocketEffect.SetActive(true);
                playerScript.jumpForce += 5.0f;
                yield return new WaitForSeconds(workingTime);
                playerScript.jumpForce -= 5.0f;
                playerScript.rocketEffect.SetActive(false);
            }
        }
    }
}
