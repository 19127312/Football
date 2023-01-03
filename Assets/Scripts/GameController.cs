using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public TMP_Text goalLeft, goalRight, timeMatchText, saveText;
    public int goalLeftCount = 0, goalRightCount = 0, timeMatch = 30;
    public bool isScored = false, endMatch = false, isShowPanel = false, isAdd = true, isPaused = false;
    GameObject ball, AI, LPlayer, RPlayer;
    public bool Pve;
    public AudioPlayerController audioPlayer;
    public GameObject panel, expPanel, pausePanel;
    int restartTime;
    private GameManager gameManager;
    private Character leftPlayer, rightPlayer;
    int levelLeftPlayer, levelRightPlayer;
    int currentExpLeftPlayer, currentExpRightPlayer;
    Image TimeMatchImage;
    private LevelLoader levelLoader;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        LPlayer = GameObject.FindGameObjectWithTag("LeftPlayer");
        audioPlayer = FindObjectOfType<AudioPlayerController>();

        gameManager = FindObjectOfType<GameManager>();
        leftPlayer = gameManager.GetSelectedCharacter(1);
        rightPlayer = gameManager.GetSelectedCharacter(2);
        TimeMatchImage = GameObject.Find("TimeBorder").GetComponent<Image>();
        if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
        {
            Pve = true;
        }
        else
        {
            Pve = false;
        }
        if (Pve)
        {
            AI = GameObject.FindGameObjectWithTag("AI");
        }
        else
        {
            RPlayer = GameObject.FindGameObjectWithTag("RightPlayer");
        }

        if (gameManager.currentGameRule == GameManager.GameRule.Time30)
        {
            timeMatch = 30;
            StartCoroutine(CountDown());
        }
        else if (gameManager.currentGameRule == GameManager.GameRule.Time60)
        {
            timeMatch = 60;
            StartCoroutine(CountDown());
        }
        else if (gameManager.currentGameRule == GameManager.GameRule.Score7)
        {
            TimeMatchImage.enabled = false;
            timeMatchText.enabled = false;

        }
        else if (gameManager.currentGameRule == GameManager.GameRule.Score9)
        {
            TimeMatchImage.enabled = false;
            timeMatchText.enabled = false;
        }
        restartTime = timeMatch;
    }

    // Update is called once per frame
    void Update()
    {
        goalLeft.text = goalLeftCount.ToString();
        goalRight.text = goalRightCount.ToString();
        timeMatchText.text = timeMatch.ToString();
        if (gameManager.currentGameRule == GameManager.GameRule.Score7)
        {
            startScoreGameMode(7);

        }
        else if (gameManager.currentGameRule == GameManager.GameRule.Score9)
        {
            startScoreGameMode(9);
        }
        showPausePanel();
    }

    IEnumerator CountDown()
    {
        while (timeMatch > 0)
        {
            if (!isPaused)
            {
                yield return new WaitForSeconds(1);
                timeMatch--;
            }
            else
            {
                yield return new WaitForSeconds(0);
            }
        }
        endMatch = true;
        showPanel();
    }
    public void ContinueGame()
    {
        StartCoroutine(WaitContinueGame());
    }
    IEnumerator WaitContinueGame()
    {
        yield return new WaitForSeconds(2);
        isScored = false;
        if (!endMatch)
        {
            resetGameObject();
        }
    }
    void showPanel()
    {
        if (endMatch)
        {
            if (panel)
            {
                panel.SetActive(true);
                isShowPanel = true;
                TMP_Text result = panel.transform.Find("Result").GetComponent<TMP_Text>();
                TMP_Text Lscore = panel.transform.Find("ScoreLeft").GetComponent<TMP_Text>();
                TMP_Text Rscore = panel.transform.Find("ScoreRight").GetComponent<TMP_Text>();
                Image iconWinner = panel.transform.Find("IconWinner").GetComponent<Image>();
                Button btnNext = panel.transform.Find("NextButton").GetComponent<Button>();
                showResult(result, Lscore, Rscore, iconWinner);
                btnNext.onClick.AddListener(showExpPanel);
            }
        }
    }
    float calculateExp(int goal)
    {
        return goal * 10;
    }
    public void showExpPanel()
    {
        panel.SetActive(false);
        isShowPanel = false;
        if (expPanel)
        {
            expPanel.SetActive(true);
            ExpBar expBarLeft = expPanel.transform.Find("LeftPlayer").Find("Expbar").GetComponent<ExpBar>();
            Image iconLeft = expPanel.transform.Find("LeftPlayer").Find("Icon").GetComponent<Image>();
            TMP_Text money = expPanel.transform.Find("Money").Find("MoneyText").GetComponent<TMP_Text>();
            iconLeft.sprite = LPlayer.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
            if (isAdd)
            {
                expBarLeft.AddExp(leftPlayer, calculateExp(goalLeftCount));
                if (!Pve)
                {
                    ExpBar expBarRight = expPanel.transform.Find("RightPlayer").Find("Expbar").GetComponent<ExpBar>();
                    if (leftPlayer.IsEqual(rightPlayer))
                    {
                        expBarLeft.AddExp(leftPlayer, calculateExp(goalLeftCount + goalRightCount));
                    }
                    else
                    {
                        expBarRight.AddExp(rightPlayer, calculateExp(goalRightCount));
                    }
                    Image iconRight = expPanel.transform.Find("RightPlayer").Find("Icon").GetComponent<Image>();
                    iconRight.sprite = RPlayer.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
                    gameManager.addMoney(goalLeftCount * 10 + goalRightCount * 10);
                    money.text = (gameManager.CurrentMoney()).ToString();
                }
                else
                {
                    GameObject expBarRight = expPanel.transform.Find("RightPlayer").gameObject;
                    expBarRight.SetActive(false);
                    gameManager.addMoney(goalLeftCount * 10);
                    money.text = (gameManager.CurrentMoney()).ToString();
                }
                isAdd = false;
            }

            Button btnRestart = expPanel.transform.Find("RestartButton").GetComponent<Button>();
            Button btnSave = expPanel.transform.Find("SaveButton").GetComponent<Button>();
            Button btnHome = expPanel.transform.Find("HomeButton").GetComponent<Button>();
            saveText.enabled = false;
            btnRestart.onClick.AddListener(restartGame);
            btnSave.onClick.AddListener(SaveGame);
        }
    }
    public void SaveGame()
    {
        GameManager.instance.SaveGame(saveText);
    }
    public void showPausePanel()
    {
        if (isPaused)
        {
            pausePanel.SetActive(true);
            isShowPanel = true;
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Button btnSetting = pausePanel.transform.Find("SettingButton").GetComponent<Button>();
            Button btnHome = pausePanel.transform.Find("HomeButton").GetComponent<Button>();
            Button btnContinue = pausePanel.transform.Find("ContinueButton").GetComponent<Button>();
            btnContinue.onClick.AddListener(ContinueGameAfterpause);
        }
    }
    public void ContinueGameAfterpause()
    {
        pausePanel.SetActive(false);
        isShowPanel = false;
        isPaused = false;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
    void restartGame()
    {
        goalLeftCount = 0;
        goalRightCount = 0;
        endMatch = false;
        isScored = false;
        isAdd = true;
        timeMatch = restartTime;
        if (expPanel)
        {
            expPanel.SetActive(false);
        }
        if (panel)
        {
            panel.SetActive(false);
            isShowPanel = false;
        }
        resetGameObject();
        if (gameManager.currentGameRule == GameManager.GameRule.Time30 || gameManager.currentGameRule == GameManager.GameRule.Time60)
        {
            StopAllCoroutines();
            StartCoroutine(CountDown());

        }
        else if (gameManager.currentGameRule == GameManager.GameRule.Score7)
        {
            TimeMatchImage.enabled = false;
            timeMatchText.enabled = false;
        }
        else if (gameManager.currentGameRule == GameManager.GameRule.Score9)
        {
            TimeMatchImage.enabled = false;
            timeMatchText.enabled = false;
        }
    }
    // public void goToHomeScene()
    // {
    //     //SaveGame();
    //     StopAllCoroutines();
    //     levelLoader.LoadScene("ChooseCharacterScene");

    //     // SceneManager.LoadScene("ChooseCharacterScene");
    //     Debug.Log("Go to home scene");
    // }
    void showResult(TMP_Text result, TMP_Text Lscore, TMP_Text Rscore, Image iconWinner)
    {
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Lscore.text = goalLeftCount.ToString();
        Rscore.text = goalRightCount.ToString();
        if (gameManager.currentGameRule == GameManager.GameRule.Time30 || gameManager.currentGameRule == GameManager.GameRule.Time60)
        {
            if (Pve)
            {
                if (goalLeftCount > goalRightCount)
                {
                    audioPlayer.playMatchWinClip();
                    result.text = "You win";
                    iconWinner.GetComponent<Image>().sprite = LPlayer.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
                }
                else if (goalLeftCount < goalRightCount)
                {
                    audioPlayer.playMatchLoseClip();
                    result.text = "You lose";
                    iconWinner.GetComponent<Image>().sprite = AI.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    audioPlayer.playMatchDrawClip();
                    iconWinner.enabled = false;
                    result.text = "Draw";
                }
            }
            else
            {
                if (goalLeftCount > goalRightCount)
                {
                    audioPlayer.playMatchWinClip();
                    result.text = "Player 1 win";
                    iconWinner.GetComponent<Image>().sprite = LPlayer.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
                }
                else if (goalLeftCount < goalRightCount)
                {
                    audioPlayer.playMatchWinClip();
                    result.text = "Player 2 win";
                    iconWinner.GetComponent<Image>().sprite = RPlayer.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    audioPlayer.playMatchDrawClip();
                    iconWinner.enabled = false;
                    result.text = "Draw";
                }
            }
        }
        else
        {
            int numGoal = 0;
            if (gameManager.currentGameRule == GameManager.GameRule.Score7)
            {
                numGoal = 7;
            }
            else if (gameManager.currentGameRule == GameManager.GameRule.Score9)
            {
                numGoal = 9;
            }
            if (Pve)
            {
                if (goalLeftCount == numGoal)
                {
                    audioPlayer.playMatchWinClip();
                    result.text = "You win";
                    iconWinner.GetComponent<Image>().sprite = LPlayer.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
                }
                else if (goalRightCount == numGoal)
                {
                    audioPlayer.playMatchLoseClip();
                    result.text = "You lose";
                    iconWinner.GetComponent<Image>().sprite = AI.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
                }
            }
            else
            {
                if (goalLeftCount == numGoal)
                {
                    audioPlayer.playMatchWinClip();
                    result.text = "Player 1 win";
                    iconWinner.GetComponent<Image>().sprite = LPlayer.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
                }
                else if (goalRightCount == numGoal)
                {
                    audioPlayer.playMatchWinClip();
                    result.text = "Player 2 win";
                    iconWinner.GetComponent<Image>().sprite = RPlayer.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>().sprite;
                }
            }
        }
    }

    void startScoreGameMode(int num)
    {
        if (!endMatch)
        {
            if (goalLeftCount == num || goalRightCount == num)
            {
                endMatch = true;
                if (!isShowPanel)
                {
                    showPanel();
                }

            }
        }
    }
    void resetGameObject()
    {
        ball.transform.position = new Vector3(0, 0, 0);
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        if (Pve)
        {
            AI.transform.position = new Vector3(6f, 0, 0);
        }
        else
        {
            RPlayer.transform.position = new Vector3(6f, 0, 0);
        }
        LPlayer.transform.position = new Vector3(-6f, 0, 0);
    }
}
