using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterData
{
    public string namePlayer;
    public int level;
    public float exp;
    public int statToUpgrade;
    public int speedStat;
    public int shootStat;
    public int jumpStat;
    public bool isOwn;
    public float coolDownTime;

    public CharacterData(Character character)
    {
        namePlayer = character.Name;
        level = character.Level;
        exp = character.CurrentExp;
        statToUpgrade = character.StatToUpgrade;
        speedStat = character.SpeedStat;
        shootStat = character.ShootStat;
        jumpStat = character.JumpStat;
        isOwn = character.IsOwn;
        coolDownTime = character.CoolDownTime;
    }

}

[Serializable]
public class ShirtData
{
    public bool isOwn;

    public string shirtName;
    public ShirtData(Shirt shirt)
    {
        shirtName = shirt.name;
        isOwn = shirt.IsOwn;

    }
}
[Serializable]
public class GameData
{
    public List<CharacterData> charactersInGame = new List<CharacterData>();
    public List<ShirtData> shirtInGame = new List<ShirtData>();
    public int currentMoney;

    public GameData(List<Character> charactersInGame, List<Shirt> shirtInGame, int money)
    {
        charactersInGame.ForEach(character => this.charactersInGame.Add(new CharacterData(character)));
        shirtInGame.ForEach(shirt => this.shirtInGame.Add(new ShirtData(shirt)));
        this.currentMoney = money;
    }
}
