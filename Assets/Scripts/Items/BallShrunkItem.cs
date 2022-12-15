using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShrunkItem : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(ShrunkBall());
        }
    }

    IEnumerator ShrunkBall()
    {
        isWorking = true;
        Vector3 initSize = Ball.transform.localScale;
        audioPlayer.playSmallBallClip();
        Ball.transform.localScale -= new Vector3(0.5f, 0.5f, 0f);
        yield return new WaitForSeconds(workingTime);
        Ball.transform.localScale = initSize;
        audioPlayer.playLargeBallClip();
    }
}
