using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float playerHealth = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerTakeDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log("ouch");

        if (playerHealth <= 0)
        {

        }
    }

    private void PlayerDie()
    {

    }
}
