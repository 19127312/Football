using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    public float speed = 5f;

    public float timeunteleportable = 0.5f;
    bool isUnteleportable;
    float teleportableTimer;

    AudioPlayerController audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = UnityEngine.Input.GetAxis("Horizontal");
        vertical = UnityEngine.Input.GetAxis("Vertical");
        
        if (isUnteleportable)
        {
            teleportableTimer -= Time.deltaTime;
            if (teleportableTimer < 0)
            {
                isUnteleportable = false;
            }
        }
        
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += (speed * horizontal * Time.deltaTime);
        position.y += (speed * vertical * Time.deltaTime);
        rb2d.MovePosition(position);
    }

    public void Tele(Vector2 portPosition){
        if (isUnteleportable)
            return;
        isUnteleportable = true;
        teleportableTimer = timeunteleportable;

        transform.position = portPosition;
        audioPlayer.playPortalClip();
    }

    void Awake(){
        audioPlayer = FindObjectOfType<AudioPlayerController>();
    }
}
