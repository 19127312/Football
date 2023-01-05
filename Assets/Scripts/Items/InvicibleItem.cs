using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvicibleItem : Item
{
    public Sprite InvicibleSprite;

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
        Sprite initSprite = Ball.GetComponent<SpriteRenderer>().sprite;
        Ball.GetComponent<SpriteRenderer>().sprite = InvicibleSprite;
        bool activeFire = Ball.gameObject.transform.GetChild(0).gameObject.activeSelf;
        bool activeFrost = Ball.gameObject.transform.GetChild(1).gameObject.activeSelf;
        bool waterFrost = Ball.gameObject.transform.GetChild(2).gameObject.activeSelf;
        Ball.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Ball.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        Ball.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        yield return new WaitForSeconds(workingTime);
        Ball.GetComponent<SpriteRenderer>().sprite = initSprite;
        Ball.gameObject.transform.GetChild(0).gameObject.SetActive(activeFire);
        Ball.gameObject.transform.GetChild(1).gameObject.SetActive(activeFrost);
        Ball.gameObject.transform.GetChild(2).gameObject.SetActive(waterFrost);
    }
}
