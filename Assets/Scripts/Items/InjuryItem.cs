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
        audioPlayer.playIceClip();
        playerScript.brokenLegEffect.SetActive(true);
        playerScript.isFreezed = true;
        yield return new WaitForSeconds(workingTime);
        playerScript.brokenLegEffect.SetActive(false);
        playerScript.isFreezed = false;

    }
}
