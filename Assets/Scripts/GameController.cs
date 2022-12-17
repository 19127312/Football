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
    public int goalLeftCount=0, goalRightCount=0, timeMatch=30;
    public bool isScored=false, endMatch=false;
    GameObject ball, AI, LPlayer, RPlayer;
    public bool Pve;
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
            ball.transform.position = new Vector3(0, 0, 0);
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            if (Pve)
            {
                AI.transform.position = new Vector3(4f, 0, 0);
            }
            else
            {
                RPlayer.transform.position = new Vector3(4f, 0, 0);
            }

            LPlayer.transform.position = new Vector3(-4f, 0, 0);
        }
    }
}
