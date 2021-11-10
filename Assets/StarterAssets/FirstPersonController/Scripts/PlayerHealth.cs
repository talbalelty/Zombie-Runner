using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("Maximum player health")]
    [SerializeField] float maxHealth = 100f;
    [SerializeField] TextMeshProUGUI healthText;

    DeathHandler deathHandler;
    float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        deathHandler = GetComponent<DeathHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.Abs(damage);
        health = Mathf.Clamp(health, 0, maxHealth);
        healthText.SetText("Health: " + health + " / 100");
        if (health <= 0)
        {
            deathHandler.HandleDeath();
        }
    }
}
