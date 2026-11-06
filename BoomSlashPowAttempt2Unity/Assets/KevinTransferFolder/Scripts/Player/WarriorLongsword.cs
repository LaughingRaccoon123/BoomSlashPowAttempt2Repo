using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private float weaponDamage = 2f;

    private bool isDamageActive = false;

    public void EnableDamage()
    {
        isDamageActive = true;
    }

    public void DisableDamage()
    {
        isDamageActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDamageActive) return;

        if (other.CompareTag("Enemy"))
        {
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
