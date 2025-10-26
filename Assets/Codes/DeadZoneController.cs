using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }
}
