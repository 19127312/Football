using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamFinger : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed = 2.0f;
    public int direction = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        position.y += speed * Time.deltaTime * direction;
        rb2d.MovePosition(position);
    }
}
