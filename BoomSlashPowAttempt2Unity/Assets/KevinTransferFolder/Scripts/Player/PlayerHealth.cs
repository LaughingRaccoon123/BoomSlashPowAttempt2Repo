using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public CanvasUI canvasUI;
    public float playerHealth { get; private set; }

    [SerializeField] private float playerMaxHealth = 10f;

    private void Awake()
    {
        playerHealth = playerMaxHealth;
    }

    public void PlayerTakeDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log("ouch");

        if (playerHealth <= 0)
        {
            PlayerDie();
            
        }
    }

    private void PlayerDie()
    {
        canvasUI.deadScreenOverlay(true);
        Time.timeScale = 0f;
    }
}
