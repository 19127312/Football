using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{

    public TeleportController otherPort;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallController controller = collision.GetComponent<BallController>();
        if (controller != null)
        {
            
            Vector2 position = otherPort.transform.position;
            Debug.Log(position);
            controller.Tele(position);

        }
    }
}
