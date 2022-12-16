using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvicibleItem : Item
{
    public Sprite InvicibleSprite;
    public Sprite initSprite;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(InvicibleBall());
        }
    }

    IEnumerator InvicibleBall()
    {
        isWorking = true;
        audioPlayer.playInvicibleClip();
        Ball.GetComponent<SpriteRenderer>().sprite = InvicibleSprite;
        yield return new WaitForSeconds(workingTime);
        Ball.GetComponent<SpriteRenderer>().sprite = initSprite;
    }
}
