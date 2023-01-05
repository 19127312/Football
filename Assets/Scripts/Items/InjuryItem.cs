using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryItem : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(Injure());
        }
    }

    IEnumerator Injure()
    {
        isWorking = true;
        Ball ballScript = Ball.GetComponent<Ball>();
        PlayerController playerScript;
        AIController aiScript;
        audioPlayer.playIceClip();
        if (!ballScript.isLeftPlayer)
        {
            playerScript = Player1.GetComponent<PlayerController>();
            playerScript.brokenLegEffect.SetActive(true);
            playerScript.isFreezed = true;
            yield return new WaitForSeconds(workingTime);
            playerScript.brokenLegEffect.SetActive(false);
            playerScript.isFreezed = false;
        }
        else
        {
            if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
            {
                aiScript = AI.GetComponent<AIController>();
                aiScript.brokenLegEffect.SetActive(true);
                aiScript.isLegBroken = true;
                yield return new WaitForSeconds(workingTime);
                aiScript.brokenLegEffect.SetActive(false);
                aiScript.isLegBroken = false;
            }
            else
            {
                playerScript = Player2.GetComponent<PlayerController>();
                playerScript.brokenLegEffect.SetActive(true);
                playerScript.isFreezed = true;
                yield return new WaitForSeconds(workingTime);
                playerScript.brokenLegEffect.SetActive(false);
                playerScript.isFreezed = false;
            }
        }
    }
}
