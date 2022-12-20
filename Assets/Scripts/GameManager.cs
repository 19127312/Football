using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum GameMode
    {
        OneVsOne,
        OneVsAI,
    }
    public enum GameRule
    {
        Random,
        Time30,
        Time60,
        Score7,
        Score9,
    }
    public static GameManager instance;
    public List<Character> charactersInGame = new List<Character>();

    private Character selectedCharacter1;
    private Character selectedCharacter2;
    public int currentLevelPlayed = 0;
    public Character SelectedCharacter1 { get => selectedCharacter1; }
    public Character SelectedCharacter2 { get => selectedCharacter2; }

    private int currentMoney = 1000;
    public GameMode currentGameMode = GameMode.OneVsOne;
    public GameRule currentGameRule = GameRule.Score7;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {

        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        selectedCharacter1 = charactersInGame[0];
        selectedCharacter2 = charactersInGame[0];
    }

    public int CurrentMoney()
    {
        return currentMoney;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeLeftCharacter(int playerNumber)
    {
        int index = 0;
        if (playerNumber == 1)
        {
            index = charactersInGame.IndexOf(selectedCharacter1);
        }
        else
        {
            index = charactersInGame.IndexOf(selectedCharacter2);

        }
        if (index == 0)
        {
            index = charactersInGame.Count - 1;
        }
        else
        {
            index--;
        }

        if (playerNumber == 1)
        {
            selectedCharacter1 = charactersInGame[index];
        }
        else
        {
            selectedCharacter2 = charactersInGame[index];
        }
    }
    public void changeRightCharacter(int playerNumber)
    {
        int index = 0;
        if (playerNumber == 1)
        {
            index = charactersInGame.IndexOf(selectedCharacter1);
        }
        else
        {
            index = charactersInGame.IndexOf(selectedCharacter2);

        }
        if (index == charactersInGame.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        if (playerNumber == 1)
        {
            selectedCharacter1 = charactersInGame[index];
        }
        else
        {
            selectedCharacter2 = charactersInGame[index];
        }
    }
    public Character GetSelectedCharacter(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return selectedCharacter1;
        }
        else
        {
            return selectedCharacter2;
        }

    }
    public void ModifyMoney(int amount)
    {
        currentMoney += amount;
    }
    public void BuyCharacter(Character character)
    {
        character.IsOwn = true;
    }
}
