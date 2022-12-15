using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Character", menuName = "Football/Character", order = 0)]
public class Character : ScriptableObject {
    

    // Start is called before the first frame update
    [SerializeField] string name;
    [SerializeField] Sprite image;
    [SerializeField] int goldToBuy;
    [SerializeField] int level;
    [SerializeField] int maxStat;
    [SerializeField] int statToUpgrade;
    [SerializeField] int speedStat;
    [SerializeField] int shootStat;
    public string Name { get => name; set => name = value; }
    public Sprite Image { get => image; set => image = value; }
    public int GoldToBuy { get => goldToBuy; }
    public int Level { get => level; set => level = value; }
    public int MaxStat { get => maxStat;}
    public int StatToUpgrade { get => statToUpgrade; set => statToUpgrade = value; }
    public int SpeedStat { get => speedStat; set => speedStat = value; }
    public int ShootStat { get => shootStat; set => shootStat = value; }
    public bool IsMaxLevel(){
        return level==maxStat;
    }
    public bool IsMaxStat(){
        return statToUpgrade==maxStat;
    }
    public bool IsEqual(Character character){
        return name==character.name;
    }

}