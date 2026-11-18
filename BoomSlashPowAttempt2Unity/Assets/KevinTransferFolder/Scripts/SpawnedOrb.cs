using UnityEngine;

public class SpawnedOrb : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float damage = 1f;
    public Enemy enemyScript;

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
    }

    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy Enemy = other.GetComponent<Enemy>();

            if (Enemy != null)
            {
                Enemy.EnemyTakeDamage(damage);
            }

        } else if (other.CompareTag("Player"))
        {
            return;
        }

        Destroy(gameObject);
    }
}
