using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [Header("Music and Sound Options")]
    public Image musicAndSoundOption;
    private AudioPlayerController audioPlayerController;
    private GameManager gameManager;


    public Sprite
        soundSprite,
        muteSprite,
        musicSprite;
    private UnityAction newGameAction;
    private UnityAction backMainMenuAction;

    [Header("Current Player 1")]
    public Image currentCharacter1Image;
    public GameObject currentPrice1Panel;
    public TextMeshProUGUI currentPrice1Text;
    public TextMeshProUGUI currentShoot1Text;
    public TextMeshProUGUI currentSpeed1Text;
    public TextMeshProUGUI currentJump1Text;


    [Header("Current Player 2")]
    public Image currentCharacter2Image;
    public GameObject currentPrice2Panel;
    public TextMeshProUGUI currentPrice2Text;

    public TextMeshProUGUI currentShoot2Text;
    public TextMeshProUGUI currentSpeed2Text;
    public TextMeshProUGUI currentJump2Text;

    [Header("Menu")]
    public GameObject selecteModeMenu;
    public GameObject levelUpMenu;
    public GameObject OneVsOneMenu;
    public GameObject OneVsAIMenu;
    public GameObject chooseLevelMenu;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        audioPlayerController = FindObjectOfType<AudioPlayerController>();
        if (audioPlayerController.audioState == "Music")
        {
            musicAndSoundOption.sprite = musicSprite;
        }
        else if (audioPlayerController.audioState == "Sound")
        {
            musicAndSoundOption.sprite = soundSprite;
        }
        else if (audioPlayerController.audioState == "Mute")
        {
            musicAndSoundOption.sprite = muteSprite;
        }
    }

    // Update is called once per frame
    public void NewGame()
    {
        audioPlayerController.playButtonClickClip();
        newGameAction = () => SceneManager.LoadScene("ChooseCharacterScene");
        StartCoroutine(HoldHostage(newGameAction));

    }
    public void BackMainMenu()
    {
        audioPlayerController.playButtonClickClip();
        backMainMenuAction = () => SceneManager.LoadScene("MenuScene");
        StartCoroutine(HoldHostage(backMainMenuAction));

    }
    public void BackToSelectModeMenu()
    {
        selecteModeMenu.SetActive(true);
        OneVsOneMenu.SetActive(false);
        OneVsAIMenu.SetActive(false);
        levelUpMenu.SetActive(false);
    }
    public void LoadGame()
    {
        audioPlayerController.playButtonClickClip();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LevelUpPlayer()
    {
    }
    public void OneVsOne()
    {
        selecteModeMenu.SetActive(false);
        OneVsOneMenu.SetActive(true);
    }
    public void OneVsAI()
    {

    }
    public void ChangeVolume()
    {
        // Sound only -> Mute
        if (musicAndSoundOption.sprite == soundSprite)
        {
            musicAndSoundOption.sprite = muteSprite;
            audioPlayerController.Mute();
        }
        //Mute -> Music
        else if (musicAndSoundOption.sprite == muteSprite)
        {
            musicAndSoundOption.sprite = musicSprite;
            audioPlayerController.UnMute();
        }
        //Music -> Sound only
        else if (musicAndSoundOption.sprite == musicSprite)
        {
            musicAndSoundOption.sprite = soundSprite;
            audioPlayerController.playSoundOnly();

        }

    }
    public void goToLeftCharacter(int playerNumber)
    {
        audioPlayerController.playButtonClickClip();

        gameManager.changeLeftCharacter(playerNumber);
        updateCurrentCharacter(gameManager.GetSelectedCharacter(playerNumber), playerNumber);
    }
    public void goToRightCharacter(int playerNumber)
    {
        audioPlayerController.playButtonClickClip();

        gameManager.changeRightCharacter(playerNumber);
        updateCurrentCharacter(gameManager.GetSelectedCharacter(playerNumber), playerNumber);
    }
    public void updateCurrentCharacter(Character character, int playerNumber)
    {
        Debug.Log(character.ShootStat.ToString());
        if (playerNumber == 1)
        {
            currentCharacter1Image.sprite = character.Image;
            currentPrice1Text.text = character.GoldToBuy.ToString();
            currentShoot1Text.text = character.ShootStat.ToString();
            currentSpeed1Text.text = character.SpeedStat.ToString();
            currentJump1Text.text = character.JumpStat.ToString();

            if (character.IsOwn)
            {
                currentPrice1Panel.SetActive(false);
            }
            else
            {
                currentPrice1Panel.SetActive(true);
            }
        }
        else
        {
            currentCharacter2Image.sprite = character.Image;
            currentPrice2Text.text = character.GoldToBuy.ToString();
            currentShoot2Text.text = character.ShootStat.ToString();
            currentSpeed2Text.text = character.SpeedStat.ToString();
            currentJump2Text.text = character.JumpStat.ToString();
            if (character.IsOwn)
            {
                currentPrice2Panel.SetActive(false);
            }
            else
            {
                currentPrice2Panel.SetActive(true);
            }
        }

    }
    private IEnumerator HoldHostage(UnityAction callback)
    {
        yield return new WaitForSeconds(0.5f);
        callback.Invoke();
    }

}
