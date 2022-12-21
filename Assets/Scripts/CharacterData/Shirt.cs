using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shirt", menuName = "Football/Shirt", order = 1)]
public class Shirt : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] Sprite image;
    [SerializeField] bool isOwn;

    public Sprite Image { get => image;  }
    public bool IsOwn { get => isOwn; set => isOwn = value; }
}
