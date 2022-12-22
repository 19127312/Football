using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Character", menuName = "Football/Character", order = 0)]
public class Character : ScriptableObject
{


    // Start is called before the first frame update
    [SerializeField] string namePlayer;
    [SerializeField] Sprite image;
    [SerializeField] int goldToBuy;
    [SerializeField] int level;
    [SerializeField] int exp;
    [SerializeField] int expToNextLevel;
    [SerializeField] int maxStat;
    [SerializeField] int statToUpgrade;
    [SerializeField] int speedStat;
    [SerializeField] int shootStat;
    [SerializeField] int jumpStat;
    [SerializeField] bool isOwn;
    [SerializeField] GameObject skillPrefab;

    public string Name { get => name; set => name = value; }
    public Sprite Image { get => image; set => image = value; }
    public int GoldToBuy { get => goldToBuy; }
    public int Level { get => level; set => level = value; }
    public int MaxStat { get => maxStat; }
    public int StatToUpgrade { get => statToUpgrade; set => statToUpgrade = value; }
    public int SpeedStat { get => speedStat; set => speedStat = value; }
    public int ShootStat { get => shootStat; set => shootStat = value; }
    public int JumpStat { get => jumpStat; set => jumpStat = value; }
    public int Exp { get => exp; set => exp = value; }
    public int ExpToNextLevel { get => expToNextLevel; set => expToNextLevel = value; }
    public bool IsOwn { get => isOwn; set => isOwn = value; }

    public GameObject SkillPrefab { get => skillPrefab; }
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
