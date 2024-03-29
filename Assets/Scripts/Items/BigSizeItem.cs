using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSizeItem : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(GrowPlayerSize());
        }
    }

    IEnumerator GrowPlayerSize()
    {
        isWorking = true;
        Ball ballScript = Ball.GetComponent<Ball>();
        audioPlayer.playLargeBallClip();
        if (ballScript.isLeftPlayer)
        {
            Vector3 initSize = Player1.transform.localScale;
            Player1.transform.position += new Vector3(0f, 1f, 0f);
            Player1.transform.localScale += new Vector3(0.5f, 0.5f, 0f);
            yield return new WaitForSeconds(workingTime);
            Player1.transform.localScale = initSize;
        }
        else
        {
            if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
            {
                Vector3 initSize = AI.transform.localScale;
                AI.transform.position += new Vector3(0f, 1f, 0f);
                AI.transform.localScale += new Vector3(0.5f, 0.5f, 0f);
                yield return new WaitForSeconds(workingTime);
                AI.transform.localScale = initSize;
            }
            else
            {
                Vector3 initSize = Player2.transform.localScale;
                Player2.transform.position += new Vector3(0f, 1f, 0f);
                Player2.transform.localScale += new Vector3(0.5f, 0.5f, 0f);
                yield return new WaitForSeconds(workingTime);
                Player2.transform.localScale = initSize;
            }
        }
        audioPlayer.playSmallBallClip();
    }
}
