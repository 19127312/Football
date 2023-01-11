using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    public enum GameMap
    {
        Normal,
        Frozen,
        Rain,
        Fire,
    }

    public static GameManager instance;
    public List<Character> charactersInGame = new List<Character>();
    public List<Shirt> shirtInGame = new List<Shirt>();

    private Character selectedCharacter1;
    private Shirt selectedShirt1;
    private Shirt selectedShirt2;

    private Character selectedCharacter2;
    private Character selectedCharacterLevelup;
    public int currentLevelPlayed = 0;
    public Character SelectedCharacter1
    {
        get => selectedCharacter1;
    }
    public Character SelectedCharacter2
    {
        get => selectedCharacter2;
    }
    public Character SelectedCharacterLevelup
    {
        get => selectedCharacterLevelup;
    }
    public Shirt SelectedShirt1
    {
        get => selectedShirt1;
    }
    public Shirt SelectedShirt2
    {
        get => selectedShirt2;
    }
    private int currentMoney = 0;
    public GameMode currentGameMode = GameMode.OneVsOne;
    public GameRule currentGameRule = GameRule.Score7;
    public GameMap currentGameMap = GameMap.Normal;
    public LevelLoader levelLoader;

    private GameObject warningPanel;
    private TMP_Text saveText;

    private void Awake()
    {
        ManageSingleton();
        selectedCharacterLevelup = charactersInGame[0];
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
    void Update() { }

    public void changeLeftCharacterLevelUp()
    {
        List<Character> charactersBuyed = new List<Character>();
        foreach (Character item in charactersInGame)
        {
            if (item.IsOwn)
            {
                charactersBuyed.Add(item);
            }
        }
        int index = charactersBuyed.IndexOf(selectedCharacterLevelup);
        if (index == 0)
        {
            index = charactersBuyed.Count - 1;
        }
        else
        {
            index--;
        }
        selectedCharacterLevelup = charactersBuyed[index];
    }

    public void changeRightCharacterLevelUp()
    {
        List<Character> charactersBuyed = new List<Character>();
        foreach (Character item in charactersInGame)
        {
            if (item.IsOwn)
            {
                charactersBuyed.Add(item);
            }
        }
        int index = charactersBuyed.IndexOf(selectedCharacterLevelup);
        if (index == charactersBuyed.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        selectedCharacterLevelup = charactersBuyed[index];
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

    public void SaveGame(TMP_Text text, GameObject panel)
    {
        string path = Path.Combine(Application.persistentDataPath, "saveFile.json");
        if (File.Exists(path))
        {
            panel.SetActive(true);
            warningPanel = panel;
            if (text)
            {
                saveText = text;
            }
        }
        else
        {
            FileStream file = File.Create(path);
            GameData gameData = new GameData(charactersInGame, shirtInGame, currentMoney);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, gameData);
            file.Close();
            if (text)
            {
                text.enabled = true;
                StartCoroutine(WaitSaveGame(text));
            }
        }
    }

    public void ChooseYesSaveGame()
    {
        Debug.Log("Yes");
        warningPanel.SetActive(false);
        string path = Path.Combine(Application.persistentDataPath, "saveFile.json");
        FileStream file = File.Create(path);
        GameData gameData = new GameData(charactersInGame, shirtInGame, currentMoney);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, gameData);
        file.Close();
        if (saveText)
        {
            saveText.enabled = true;
            StartCoroutine(WaitSaveGame(saveText));
        }
    }

    public void ChooseNoSaveGame()
    {
        Debug.Log("No");
        warningPanel.SetActive(false);
    }

    IEnumerator WaitSaveGame(TMP_Text text)
    {
        yield return new WaitForSeconds(2);
        if (text)
            text.enabled = false;
    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveFile.json");
        if (File.Exists(path))
        {
            Debug.Log(path);
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

    public void ResetSaveFile()
    {
        charactersInGame.ForEach(y =>
        {
            if (y.Name == "Fox")
            {
                y.Level = 1;
                y.CurrentExp = 0;
                y.StatToUpgrade = 0;
                y.SpeedStat = 6;
                y.ShootStat = 291;
                y.JumpStat = 8.5f;
                y.IsOwn = true;
            }
            if (y.Name == "Cow")
            {
                y.Level = 1;
                y.CurrentExp = 0;
                y.StatToUpgrade = 0;
                y.SpeedStat = 5;
                y.ShootStat = 300.5f;
                y.JumpStat = 10;
                y.IsOwn = false;
            }
            if (y.Name == "Lion")
            {
                y.Level = 1;
                y.CurrentExp = 0;
                y.StatToUpgrade = 0;
                y.SpeedStat = 7;
                y.ShootStat = 250;
                y.JumpStat = 5;
                y.IsOwn = false;
            }
            if (y.Name == "Zebra")
            {
                y.Level = 1;
                y.CurrentExp = 0;
                y.StatToUpgrade = 0;
                y.SpeedStat = 6;
                y.ShootStat = 350;
                y.JumpStat = 7;
                y.IsOwn = false;
            }
        });
        shirtInGame.ForEach(y =>
        {
            if (y.Name == "Shirt 1")
            {
                y.IsOwn = true;
            }
            else
            {
                y.IsOwn = false;
            }
        });
        currentMoney = 0;
    }

    public void ModifyStatPoint(int amount)
    {
        selectedCharacterLevelup.StatToUpgrade += amount;
    }

    public void ModifyStat(int index, float amount)
    {
        switch (index)
        {
            case 0:
                selectedCharacterLevelup.ShootStat += amount;
                break;
            case 1:
                selectedCharacterLevelup.SpeedStat += amount;
                break;
            case 2:
                selectedCharacterLevelup.JumpStat += amount;
                break;
            case 3:
                selectedCharacterLevelup.CoolDownTime -= amount;
                break;
            default:
                break;
        }
    }
}
