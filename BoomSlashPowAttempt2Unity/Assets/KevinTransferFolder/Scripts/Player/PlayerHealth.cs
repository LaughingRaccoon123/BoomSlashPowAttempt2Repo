using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public CanvasUI canvasUI;
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
            PlayerDie();
            
        }
    }

    private void PlayerDie()
    {
        canvasUI.deadScreenOverlay(true);
        Time.timeScale = 0f;
    }
}
