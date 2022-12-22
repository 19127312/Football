using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayeSkill : MonoBehaviour
{
    // Start is called before the first frame update
    public Image leftPlayerSkill;
    public Image rightPlayerSkill;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        leftPlayerSkill.sprite = gameManager.SelectedCharacter1.GetSkillSprite();
        rightPlayerSkill.sprite = gameManager.SelectedCharacter2.GetSkillSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
