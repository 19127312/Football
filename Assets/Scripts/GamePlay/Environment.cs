using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject Camera;
    private FrostEffect forstScript;
    private RealisticRainDrop rainScript;
    private GameManager gameManager;

    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        forstScript = Camera.GetComponent<FrostEffect>();
        rainScript = Camera.GetComponent<RealisticRainDrop>();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.currentGameMap == GameManager.GameMap.Frozen)
        {
            forstScript.enabled = true;
            forstScript.FrostAmount = 0;
        }
        if (gameManager.currentGameMap == GameManager.GameMap.Rain)
        {
            rainScript.enabled = true;
            rainScript.blurSpreadSize = 0;
            rainScript.intensity = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentGameMap == GameManager.GameMap.Frozen)
        {
            updateFrost();
        }
        if (gameManager.currentGameMap == GameManager.GameMap.Rain)
        {
            updateRain();
        }
    }

    private void updateRain()
    {
        // 0 -> 0.5 in 30 secs
        if (rainScript.blurSpreadSize < 1.5f)
        {
            rainScript.blurSpreadSize += 0.000075f;
        }

        if (rainScript.intensity < 4.0f)
        {
            rainScript.intensity += 0.0004f;
        }
    }

    private void updateFrost()
    {
        if (forstScript.FrostAmount < 0.3f)
        {
            forstScript.FrostAmount += 0.00003f;
        }
    }
}
