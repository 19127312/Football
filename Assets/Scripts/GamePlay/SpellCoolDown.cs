using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellCoolDown : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;

    [SerializeField]
    private TMP_Text textCooldown;

    private bool isCoolDown = false;
    private float coolDownTime = 10.0f;
    private float coolDownTimer = 0.0f;
    private GameManager gameManager;
    public bool isLeftSpell;

    // Start is called before the first frame update
    void Start()
    {
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;

        gameManager = FindObjectOfType<GameManager>();
        if (isLeftSpell)
        {
            coolDownTime = (float)gameManager.SelectedCharacter1.CoolDownTime;
        }
        else
        {
            coolDownTime = (float)gameManager.SelectedCharacter1.CoolDownTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolDown && !GameController.instance.isPaused)
        {
            ApplyCoolDown();
        }
    }

    void ApplyCoolDown()
    {
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer < 0.0f)
        {
            isCoolDown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(coolDownTimer).ToString();
            imageCooldown.fillAmount = coolDownTimer / coolDownTime;
        }
    }

    public void UseSpeel(bool isLeftPlayer)
    {
        if (isCoolDown)
        {
            // Do nothing
        }
        else
        {
            isCoolDown = true;
            textCooldown.gameObject.SetActive(true);
            coolDownTimer = coolDownTime;
        }
    }
}
