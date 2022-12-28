using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shirt", menuName = "Football/Shirt", order = 1)]
public class Shirt : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] Sprite image;
    [SerializeField] bool isOwn;
    [SerializeField] int goldToBuy;
    [SerializeField] string shirtName;

    public int GoldToBuy { get => goldToBuy; }
    public Sprite Image { get => image; }
    public bool IsOwn { get => isOwn; set => isOwn = value; }
    public string Name { get => shirtName; set => shirtName = value; }
    public Shirt(ShirtData data)
    {
        isOwn = data.isOwn;
        shirtName = data.shirtName;
    }
}
