using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSkill : Skills
{
    // Start is called before the first frame update
    public override void UseSkill(bool isLeftPlayer)
    {
        base.UseSkill(isLeftPlayer);
        //TODO: Implement skill

        StartCoroutine(DefenceSkill());
        Debug.Log("Brick skill used");
    }

    IEnumerator DefenceSkill()
    {
        audioPlayer.playSkillDefence();
        GameObject foamFinger;
        if (isLeftSkill)
        {
            foamFinger = GameObject.Find("LeftFoamFinger");
        }
        else
        {
            foamFinger = GameObject.Find("RightFoamFinger");
        }

        FoamFinger foamFingerScript = foamFinger.GetComponent<FoamFinger>();
        foamFingerScript.direction = 1;
        yield return new WaitForSeconds(1.5f);
        foamFingerScript.direction = 0;
        yield return new WaitForSeconds(4);
        foamFingerScript.direction = -1;
        yield return new WaitForSeconds(1.5f);
        foamFingerScript.direction = 0;
    }
}
