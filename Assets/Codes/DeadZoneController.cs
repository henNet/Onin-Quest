using System.Xml.Serialization;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    private GameObject player;
    public int totalHearts = 4;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            totalHearts--;
            UiController.instance.DisableHeart(totalHearts);

            if (totalHearts == 0)
            {
                Invoke(nameof(DisablePlayer), 0.389f);
                Invoke(nameof(EnableGameOverButton), 1f);
                player.GetComponent<MoveController>().StartDeath();
                player.GetComponent<MoveController>().enabled = false;
            }
        }
    }

    private void DisablePlayer()
    {
        player.gameObject.SetActive(false);
    }

    private void EnableGameOverButton()
    {
        UiController.instance.EnableGameOverButton();
    }
}
