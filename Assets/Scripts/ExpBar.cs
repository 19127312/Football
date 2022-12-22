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
    public int currentLevel = 0;
    public float currentExp = 0;

    public float maxExp = 100;
    private Coroutine coroutine;

    void OnEnable()
    {
        fillImage.color = color;
        currentLevel = 0;
        currentExp = 0;
        fillImage.fillAmount = currentExp;
        UpdateExp(110, 10.0f);
    }

    void InitColor()
    {
        fillImage.color = color;
        outlineImage.color = color;
        levelImage.color = color;
    }

    public void UpdateExp(int exp, float duration)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        float targetExp = currentExp + exp;
        coroutine = StartCoroutine(FillRoutine(targetExp, duration));
    }

    private IEnumerator FillRoutine(float targetExp, float duration)
    {
        float timer = 0;
        float tempExp = currentExp;
        float diff = targetExp - tempExp;
        currentExp = targetExp;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float percent = timer / duration;
            fillImage.fillAmount = tempExp + diff * percent;
            yield return null;
        }
        // if (currentExp)
    }
}
