using Unity.VisualScripting;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target" || collision.tag == "Player")
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }
    }
}
