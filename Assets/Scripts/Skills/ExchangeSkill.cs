using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeSkill : Skills
{
    // Start is called before the first frame update
    public override void UseSkill(bool isLeftPlayer)
    {
        base.UseSkill(isLeftPlayer);
        //TODO: Implement skill

        StartCoroutine(Exchange());
    }

    IEnumerator Exchange()
    {
        audioPlayer.playSkillExchange();
        GameObject Player1 = GameObject.Find("Player 1");
        // PlayerController player1Script = Player1.GetComponent<PlayerController>();
        Vector2 leftPosition = Player1.transform.position;
        if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
        {
            GameObject AI = GameObject.Find("AI");
            Vector2 rightPosition = AI.transform.position;
            Player1.transform.position = rightPosition;
            AI.transform.position = leftPosition;
        }
        else
        {
            GameObject Player2 = GameObject.Find("Player 2");
            Vector2 rightPosition = Player2.transform.position;
            Player1.transform.position = rightPosition;
            Player2.transform.position = leftPosition;
        }
        yield return 0;
    }
}
