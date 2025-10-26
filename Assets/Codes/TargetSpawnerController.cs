using UnityEngine;

public class TargetSpawnerController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Sprite[] targetTypes;

    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float coolDown;
    private float time;

    [Header("Increase Level")]
    private int foodCreated = 0;
    private int foodIncreaseLevel = 10;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            time = coolDown;
            foodCreated++;

            /* Check to increase level */
            if (foodCreated > foodIncreaseLevel && coolDown > 0.5f)
            {
                foodIncreaseLevel += 10;
                coolDown -= 0.3f;
            }

            GameObject newTarget = Instantiate(targetPrefab);

            float rangeX = Random.Range(
                boxCollider.bounds.min.x,
                boxCollider.bounds.max.x);

            newTarget.transform.position =
                new Vector2(rangeX, transform.position.y);

            int rangeIndex = Random.Range(0, targetTypes.Length);
            newTarget.GetComponent<SpriteRenderer>().sprite =
                targetTypes[rangeIndex];
        }
    }
}
