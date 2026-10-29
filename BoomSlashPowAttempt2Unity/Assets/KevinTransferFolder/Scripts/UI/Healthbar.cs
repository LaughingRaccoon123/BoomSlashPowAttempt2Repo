using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public PlayerHealth playerHealthScript;
    [SerializeField] private Slider healthbar;

    void Start()
    {
        healthbar.maxValue = playerHealthScript.playerHealth;
    }

    void Update()
    {
        healthbar.value = playerHealthScript.playerHealth;
    }
}
