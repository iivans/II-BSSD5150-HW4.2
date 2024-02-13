using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerUpDown : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f; // Speed of the movement
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.MovePosition(rb2d.position + Vector2.up * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reverse direction when hitting a wall
            speed *= -1;
        }
    }
}
