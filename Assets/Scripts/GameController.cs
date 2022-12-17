using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public TMP_Text goalLeft, goalRight, timeMatchText;
    public int goalLeftCount = 0, goalRightCount = 0, timeMatch = 30;
    public bool isScored = false, endMatch = false;
    GameObject ball, AI, LPlayer, RPlayer;
    public bool Pve;
    public AudioPlayerController audioPlayer;
    public GameObject panel;
    int restartTime;
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
        ball = GameObject.FindGameObjectWithTag("Ball");
        LPlayer = GameObject.FindGameObjectWithTag("LeftPlayer");
        audioPlayer = FindObjectOfType<AudioPlayerController>();
        restartTime = timeMatch;
        if (Pve)
        {
            AI = GameObject.FindGameObjectWithTag("AI");
        }
        else
        {
            RPlayer = GameObject.FindGameObjectWithTag("RightPlayer");
        }

        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        goalLeft.text = goalLeftCount.ToString();
        goalRight.text = goalRightCount.ToString();
        timeMatchText.text = timeMatch.ToString();

    }

    IEnumerator CountDown()
    {
        while (timeMatch > 0)
        {
            yield return new WaitForSeconds(1);
            timeMatch--;
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
                TMP_Text result = panel.transform.Find("Result").GetComponent<TMP_Text>();
                TMP_Text Lscore = panel.transform.Find("ScoreLeft").GetComponent<TMP_Text>();
                TMP_Text Rscore = panel.transform.Find("ScoreRight").GetComponent<TMP_Text>();
                Image iconWinner = panel.transform.Find("IconWinner").GetComponent<Image>();
                Button btnRestart = panel.transform.Find("RestartButton").GetComponent<Button>();
                showResult(result, Lscore, Rscore, iconWinner);
                btnRestart.onClick.AddListener(restartGame);
                //StopCoroutine(CountDown());
            }
        }
    }

    void restartGame()
    {
        goalLeftCount = 0;
        goalRightCount = 0;
        timeMatch = restartTime;
        endMatch = false;
        isScored = false;
        if (panel)
        {
            panel.SetActive(false);
        }
        resetGameObject();
        StartCoroutine(CountDown());
    }

    void showResult(TMP_Text result, TMP_Text Lscore, TMP_Text Rscore, Image iconWinner)
    {
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Lscore.text = goalLeftCount.ToString();
        Rscore.text = goalRightCount.ToString();
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
                result.text = "Draw";
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
