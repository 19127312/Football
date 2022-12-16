using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isMuteSound = false;
    public string audioState = "Music";

    [Header("Portal")]
    [SerializeField] AudioClip portalClip;
    [SerializeField][Range(0f, 1f)] float portalVolumn = 1f;

    // character movemonet audio
    [Header("Kick")]
    [SerializeField] AudioClip kickClip;
    [SerializeField][Range(0f, 1f)] float kickVolumn = 1f;

    [Header("Jump")]
    [SerializeField] AudioClip jumpClip;
    [SerializeField][Range(0f, 1f)] float jumpVolumn = 1f;

    // goal size audio
    [Header("Goal Grow")]
    [SerializeField] AudioClip goalGrowClip;
    [SerializeField][Range(0f, 1f)] float goalGrowVolumn = 1f;

    [Header("Goal Shrink")]
    [SerializeField] AudioClip goalShrinkClip;
    [SerializeField][Range(0f, 1f)] float goalShrinkVolumn = 1f;

    // ball to audio
    [Header("Cross Bar Hit ")]
    [SerializeField] AudioClip crossBarHitClip;
    [SerializeField][Range(0f, 1f)] float crossBarHitVolumn = 1f;

    [Header("Score")]
    [SerializeField] AudioClip scoreClip;
    [SerializeField][Range(0f, 1f)] float scoreVolumn = 1f;

    [Header("Ball Kick")]
    [SerializeField] AudioClip ballKickClip;
    [SerializeField][Range(0f, 1f)] float ballKickVolumn = 1f;

    // ball size audio

    [Header("Large Ball")]
    [SerializeField] AudioClip largeBallClip;
    [SerializeField][Range(0f, 1f)] float largeBallVolumn = 1f;

    [Header("Small Ball")]
    [SerializeField] AudioClip smallBallClip;
    [SerializeField][Range(0f, 1f)] float smallBallVolumn = 1f;

    // effect on player

    [Header("Ice")]
    [SerializeField] AudioClip iceClip;
    [SerializeField][Range(0f, 1f)] float iceVolumn = 1f;



    static AudioPlayerController instance;

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

    private void playClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            if (!isMuteSound)
            {
                AudioSource.PlayClipAtPoint(clip,
                                        Camera.main.transform.position,
                                        volume);
            }

        }
    }

    public void playPortalClip()
    {
        playClip(portalClip, portalVolumn);
    }

    public void playKickClip()
    {
        playClip(kickClip, kickVolumn);
    }

    public void playJumpClip()
    {
        playClip(jumpClip, jumpVolumn);
    }

    public void playGoalGrowClip()
    {
        playClip(goalGrowClip, goalGrowVolumn);
    }

    public void playGoalShrinkClip()
    {
        playClip(goalShrinkClip, goalShrinkVolumn);
    }

    public void playCrossBarHitClip()
    {
        playClip(crossBarHitClip, crossBarHitVolumn);
    }

    public void playScoreClip()
    {
        playClip(scoreClip, scoreVolumn);
    }

    public void playBallKickClip()
    {
        playClip(ballKickClip, ballKickVolumn);
    }

    public void playLargeBallClip()
    {
        playClip(largeBallClip, largeBallVolumn);
    }

    public void playSmallBallClip()
    {
        playClip(smallBallClip, smallBallVolumn);
    }

    public void playIceClip()
    {
        playClip(iceClip, iceVolumn);
    }

    public void Mute()
    {
        audioSource.mute = true;
        isMuteSound = true;
        audioState = "Mute";
    }
    public void UnMute()
    {
        audioSource.mute = false;
        isMuteSound = false;
        audioState = "Music";
    }
    public void playSoundOnly()
    {
        isMuteSound = false;
        audioSource.mute = true;
        audioState = "Sound";
    }
}


