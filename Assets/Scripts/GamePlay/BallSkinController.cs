using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkinController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Ball;
    public Sprite fireSprite;
    public Sprite forstSprite;
    public Sprite normalSprite;

    void Start()
    {
        Ball = GameObject.Find("Ball");
        if (GameManager.instance.currentGameBallSkin == GameManager.GameBallSkin.Normal)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = normalSprite;
            Ball.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Ball.gameObject.transform
                .GetChild(0)
                .gameObject.transform.GetChild(0)
                .gameObject.SetActive(false);
            Ball.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            Ball.gameObject.transform
                .GetChild(1)
                .gameObject.transform.GetChild(0)
                .gameObject.SetActive(false);
        }
        if (GameManager.instance.currentGameBallSkin == GameManager.GameBallSkin.FireSkin)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = fireSprite;
            Ball.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Ball.gameObject.transform
                .GetChild(0)
                .gameObject.transform.GetChild(0)
                .gameObject.SetActive(true);
            Ball.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            Ball.gameObject.transform
                .GetChild(1)
                .gameObject.transform.GetChild(0)
                .gameObject.SetActive(false);
        }
        if (GameManager.instance.currentGameBallSkin == GameManager.GameBallSkin.ForstSkin)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = forstSprite;
            Ball.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Ball.gameObject.transform
                .GetChild(0)
                .gameObject.transform.GetChild(0)
                .gameObject.SetActive(false);
            Ball.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            Ball.gameObject.transform
                .GetChild(1)
                .gameObject.transform.GetChild(0)
                .gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() { }
}
