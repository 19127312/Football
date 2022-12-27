using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    protected bool isLeftSkill;

    protected AudioPlayerController audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayerController>();
    }

    // Update is called once per frame
    void Update() { }

    public virtual void UseSkill(bool isLeftPlayer)
    {
        Debug.Log("Skill used");
        isLeftSkill = isLeftPlayer;
    }
}
