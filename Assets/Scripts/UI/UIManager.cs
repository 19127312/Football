using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Music and Sound Options")]
    public Image musicAndSoundOption;

    public Sprite soundSprite,
        muteSprite,
        musicSprite;

    [Header("Current Level Up Character")]
    public Image currentCharacterLVImage;
    public TextMeshProUGUI currentShootText;
    public TextMeshProUGUI currentSpeedText;
    public TextMeshProUGUI currentJumpText;
    public TextMeshProUGUI currentLVText;
    public TextMeshProUGUI currentCDSkillLVText;
    public TextMeshProUGUI currentStatPointLVText;

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
    public TextMeshProUGUI currentCDSkillLV1Text;

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
    public TextMeshProUGUI currentCDSkillLV2Text;

    [Header("Menu")]
    public GameObject selecteModeMenu;
    public GameObject levelUpMenu;
    public GameObject OneVsOneMenu;
    public GameObject OneVsAIMenu;
    public GameObject chooseLevelMenu;
    public GameObject mainMenu;
    public GameObject settingMenu;
    public GameObject warningMenu;

    private AudioPlayerController audioPlayerController;
    private GameManager gameManager;

    [Header("Share Resources")]
    public GameObject currentCharacter1Panel;
    public GameObject MoneyPanel;

    [Header("OneVsOne")]
    public TextMeshProUGUI currentMoneyText;

    public Button nextButton;
    public Image nextButtonImage;
    public Sprite nextEnableButtonImage;
    public Sprite nextDiableButtonImage;

    [Header("OneVsAI")]
    public Button nextButtonAI;
    public Image nextButtonImageAI;

    [Header("Choose Level Menu")]
    public Image levelDescriptionImage;
    public Sprite map0;
    public Sprite map1;
    public Sprite map2;
    public Sprite map3;
    public Sprite map4;
    public Button loadButton;
    private bool isSave = false;
    private LevelLoader levelLoader;

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
        updateCurrentCharacterLVUP(gameManager.SelectedCharacterLevelup);
        ManageStatPoint();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    void Update()
    {
        checkSave();
        if (!isSave && loadButton)
        {
            loadButton.interactable = false;
        }
        else if (isSave && loadButton)
        {
            loadButton.interactable = true;
        }
        if (currentMoneyText != null)
        {
            currentMoneyText.text = gameManager.CurrentMoney().ToString();
        }
        ManageStatPoint();
    }

    // Update is called once per frame

    private void ManagementPlay()
    {
        bool ownPlayer1 = gameManager.GetSelectedCharacter(1).IsOwn;
        bool ownPlayer2 = gameManager.GetSelectedCharacter(2).IsOwn;
        bool ownShirt1 = gameManager.GetSelectedShirt(1).IsOwn;
        bool ownShirt2 = gameManager.GetSelectedShirt(2).IsOwn;
        switch (GameManager.instance.currentGameMode)
        {
            case GameManager.GameMode.OneVsOne:

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

                if (ownPlayer1 && ownShirt1)
                {
                    nextButtonAI.interactable = true;
                    nextButtonImageAI.sprite = nextEnableButtonImage;
                }
                else
                {
                    nextButtonAI.interactable = false;
                    nextButtonImageAI.sprite = nextDiableButtonImage;
                }
                break;
            default:
                break;
        }
    }

    public void NewGame()
    {
        if (isSave)
        {
            audioPlayerController.playButtonClickClip();
            warningMenu.SetActive(true);
        }
        else
        {
            gameManager.ResetSaveFile();
            ResetUI();
            audioPlayerController.playButtonClickClip();
            selecteModeMenu.SetActive(true);
            OneVsOneMenu.SetActive(false);
            OneVsAIMenu.SetActive(false);
            levelUpMenu.SetActive(false);
            mainMenu.SetActive(false);
        }
    }

    public void chooseYesWarningMenu()
    {
        gameManager.ResetSaveFile();
        ResetUI();
        audioPlayerController.playButtonClickClip();
        warningMenu.SetActive(false);
        selecteModeMenu.SetActive(true);
        OneVsOneMenu.SetActive(false);
        OneVsAIMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void chooseNoWarningMenu()
    {
        warningMenu.SetActive(false);
    }

    public void LoadGame()
    {
        gameManager.LoadGame();
        audioPlayerController.playButtonClickClip();
        selecteModeMenu.SetActive(true);
        OneVsOneMenu.SetActive(false);
        OneVsAIMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void goToSetting()
    {
        audioPlayerController.playButtonClickClip();
        settingMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void checkSave()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveFile.json");
        if (File.Exists(path))
        {
            isSave = true;
        }
        else
        {
            isSave = false;
        }
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

    public void BackFromSettingMenu()
    {
        audioPlayerController.playButtonClickClip();
        settingMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void BackFromChooseLevelMenu()
    {
        audioPlayerController.playButtonClickClip();
        chooseLevelMenu.SetActive(false);

        switch (GameManager.instance.currentGameMode)
        {
            case GameManager.GameMode.OneVsOne: //OneVsOne
                OneVsOneMenu.SetActive(true);
                break;
            case GameManager.GameMode.OneVsAI: //OneVsAI
                OneVsAIMenu.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void ToChooseLevelMenu()
    {
        audioPlayerController.playButtonClickClip();
        chooseLevelMenu.SetActive(true);
        switch (GameManager.instance.currentGameMode)
        {
            case GameManager.GameMode.OneVsOne: //OneVsOne
                OneVsOneMenu.SetActive(false);
                break;
            case GameManager.GameMode.OneVsAI: //OneVsAI
                OneVsAIMenu.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LevelUpPlayer()
    {
        audioPlayerController.playButtonClickClip();
        selecteModeMenu.SetActive(false);

        levelUpMenu.SetActive(true);
    }

    public void OneVsOne()
    {
        audioPlayerController.playButtonClickClip();
        selecteModeMenu.SetActive(false);
        OneVsOneMenu.SetActive(true);
        GameManager.instance.currentGameMode = GameManager.GameMode.OneVsOne;
        currentCharacter1Panel.transform.SetParent(OneVsOneMenu.transform);
        currentCharacter1Panel.transform.localPosition = new Vector3(-400, 240, 0);

        MoneyPanel.transform.SetParent(OneVsOneMenu.transform);
    }

    public void OneVsAI()
    {
        audioPlayerController.playButtonClickClip();
        GameManager.instance.currentGameMode = GameManager.GameMode.OneVsAI;
        selecteModeMenu.SetActive(false);
        OneVsAIMenu.SetActive(true);
        currentCharacter1Panel.transform.SetParent(OneVsAIMenu.transform);
        currentCharacter1Panel.transform.localPosition = new Vector3(0, 240, 0);
        MoneyPanel.transform.SetParent(OneVsAIMenu.transform);
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

    public void goToLeftCharacterLVUP()
    {
        audioPlayerController.playButtonClickClip();

        gameManager.changeLeftCharacterLevelUp();
        updateCurrentCharacterLVUP(gameManager.SelectedCharacterLevelup);
    }

    public void goToRightCharacterLVUP()
    {
        audioPlayerController.playButtonClickClip();

        gameManager.changeRightCharacterLevelUp();
        updateCurrentCharacterLVUP(gameManager.SelectedCharacterLevelup);
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

    public void updateCurrentCharacterLVUP(Character character)
    {
        currentCharacterLVImage.sprite = character.Image;

        currentShootText.text = character.ShootStat.ToString();
        currentSpeedText.text = character.SpeedStat.ToString();
        currentJumpText.text = character.JumpStat.ToString();
        currentLVText.text = "LV " + character.Level.ToString();
        currentCDSkillLVText.text = character.CoolDownTime.ToString();
        currentStatPointLVText.text = "UPGRADE POINTS LEFT: " + character.StatToUpgrade.ToString();
    }

    public void TurnFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeResolution(int resolutionIndex)
    {
        switch (resolutionIndex)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;

            default:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
        }
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
            currentCDSkillLV1Text.text = character.CoolDownTime.ToString();

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
            currentCDSkillLV2Text.text = character.CoolDownTime.ToString();
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

    public void UpgradeStat(int index)
    {
        // 0 : Shoot, 1 : Speed, 2 : Jump, 3 : CoolDownTime
        if (gameManager.SelectedCharacterLevelup.StatToUpgrade > 0)
        {
            gameManager.ModifyStatPoint(-1);
            gameManager.ModifyStat(index, 0.5f);
            currentStatPointLVText.text =
                "UPGRADE POINTS LEFT: "
                + gameManager.SelectedCharacterLevelup.StatToUpgrade.ToString();
            switch (index)
            {
                case 0:

                    currentShoot1Text.text = gameManager.SelectedCharacter1.ShootStat.ToString();
                    currentShoot2Text.text = gameManager.SelectedCharacter2.ShootStat.ToString();
                    currentShootText.text =
                        gameManager.SelectedCharacterLevelup.ShootStat.ToString();
                    break;
                case 1:
                    currentSpeed1Text.text = gameManager.SelectedCharacter1.SpeedStat.ToString();
                    currentSpeed2Text.text = gameManager.SelectedCharacter2.SpeedStat.ToString();
                    currentSpeedText.text =
                        gameManager.SelectedCharacterLevelup.SpeedStat.ToString();
                    break;
                case 2:
                    currentJump1Text.text = gameManager.SelectedCharacter1.JumpStat.ToString();
                    currentJump2Text.text = gameManager.SelectedCharacter2.JumpStat.ToString();
                    currentJumpText.text = gameManager.SelectedCharacterLevelup.JumpStat.ToString();
                    break;
                case 3:
                    currentCDSkillLV1Text.text =
                        gameManager.SelectedCharacter1.CoolDownTime.ToString();
                    currentCDSkillLV2Text.text =
                        gameManager.SelectedCharacter2.CoolDownTime.ToString();
                    currentCDSkillLVText.text =
                        gameManager.SelectedCharacterLevelup.CoolDownTime.ToString();
                    break;
                default:
                    break;
            }
        }
    }

    public void DowngradeStat(int index)
    {
        switch (index)
        {
            case 0:
                if (
                    gameManager.SelectedCharacterLevelup.ShootStat
                    > gameManager.SelectedCharacterLevelup.ShootInit
                )
                {
                    gameManager.ModifyStatPoint(1);
                    gameManager.ModifyStat(index, -0.5f);
                    currentStatPointLVText.text =
                        "UPGRADE POINTS LEFT: "
                        + gameManager.SelectedCharacterLevelup.StatToUpgrade.ToString();
                    currentShoot1Text.text = gameManager.SelectedCharacter1.ShootStat.ToString();
                    currentShoot2Text.text = gameManager.SelectedCharacter2.ShootStat.ToString();
                    currentShootText.text =
                        gameManager.SelectedCharacterLevelup.ShootStat.ToString();
                }
                break;
            case 1:
                if (
                    gameManager.SelectedCharacterLevelup.SpeedStat
                    > gameManager.SelectedCharacterLevelup.SpeedInit
                )
                {
                    gameManager.ModifyStatPoint(1);
                    gameManager.ModifyStat(index, -0.5f);
                    currentStatPointLVText.text =
                        "UPGRADE POINTS LEFT: "
                        + gameManager.SelectedCharacterLevelup.StatToUpgrade.ToString();
                    currentSpeed1Text.text = gameManager.SelectedCharacter1.SpeedStat.ToString();
                    currentSpeed2Text.text = gameManager.SelectedCharacter2.SpeedStat.ToString();
                    currentSpeedText.text =
                        gameManager.SelectedCharacterLevelup.SpeedStat.ToString();
                }
                break;
            case 2:
                if (
                    gameManager.SelectedCharacterLevelup.JumpStat
                    > gameManager.SelectedCharacterLevelup.JumpInit
                )
                {
                    gameManager.ModifyStatPoint(1);
                    gameManager.ModifyStat(index, -0.5f);
                    currentStatPointLVText.text =
                        "UPGRADE POINTS LEFT: "
                        + gameManager.SelectedCharacterLevelup.StatToUpgrade.ToString();
                    currentJump1Text.text = gameManager.SelectedCharacter1.JumpStat.ToString();
                    currentJump2Text.text = gameManager.SelectedCharacter2.JumpStat.ToString();
                    currentJumpText.text = gameManager.SelectedCharacterLevelup.JumpStat.ToString();
                }
                break;
            case 3:
                if (
                    gameManager.SelectedCharacterLevelup.CoolDownTime
                    < gameManager.SelectedCharacterLevelup.CoolDownTimeInit
                )
                {
                    gameManager.ModifyStatPoint(1);
                    gameManager.ModifyStat(index, -0.5f);
                    currentStatPointLVText.text =
                        "UPGRADE POINTS LEFT: "
                        + gameManager.SelectedCharacterLevelup.StatToUpgrade.ToString();
                    currentCDSkillLV1Text.text =
                        gameManager.SelectedCharacter1.CoolDownTime.ToString();
                    currentCDSkillLV2Text.text =
                        gameManager.SelectedCharacter2.CoolDownTime.ToString();
                    currentCDSkillLVText.text =
                        gameManager.SelectedCharacterLevelup.CoolDownTime.ToString();
                }
                break;
            default:
                break;
        }
    }

    public void ManageStatPoint()
    {
        if (
            gameManager.SelectedCharacterLevelup.ShootStat
            != gameManager.SelectedCharacterLevelup.ShootInit
        )
        {
            currentShootText.color = Color.red;
        }
        else
        {
            currentShootText.color = Color.black;
        }

        if (
            gameManager.SelectedCharacterLevelup.SpeedStat
            != gameManager.SelectedCharacterLevelup.SpeedInit
        )
        {
            currentSpeedText.color = Color.red;
        }
        else
        {
            currentSpeedText.color = Color.black;
        }

        if (
            gameManager.SelectedCharacterLevelup.JumpStat
            != gameManager.SelectedCharacterLevelup.JumpInit
        )
        {
            currentJumpText.color = Color.red;
        }
        else
        {
            currentJumpText.color = Color.black;
        }

        if (
            gameManager.SelectedCharacterLevelup.CoolDownTime
            != gameManager.SelectedCharacterLevelup.CoolDownTimeInit
        )
        {
            currentCDSkillLVText.color = Color.red;
        }
        else
        {
            currentCDSkillLVText.color = Color.black;
        }
    }

    public void ChangeLevel(int level)
    {
        switch (level)
        {
            case 0:
                GameManager.instance.currentLevelPlayed = 0;
                break;
            case 1:
                GameManager.instance.currentLevelPlayed = 1;
                break;
            case 2:
                GameManager.instance.currentLevelPlayed = 2;
                break;
            case 3:
                GameManager.instance.currentLevelPlayed = 3;
                break;
            case 4:
                GameManager.instance.currentLevelPlayed = 4;
                break;
            case 5:
                GameManager.instance.currentLevelPlayed = 5;
                break;
            default:
                break;
        }
    }

    public void ChangeMap(int map)
    {
        switch (map)
        {
            case 0:
                int randomMode = UnityEngine.Random.Range(0, 4);
                switch (randomMode)
                {
                    case 0:
                        GameManager.instance.currentGameMap = GameManager.GameMap.Normal;
                        break;
                    case 1:
                        GameManager.instance.currentGameMap = GameManager.GameMap.Frozen;
                        break;
                    case 2:
                        GameManager.instance.currentGameMap = GameManager.GameMap.Fire;
                        break;
                    case 3:
                        GameManager.instance.currentGameMap = GameManager.GameMap.Rain;
                        break;

                    default:
                        break;
                }
                levelDescriptionImage.sprite = map0;
                break;
            case 1:
                GameManager.instance.currentGameMap = GameManager.GameMap.Normal;
                levelDescriptionImage.sprite = map1;
                break;
            case 2:
                GameManager.instance.currentGameMap = GameManager.GameMap.Frozen;
                levelDescriptionImage.sprite = map2;
                break;
            case 3:
                GameManager.instance.currentGameMap = GameManager.GameMap.Fire;
                levelDescriptionImage.sprite = map3;
                break;
            case 4:
                GameManager.instance.currentGameMap = GameManager.GameMap.Rain;
                levelDescriptionImage.sprite = map4;
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

                int randomMode = UnityEngine.Random.Range(0, 4);
                switch (randomMode)
                {
                    case 0:
                        GameManager.instance.currentGameRule = GameManager.GameRule.Time60;
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

                    default:
                        break;
                }
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
                levelLoader.LoadScene("LevelScene" + UnityEngine.Random.Range(1, 6).ToString());
                break;
            case 1:
                levelLoader.LoadScene("LevelScene1");

                break;
            case 2:
                levelLoader.LoadScene("LevelScene2");

                break;
            case 3:
                levelLoader.LoadScene("LevelScene3");

                break;
            case 4:
                levelLoader.LoadScene("LevelScene4");

                break;
            case 5:
                levelLoader.LoadScene("LevelScene5");
                break;
            default:
                break;
        }
    }

    private void ResetUI()
    {
        updateCurrentCharacter(gameManager.GetSelectedCharacter(1), 1);
        updateCurrentCharacter(gameManager.GetSelectedCharacter(2), 2);
        updateCurrentShirt(gameManager.GetSelectedShirt(1), 1);
        updateCurrentShirt(gameManager.GetSelectedShirt(2), 2);
        updateCurrentCharacterLVUP(gameManager.SelectedCharacterLevelup);
    }
}
