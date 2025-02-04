using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -7.0f) {
            Destroy(gameObject);
        }
    }

    void Jump() {
        Rigidbody2D rigidBody2d = GetComponent<Rigidbody2D>();
        float jumpHeight = Random.Range(4.0f, 8.0f);
        Vector2 jumpVelocity = Vector2.up * jumpHeight;
        jumpVelocity.x = Random.Range(-1.0f, 1.0f);
        rigidBody2d.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }
}
