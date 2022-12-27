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
        AIController aiScript;
        audioPlayer.playSlowSpeedClip();
        if (!ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();

            playerScript.blackLightningEffect.SetActive(true);
            playerScript.speed -= 2.5f;
            yield return new WaitForSeconds(workingTime);
            playerScript.speed += 2.5f;
            playerScript.blackLightningEffect.SetActive(false);
        }
        else
        {
            if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
            {
                aiScript = AI.GetComponent<AIController>();
                aiScript.blackLightningEffect.SetActive(true);
                aiScript.speed -= 2.5f;
                yield return new WaitForSeconds(workingTime);
                aiScript.speed += 2.5f;
                aiScript.blackLightningEffect.SetActive(false);
            }
            else
            {
                playerScript = Player2.GetComponent<PlayerController>();
                playerScript.blackLightningEffect.SetActive(true);
                playerScript.speed -= 2.5f;
                yield return new WaitForSeconds(workingTime);
                playerScript.speed += 2.5f;
                playerScript.blackLightningEffect.SetActive(false);
            }
        }
    }
}
