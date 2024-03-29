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
    [Header("Ball Head")]
    [SerializeField] AudioClip HeadClip;
    [SerializeField][Range(0f, 1f)] float headVolumn = 1f;
    [Header("Ball Hit")]
    [SerializeField][Range(0f, 2f)] float ballHitVolumn = 1f;
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


    [Header("Button Click")]
    [SerializeField] AudioClip buttonClickClip;
    [SerializeField][Range(0f, 1f)] float buttonClickVolumn = 1f;


    [Header("High Jump")]
    [SerializeField] AudioClip highJumpClip;
    [SerializeField][Range(0f, 1f)] float highJumpVolumn = 1f;

    [Header("Jump Disable")]
    [SerializeField] AudioClip jumpDisableClip;
    [SerializeField][Range(0f, 1f)] float jumpDisableVolumn = 1f;


    [Header("Speed")]
    [SerializeField] AudioClip speedClip;
    [SerializeField][Range(0f, 1f)] float speedVolumn = 1f;

    [Header("Slow Speed")]
    [SerializeField] AudioClip slowSpeedClip;
    [SerializeField][Range(0f, 1f)] float slowSpeedVolumn = 1f;

    [Header("Injure")]
    [SerializeField] AudioClip injureClip;
    [SerializeField][Range(0f, 1f)] float injureVolumn = 1f;

    [Header("Invicible")]
    [SerializeField] AudioClip invicibleClip;
    [SerializeField][Range(0f, 1f)] float invicibleVolumn = 1f;

    [Header("Match Lose")]
    [SerializeField] AudioClip matchLoseClip;
    [SerializeField][Range(0f, 1f)] float matchLoseVolumn = 1f;

    [Header("Match Win")]
    [SerializeField] AudioClip matchWinClip;
    [SerializeField][Range(0f, 1f)] float matchWinVolumn = 1f;

    [Header("Match Draw")]
    [SerializeField] AudioClip matchDrawClip;
    [SerializeField][Range(0f, 1f)] float matchDrawVolumn = 1f;

    [Header("Button Click")]
    [SerializeField] AudioClip buyClip;
    [SerializeField][Range(0f, 1f)] float buyClipVolumn = 1f;

    [Header("Skill Exchange")]
    [SerializeField] AudioClip skillExchangeClip;
    [SerializeField][Range(0f, 1f)] float skillExchangeClipVolumn = 1f;

    [Header("Skill Defence")]
    [SerializeField] AudioClip skillDefenceClip;
    [SerializeField][Range(0f, 1f)] float skillDefenceClipVolumn = 1f;
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


    public void playHighJumpClip()
    {
        playClip(highJumpClip, highJumpVolumn);
    }

    public void playJumpDisableClip()
    {
        playClip(jumpDisableClip, jumpDisableVolumn);
    }

    public void playSpeedClip()
    {
        playClip(speedClip, speedVolumn);
    }

    public void playSlowSpeedClip()
    {
        playClip(slowSpeedClip, slowSpeedVolumn);
    }

    public void playInjureClip()
    {
        playClip(injureClip, injureVolumn);
    }

    public void playInvicibleClip()
    {
        playClip(invicibleClip, invicibleVolumn);
    }

    public void playButtonClickClip()
    {
        playClip(buttonClickClip, buttonClickVolumn);
    }

    public void playMatchLoseClip()
    {
        playClip(matchLoseClip, matchLoseVolumn);
    }

    public void playMatchWinClip()
    {
        playClip(matchWinClip, matchWinVolumn);
    }

    public void playMatchDrawClip()
    {
        playClip(matchDrawClip, matchDrawVolumn);
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
    public void playBuyClip()
    {
        playClip(buyClip, buyClipVolumn);
    }

    public void playHeadClip()
    {
        playClip(HeadClip, headVolumn);
    }
    public void playBallHit()
    {
        playClip(HeadClip, ballHitVolumn);
    }

    public void playSkillExchange()
    {
        playClip(skillExchangeClip, skillExchangeClipVolumn);
    }

    public void playSkillDefence()
    {
        playClip(skillDefenceClip, skillDefenceClipVolumn);
    }
}


