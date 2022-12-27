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
        AIController aiScript;
        audioPlayer.playIceClip();
        if (!ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();
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
        }
        else
        {
            if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
            {
                aiScript = AI.GetComponent<AIController>();
                float initSpeed = aiScript.speed;
                float initJumpForce = aiScript.jumpForce;

                aiScript.speed = 0;
                aiScript.jumpForce = 0;
                aiScript.freezeEffect.SetActive(true);
                aiScript.isFreezed = true;

                yield return new WaitForSeconds(workingTime);

                aiScript.speed = initSpeed;
                aiScript.jumpForce = initJumpForce;
                aiScript.freezeEffect.SetActive(false);
                aiScript.isFreezed = false;
            }
            else
            {
                playerScript = Player2.GetComponent<PlayerController>();
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
            }
        }

        // audioPlayer.playSmallBallClip();
    }
}
