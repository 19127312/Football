using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBallItem : Item
{
    public PhysicsMaterial2D initPhysics;
    public PhysicsMaterial2D heavyPhysics;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            workingTime = 3.0f;
            StartCoroutine(HeavyBall());
        }
    }

    IEnumerator HeavyBall()
    {
        isWorking = true;
        audioPlayer.playInjureClip();
        Color initColor = Ball.GetComponent<SpriteRenderer>().material.color;
        Color customColor = new Color(0.0f, 0f, 1.0f, 1.0f);
        Ball.GetComponent<SpriteRenderer>().material.SetColor("_Color", customColor);
        Ball.GetComponent<Collider2D>().sharedMaterial = heavyPhysics;
        yield return new WaitForSeconds(workingTime);
        Ball.GetComponent<Collider2D>().sharedMaterial = initPhysics;
        Ball.GetComponent<SpriteRenderer>().material.SetColor("_Color", initColor);
    }
}
