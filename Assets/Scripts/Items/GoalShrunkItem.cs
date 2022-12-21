using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalShrunkItem : Item
{
    private GameObject childObject;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            childObject = this.gameObject.transform.GetChild(1).gameObject;
            childObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(ShrunkGoal());
        }
    }

    IEnumerator ShrunkGoal()
    {
        isWorking = true;
        Vector3 initSize = Goal1.transform.localScale;

        Ball scriptName = Ball.GetComponent<Ball>();
        audioPlayer.playGoalShrinkClip();
        if (!scriptName.isLeftPlayer)
        {
            Goal2.transform.position -= new Vector3(0f, 1f, 0f);
            Goal2.transform.localScale -= new Vector3(0f, 0.3f, 0f);
            yield return new WaitForSeconds(workingTime);
            Goal2.transform.localScale = initSize;
            Goal2.transform.position += new Vector3(0f, 1f, 0f);
        }
        else
        {
            Goal1.transform.position -= new Vector3(0f, 1f, 0f);
            Goal1.transform.localScale -= new Vector3(0f, 0.3f, 0f);
            yield return new WaitForSeconds(workingTime);
            Goal1.transform.localScale = initSize;
            Goal1.transform.position += new Vector3(0f, 1f, 0f);
        }
        audioPlayer.playGoalGrowClip();
    }
}
