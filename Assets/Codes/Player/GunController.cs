using UnityEngine;

public class GunController : MonoBehaviour
{
    private InputController input;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform gun;
    [SerializeField] private float gunDistance = 1.5f;

    private bool gunFacingRight = true;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speedBullet;
    [SerializeField] private int currentBullets = 15;
    [SerializeField] private int maxBullets = 15;

    private GameObject[] targets;
    private GameObject targetFood;

    void Awake()
    {
        input = GetComponent<InputController>();
    }

    void Update()
    {
        /* TODO: Refatorar condicionando ao uso de um collider */
        FindClosestTarget();

        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = targetFood != null ?
            new Vector3(targetFood.transform.position.x, targetFood.transform.position.y, 0) :
            Vector3.zero;

        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        gun.rotation = Quaternion.Euler(
            new Vector3(0, 0, angle));

        gun.position =
            transform.position +
            Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        // Quaternion rotationToEnemy = Quaternion.LookRotation(direction.normalized);

        // gun.rotation =
        //     Quaternion.Lerp(gun.rotation, rotationToEnemy, 2 * Time.deltaTime);

        GunFlipController(mousePos);

        // if ((Input.GetKeyDown(KeyCode.Mouse0) ||
        //     Input.GetButtonDown("Fire2")) && HasGunBullets())
        if (input.GetShootAction() && HasGunBullets())
        {
            Shoot(direction);
        }
    }

    void GunFlipController(Vector2 mousePos)
    {
        if (mousePos.x < gun.position.x && gunFacingRight)
            GunFlip();
        else if (mousePos.x > gun.position.x && !gunFacingRight)
            GunFlip();
    }

    private void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(
            gun.localScale.x,
            gun.localScale.y * -1,
            gun.localScale.z);
    }

    private void Shoot(Vector3 direction)
    {
        animator.SetTrigger("Shoot");
        GameObject newBullet = Instantiate(
            bulletPrefab, gun.position, Quaternion.identity);

        newBullet.GetComponent<Rigidbody2D>().linearVelocity =
            direction.normalized * speedBullet;

        Destroy(newBullet, 3f);

        AudioController.instance.PlayGunShoot();
    }

    private bool HasGunBullets()
    {
        // if (currentBullets <= 0) return false;

        // currentBullets--;
        return true;
    }

    private void FindClosestTarget()
    {
        // Encontra todos os inimigos na cena
        targets = GameObject.FindGameObjectsWithTag("Target");

        if (targets.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            GameObject closestTarget = null;

            foreach (GameObject target in targets)
            {
                float distance =
                    Vector3.Distance(transform.position, target.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            targetFood = closestTarget;
        }
    }
}
