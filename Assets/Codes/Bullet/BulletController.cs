using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rigidBody => GetComponent<Rigidbody2D>();

    void Update() => transform.right = rigidBody.linearVelocity;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            UiController.instance.AddScore();
        }
    }
}
