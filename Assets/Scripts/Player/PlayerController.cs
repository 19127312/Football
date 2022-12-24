using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    public float speed = 5f;
    public float jumpForce = 10f;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool canShoot;
    private GameObject ball;
    public Animator anim;
    public int ShootForce;
    Vector2 rawInput;
    public GameObject shootEffect;

    public GameObject freezeEffect;
    public GameObject rocketEffect;
    public GameObject speedLightEffect;
    public GameObject bubbleGumEffect;
    public GameObject blackLightningEffect;
    public GameObject brokenLegEffect;
    public GameObject Skill;
    public bool isFreezed = false;
    [SerializeField] bool isLeftPlayer;
    [SerializeField] GameObject head;
    [SerializeField] GameObject shirt;
    [SerializeField] GameObject skill;


    private GameManager gameManager;


    AudioPlayerController audioPlayer;

    // Start is called before the first frame update

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        audioPlayer = FindObjectOfType<AudioPlayerController>();
        isGrounded = true;
        canShoot = false;
        if (isLeftPlayer)
        {
            head.GetComponent<SpriteRenderer>().sprite = gameManager.SelectedCharacter1.Image;
            shirt.GetComponent<SpriteRenderer>().sprite = gameManager.SelectedShirt1.Image;
            speed = (float)gameManager.SelectedCharacter1.SpeedStat;
            jumpForce = (float)gameManager.SelectedCharacter1.JumpStat;
            ShootForce = (int)gameManager.SelectedCharacter1.ShootStat;
            skill = gameManager.SelectedCharacter1.SkillPrefab;
        }
        else
        {
            head.GetComponent<SpriteRenderer>().sprite = gameManager.SelectedCharacter2.Image;
            shirt.GetComponent<SpriteRenderer>().sprite = gameManager.SelectedShirt2.Image;
            speed = (float)gameManager.SelectedCharacter2.SpeedStat;
            jumpForce = (float)gameManager.SelectedCharacter2.JumpStat;
            ShootForce = -(int)gameManager.SelectedCharacter2.ShootStat;
            skill = gameManager.SelectedCharacter2.SkillPrefab;

        }
        if (GameManager.instance.currentGameMode == GameManager.GameMode.OneVsAI)
        {
            if (!isLeftPlayer)
            {
                gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update() { }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        anim.SetFloat("Speed", rawInput.magnitude);
    }

    void OnShoot(InputValue value)
    {
        if (!isFreezed)
        {
            anim.SetTrigger("Kick");
        }

    }

    void OnSkill(InputValue value)
    {
        // skill.run();
        skill.GetComponent<Skills>().UseSkill();
    }

    private void FixedUpdate()
    {
        if (!GameController.instance.endMatch && !GameController.instance.isScored)
        {
            movePlayer();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void movePlayer()
    {
        if (rawInput.x < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        if (rawInput.x > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (rawInput.y > 0)
        {
            Jump();
        }
    }

    public void Shoot()
    {
        if (canShoot && !GameController.instance.endMatch && !GameController.instance.isScored)
        {
            audioPlayer.playKickClip();
            shootEffect.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(ShootForce, 500));
            StartCoroutine(ExecuteAfterTime(0.1f));
        }
    }

    public void Jump()
    {
        if (isGrounded && !GameController.instance.endMatch && !GameController.instance.isScored)
        {
            audioPlayer.playJumpClip();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        shootEffect.SetActive(false);
    }
}
