using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGrowItem : Item
{
    private GameObject childObject;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            childObject = this.gameObject.transform.GetChild(0).gameObject;
            childObject.GetComponent<SpriteRenderer>().enabled = false;
            workingTime = 3.0f;
            StartCoroutine(GrowGoal());
        }
    }

    IEnumerator GrowGoal()
    {
        isWorking = true;
        Vector3 initSize = Goal1.transform.localScale;
        BallController scriptName = Ball.GetComponent<BallController>();
        audioPlayer.playGoalGrowClip();
        if (scriptName.isLeftPlayer)
        {
            Goal2.transform.localScale += new Vector3(0f, 1f, 0f);
            yield return new WaitForSeconds(workingTime);
            Goal2.transform.localScale = initSize;
        }
        else
        {
            Goal1.transform.localScale += new Vector3(0f, 1f, 0f);
            yield return new WaitForSeconds(workingTime);
            Goal1.transform.localScale = initSize;
        }
        audioPlayer.playGoalShrinkClip();
    }
}
