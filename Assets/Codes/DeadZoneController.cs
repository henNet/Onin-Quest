using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    private GameObject player;
    public int totalHearts = 4;

    [Header("Hit/Camera Shake")]
    [SerializeField] private Animator animCamera;
    [SerializeField] private GameObject hitPanel;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            totalHearts--;
            if (totalHearts >= 0)
                UiController.instance.DisableHeart(totalHearts);

            if (totalHearts == 0)
            {
                Invoke(nameof(DisablePlayer), 0.389f);
                Invoke(nameof(EnableGameOverButton), 1f);
                player.GetComponent<MoveController>().StartDeath();
                player.GetComponent<MoveController>().enabled = false;
                AudioController.instance.PlayDeathSound();
            }

            AudioController.instance.PlayHitSound();
            animCamera.SetTrigger("Shake");
            hitPanel.gameObject.SetActive(true);
            Invoke(nameof(DisableHitPanel), 0.2f);
        }
    }

    private void DisablePlayer()
    {
        player.gameObject.SetActive(false);
    }

    private void DisableHitPanel()
    {
        hitPanel.gameObject.SetActive(false);
    }

    private void EnableGameOverButton()
    {
        UiController.instance.EnableGameOverButton();
    }
}
