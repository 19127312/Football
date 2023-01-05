using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class ExpBar : MonoBehaviour
{
    public TMP_Text levelText;
    [SerializeField] Image fillImage;
    [SerializeField] Image outlineImage;
    [SerializeField] Image levelImage;
    [SerializeField] Color color;
    public int currentLevel { get; set; }
    public float currentExp { get; set; }
    public float maxExp { get; set; }
    void Start()
    {
        InitColor();
    }

    void Update()
    {

    }
    public void AddExp(Character character, float exp)
    {
        if (character.Level == 0)
        {
            character.Level = 1;
        }
        currentExp = character.CurrentExp;
        currentLevel = character.Level;
        levelText.text = currentLevel.ToString();
        float totalExp = currentExp + exp;
        maxExp = currentLevel * 100;
        StartCoroutine(AnimateProgress(currentExp / maxExp, totalExp / maxExp, 0.5f, character));
    }
    void InitColor()
    {
        fillImage.color = color;
        outlineImage.color = color;
        levelImage.color = color;
    }

    private IEnumerator AnimateProgress(float start, float end, float duration, Character character)
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float progress = Mathf.Lerp(start, end, counter / duration);
            fillImage.fillAmount = progress;
            yield return null;
        }
        if (fillImage.fillAmount >= 1)
        {
            LevelUp(character, end * maxExp - maxExp);
        }
        else
        {
            character.CurrentExp = end * maxExp;
        }
    }
    void LevelUp(Character character, float exp)
    {
        currentLevel++;
        changeStat(character);
        character.Level = currentLevel;
        character.CurrentExp = 0;
        levelText.text = currentLevel.ToString();
        AddExp(character, exp);
    }

    void changeStat(Character character)
    {
        if (character.Level < 11)
        {
            character.StatToUpgrade += 1;
        }
    }
}
