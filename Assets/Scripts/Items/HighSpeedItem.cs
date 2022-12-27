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
        AIController aiScript;
        audioPlayer.playSpeedClip();
        if (ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();
            playerScript.speedLightEffect.SetActive(true);
            playerScript.speed += 2.5f;
            yield return new WaitForSeconds(workingTime);
            playerScript.speed -= 2.5f;
            playerScript.speedLightEffect.SetActive(false);
        }
        else
        {
            if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
            {
                aiScript = AI.GetComponent<AIController>();
                aiScript.speedLightEffect.SetActive(true);
                aiScript.speed += 2.5f;
                yield return new WaitForSeconds(workingTime);
                aiScript.speed -= 2.5f;
                aiScript.speedLightEffect.SetActive(false);
            }
            else
            {
                playerScript = Player2.GetComponent<PlayerController>();
                playerScript.speedLightEffect.SetActive(true);
                playerScript.speed += 2.5f;
                yield return new WaitForSeconds(workingTime);
                playerScript.speed -= 2.5f;
                playerScript.speedLightEffect.SetActive(false);
            }
        }
    }
}
