using Unity.VisualScripting;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target" || collision.tag == "Player")
        {
            UiController.instance.EnableGameOverButton();
            Debug.Log("Game Over");
        }
    }
}
