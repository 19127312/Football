using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
        Time30,
        Time60,
        Score7,
        Score9,
    }
    public static GameManager instance;
    public List<Character> charactersInGame = new List<Character>();
    public List<Shirt> shirtInGame = new List<Shirt>();

    private Character selectedCharacter1;
    private Shirt selectedShirt1;
    private Shirt selectedShirt2;

    private Character selectedCharacter2;
    public int currentLevelPlayed = 0;
    public Character SelectedCharacter1 { get => selectedCharacter1; }
    public Character SelectedCharacter2 { get => selectedCharacter2; }
    public Shirt SelectedShirt1 { get => selectedShirt1; }
    public Shirt SelectedShirt2 { get => selectedShirt2; }
    private int currentMoney = 1000;
    public GameMode currentGameMode = GameMode.OneVsOne;
    public GameRule currentGameRule = GameRule.Score7;

    private void Awake()
    {
        ManageSingleton();
        selectedCharacter1 = charactersInGame[0];
        selectedCharacter2 = charactersInGame[0];
        selectedShirt1 = shirtInGame[0];
        selectedShirt2 = shirtInGame[0];
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
        int randomMode = UnityEngine.Random.Range(0, 4);
        switch (randomMode)
        {
            case 0:
                currentGameRule = GameManager.GameRule.Time60;
                break;
            case 1:
                currentGameRule = GameManager.GameRule.Score7;
                break;
            case 2:
                currentGameRule = GameManager.GameRule.Score9;
                break;
            case 3:
                currentGameRule = GameManager.GameRule.Time30;
                break;

            default:
                break;
        }
    }

    public int CurrentMoney()
    {
        return currentMoney;
    }

    public void addMoney(int money)
    {
        currentMoney += money;
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
    public void changeLeftShirt(int playerNumber)
    {
        int index = 0;
        if (playerNumber == 1)
        {
            index = shirtInGame.IndexOf(selectedShirt1);
        }
        else
        {
            index = shirtInGame.IndexOf(selectedShirt2);

        }
        if (index == 0)
        {
            index = shirtInGame.Count - 1;
        }
        else
        {
            index--;
        }

        if (playerNumber == 1)
        {
            selectedShirt1 = shirtInGame[index];
        }
        else
        {
            selectedShirt2 = shirtInGame[index];
        }
    }
    public void changeRightShirt(int playerNumber)
    {
        int index = 0;
        if (playerNumber == 1)
        {
            index = shirtInGame.IndexOf(selectedShirt1);
        }
        else
        {
            index = shirtInGame.IndexOf(selectedShirt2);

        }
        if (index == shirtInGame.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        if (playerNumber == 1)
        {
            selectedShirt1 = shirtInGame[index];
        }
        else
        {
            selectedShirt2 = shirtInGame[index];
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
    public Shirt GetSelectedShirt(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return selectedShirt1;
        }
        else
        {
            return selectedShirt2;
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
    public void BuyShirt(Shirt shirt)
    {
        shirt.IsOwn = true;
    }
    public void SaveGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveFile.json");
        FileStream file = File.Create(path);
        GameData gameData = new GameData(charactersInGame, shirtInGame, currentMoney);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, gameData);
        file.Close();
    }
    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveFile.json");
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            GameData gameData = (GameData)bf.Deserialize(file);
            file.Close();

            gameData.charactersInGame.ForEach(x =>
            {
                Character temp = new Character(x);
                charactersInGame.ForEach(y =>
                {
                    if (y.Name == temp.Name)
                    {
                        y.Level = temp.Level;
                        y.CurrentExp = temp.CurrentExp;
                        y.StatToUpgrade = temp.StatToUpgrade;
                        y.SpeedStat = temp.SpeedStat;
                        y.ShootStat = temp.ShootStat;
                        y.JumpStat = temp.JumpStat;
                        y.IsOwn = temp.IsOwn;
                    }
                });
            });
            gameData.shirtInGame.ForEach(x =>
            {
                Shirt temp = new Shirt(x);
                shirtInGame.ForEach(y =>
                {
                    if (y.Name == temp.Name)
                    {
                        y.IsOwn = temp.IsOwn;
                    }
                });
            });
            currentMoney = gameData.currentMoney;
        }
    }

}
