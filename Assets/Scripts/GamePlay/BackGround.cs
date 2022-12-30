using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject backGround;

    void Start()
    {
        backGround = GameObject.Find("BackGround");
        if (GameManager.instance.currentGameMap == GameManager.GameMap.Normal)
        {
            backGround.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            backGround.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            backGround.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (GameManager.instance.currentGameMap == GameManager.GameMap.Frozen)
        {
            backGround.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            backGround.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            backGround.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            backGround.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            backGround.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            backGround.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() { }
}
