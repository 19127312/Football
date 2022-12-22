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

    public Sprite
        soundSprite,
        muteSprite,
        musicSprite;


    [Header("Current Player 1")]
    public Image currentCharacter1Image;
    public Image currentShirt1Image;
    public GameObject currentPrice1Panel;
    public GameObject currentPriceShirt1Panel;
    public TextMeshProUGUI currentPrice1Text;
    public TextMeshProUGUI currentPriceShirt1Text;
    public TextMeshProUGUI currentShoot1Text;
    public TextMeshProUGUI currentSpeed1Text;
    public TextMeshProUGUI currentJump1Text;


    [Header("Current Player 2")]
    public Image currentCharacter2Image;
    public Image currentShirt2Image;
    public GameObject currentPrice2Panel;
    public GameObject currentPriceShirt2Panel;
    public TextMeshProUGUI currentPrice2Text;
    public TextMeshProUGUI currentPriceShirt2Text;
    public TextMeshProUGUI currentShoot2Text;
    public TextMeshProUGUI currentSpeed2Text;
    public TextMeshProUGUI currentJump2Text;

    [Header("Menu")]
    public GameObject selecteModeMenu;
    public GameObject levelUpMenu;
    public GameObject OneVsOneMenu;
    public GameObject OneVsAIMenu;
    public GameObject chooseLevelMenu;
    public GameObject mainMenu;

    private AudioPlayerController audioPlayerController;
    private GameManager gameManager;


    [Header("OneVsOne")]
    public TextMeshProUGUI currentMoneyText;
    public Button nextButton;
    public Image nextButtonImage;
    public Sprite nextEnableButtonImage;
    public Sprite nextDiableButtonImage;
    [Header("Choose Level Menu")]

    public Image levelDescriptionImage;
    public Sprite lv0;
    public Sprite lv1;
    public Sprite lv2;
    public Sprite lv3;
    public Sprite lv4;
    public Sprite lv5;


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
        updateCurrentCharacter(gameManager.GetSelectedCharacter(1), 1);
        updateCurrentCharacter(gameManager.GetSelectedCharacter(2), 2);
        updateCurrentShirt(gameManager.GetSelectedShirt(1), 1);
        updateCurrentShirt(gameManager.GetSelectedShirt(2), 2);

    }
    void Update()
    {
        if (currentMoneyText != null)
        {
            currentMoneyText.text = gameManager.CurrentMoney().ToString();
        }
    }

    // Update is called once per frame

    private void ManagementPlay()
    {
        switch (GameManager.instance.currentGameMode)
        {
            case GameManager.GameMode.OneVsOne:
                bool ownPlayer1 = gameManager.GetSelectedCharacter(1).IsOwn;
                bool ownPlayer2 = gameManager.GetSelectedCharacter(2).IsOwn;
                bool ownShirt1 = gameManager.GetSelectedShirt(1).IsOwn;
                bool ownShirt2 = gameManager.GetSelectedShirt(2).IsOwn;
                if (ownPlayer1 && ownPlayer2 && ownShirt1 && ownShirt2)
                {
                    nextButton.interactable = true;
                    nextButtonImage.sprite = nextEnableButtonImage;
                }
                else
                {
                    nextButton.interactable = false;
                    nextButtonImage.sprite = nextDiableButtonImage;
                }

                break;
            case GameManager.GameMode.OneVsAI:
                break;
            default:
                Debug.Log("Game Mode is not selected");
                break;
        }
    }
    public void NewGame()
    {
        audioPlayerController.playButtonClickClip();
        selecteModeMenu.SetActive(true);
        OneVsOneMenu.SetActive(false);
        OneVsAIMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        mainMenu.SetActive(false);
    }
    public void BackMainMenu()
    {
        mainMenu.SetActive(true);
        selecteModeMenu.SetActive(false);

    }
    public void BackToSelectModeMenu()
    {
        audioPlayerController.playButtonClickClip();
        selecteModeMenu.SetActive(true);
        OneVsOneMenu.SetActive(false);
        OneVsAIMenu.SetActive(false);
        levelUpMenu.SetActive(false);
    }
    public void BackFromChooseLevelMenuOneVsOne()
    {
        audioPlayerController.playButtonClickClip();
        chooseLevelMenu.SetActive(false);
        OneVsOneMenu.SetActive(true);
    }
    public void ToChooseLevelMenuOneVsOne()
    {
        audioPlayerController.playButtonClickClip();
        chooseLevelMenu.SetActive(true);
        OneVsOneMenu.SetActive(false);
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
        audioPlayerController.playButtonClickClip();

    }
    public void OneVsOne()
    {
        audioPlayerController.playButtonClickClip();
        selecteModeMenu.SetActive(false);
        OneVsOneMenu.SetActive(true);
        GameManager.instance.currentGameMode = GameManager.GameMode.OneVsOne;
    }
    public void OneVsAI()
    {
        audioPlayerController.playButtonClickClip();
        GameManager.instance.currentGameMode = GameManager.GameMode.OneVsAI;
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
        ManagementPlay();
    }
    public void goToRightCharacter(int playerNumber)
    {
        audioPlayerController.playButtonClickClip();

        gameManager.changeRightCharacter(playerNumber);
        updateCurrentCharacter(gameManager.GetSelectedCharacter(playerNumber), playerNumber);
        ManagementPlay();
    }
    public void goToLeftShirt(int playerNumber)
    {
        audioPlayerController.playButtonClickClip();

        gameManager.changeLeftShirt(playerNumber);
        updateCurrentShirt(gameManager.GetSelectedShirt(playerNumber), playerNumber);
        ManagementPlay();
    }
    public void goToRightShirt(int playerNumber)
    {
        audioPlayerController.playButtonClickClip();

        gameManager.changeRightShirt(playerNumber);
        updateCurrentShirt(gameManager.GetSelectedShirt(playerNumber), playerNumber);
        ManagementPlay();
    }
    public void updateCurrentCharacter(Character character, int playerNumber)
    {
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
                currentCharacter1Image.color = new Color(1f, 1f, 1f, 1f);

            }
            else
            {
                currentPrice1Panel.SetActive(true);
                currentCharacter1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
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
                currentCharacter2Image.color = new Color(1f, 1f, 1f, 1f);

            }
            else
            {
                currentPrice2Panel.SetActive(true);
                currentCharacter2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);

            }
        }

    }
    public void updateCurrentShirt(Shirt shirt, int playerNumber)
    {
        if (playerNumber == 1)
        {
            currentShirt1Image.sprite = shirt.Image;
            currentPriceShirt1Text.text = shirt.GoldToBuy.ToString();
            if (shirt.IsOwn)
            {
                currentPriceShirt1Panel.SetActive(false);
                currentShirt1Image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                currentPriceShirt1Panel.SetActive(true);
                currentShirt1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }
        else
        {
            currentShirt2Image.sprite = shirt.Image;
            currentPriceShirt2Text.text = shirt.GoldToBuy.ToString();
            if (shirt.IsOwn)
            {
                currentPriceShirt2Panel.SetActive(false);
                currentShirt2Image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                currentPriceShirt2Panel.SetActive(true);
                currentShirt2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }
    }
    public void BuyCharacter(int playerNumber)
    {
        if (gameManager.GetSelectedCharacter(playerNumber).GoldToBuy > gameManager.CurrentMoney())
        {
            return;
        }
        audioPlayerController.playBuyClip();

        gameManager.ModifyMoney(-gameManager.GetSelectedCharacter(playerNumber).GoldToBuy);
        gameManager.BuyCharacter(gameManager.GetSelectedCharacter(playerNumber));
        updateCurrentCharacter(GameManager.instance.SelectedCharacter1, 1);
        updateCurrentCharacter(GameManager.instance.SelectedCharacter2, 2);
        ManagementPlay();
    }
    public void BuyShirt(int playerNumber)
    {
        if (gameManager.GetSelectedShirt(playerNumber).GoldToBuy > gameManager.CurrentMoney())
        {
            return;
        }
        audioPlayerController.playBuyClip();

        gameManager.ModifyMoney(-gameManager.GetSelectedShirt(playerNumber).GoldToBuy);
        gameManager.BuyShirt(gameManager.GetSelectedShirt(playerNumber));
        updateCurrentShirt(GameManager.instance.SelectedShirt1, 1);
        updateCurrentShirt(GameManager.instance.SelectedShirt2, 2);
        ManagementPlay();
    }


    public void ChangeLevel(int level)
    {
        switch (level)
        {
            case 0:
                GameManager.instance.currentLevelPlayed = 0;
                levelDescriptionImage.sprite = lv0;
                break;
            case 1:
                GameManager.instance.currentLevelPlayed = 1;
                levelDescriptionImage.sprite = lv1;
                break;
            case 2:
                GameManager.instance.currentLevelPlayed = 2;
                levelDescriptionImage.sprite = lv2;
                break;
            case 3:
                GameManager.instance.currentLevelPlayed = 3;
                levelDescriptionImage.sprite = lv3;
                break;
            case 4:
                GameManager.instance.currentLevelPlayed = 4;
                levelDescriptionImage.sprite = lv4;
                break;
            case 5:
                GameManager.instance.currentLevelPlayed = 5;
                levelDescriptionImage.sprite = lv5;
                break;
            default:
                break;
        }
    }
    public void ChangeGameRule(int mode)
    {
        switch (mode)
        {
            case 0:
                GameManager.instance.currentGameRule = GameManager.GameRule.Random;
                break;
            case 1:
                GameManager.instance.currentGameRule = GameManager.GameRule.Score7;
                break;
            case 2:
                GameManager.instance.currentGameRule = GameManager.GameRule.Score9;
                break;
            case 3:
                GameManager.instance.currentGameRule = GameManager.GameRule.Time30;
                break;
            case 4:
                GameManager.instance.currentGameRule = GameManager.GameRule.Time60;
                break;
            default:
                break;
        }
    }
    public void PlayGameButton()
    {
        switch (GameManager.instance.currentLevelPlayed)
        {
            case 0:
                SceneManager.LoadScene("LevelScene" + UnityEngine.Random.Range(1, 6).ToString());
                break;
            case 1:
                SceneManager.LoadScene("LevelScene1");

                break;
            case 2:
                SceneManager.LoadScene("LevelScene2");

                break;
            case 3:
                SceneManager.LoadScene("LevelScene3");

                break;
            case 4:
                SceneManager.LoadScene("LevelScene4");

                break;
            case 5:
                SceneManager.LoadScene("LevelScene5");
                break;
            default:
                break;
        }
    }
}
