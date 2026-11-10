using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private float weaponDamage = 2f;

    private bool isDamageActive = false;

    [SerializeField] private GameObject longSword;
    private Collider swordCollider;

    private void Awake()
    {
        if (longSword != null)
        {
            swordCollider = longSword.GetComponent<Collider>();
        }
        else
        {
            swordCollider = GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sword collided with: " + other.name);

        if (other.CompareTag("Enemy"))
        {

            Debug.Log("contact with enemy");
            Enemy enemy = other.GetComponent<Enemy>();


            if (enemy != null)
            {
                enemy.EnemyTakeDamage(weaponDamage);
                isDamageActive = false;
                Debug.Log("Hit enemy for " + weaponDamage + " damage!");
            }
        }
    }
}
