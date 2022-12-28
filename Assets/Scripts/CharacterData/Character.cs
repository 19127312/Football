using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

[CreateAssetMenu(fileName = "Character", menuName = "Football/Character", order = 0)]
public class Character : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] string namePlayer;
    [SerializeField] Sprite image;
    [SerializeField] int goldToBuy;
    [SerializeField] int level;
    [SerializeField] float exp;
    [SerializeField] int maxStat;
    [SerializeField] int statToUpgrade;
    [SerializeField] float speedStat;
    [SerializeField] float speedStatInit;
    [SerializeField] float shootStat;
    [SerializeField] float shootStatInit;
    [SerializeField] float jumpStat;
    [SerializeField] float jumpStatInit;
    [SerializeField] bool isOwn;
    [SerializeField] GameObject skillPrefab;

    public string Name { get => name; set => name = value; }
    public Sprite Image { get => image; set => image = value; }
    public int GoldToBuy { get => goldToBuy; }
    public int Level { get => level; set => level = value; }
    public float CurrentExp { get => exp; set => exp = value; }
    public int MaxStat { get => maxStat; }
    public int StatToUpgrade { get => statToUpgrade; set => statToUpgrade = value; }
    public float SpeedStat { get => speedStat; set => speedStat = value; }
    public float SpeedInit { get => speedStatInit; }
    public float ShootStat { get => shootStat; set => shootStat = value; }
    public float ShootInit { get => shootStatInit; }
    public float JumpStat { get => jumpStat; set => jumpStat = value; }
    public float JumpInit { get => jumpStatInit; }
    public bool IsOwn { get => isOwn; set => isOwn = value; }
    public GameObject SkillPrefab { get => skillPrefab; }

    public string getPathImage()
    {
        return AssetDatabase.GetAssetPath(image);
    }
    public Character(CharacterData data)
    {
        name = data.namePlayer;
        level = data.level;
        exp = data.exp;
        statToUpgrade = data.statToUpgrade;
        speedStat = data.speedStat;
        shootStat = data.shootStat;
        jumpStat = data.jumpStat;
        isOwn = data.isOwn;
    }
    public bool IsMaxLevel()
    {
        return level == maxStat;
    }
    public bool IsMaxStat()
    {
        return statToUpgrade == maxStat;
    }
    public bool IsEqual(Character character)
    {
        return name == character.name;
    }
    public Sprite GetSkillSprite()
    {
        return skillPrefab.GetComponent<SpriteRenderer>().sprite;
    }

}
