using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("Maximum player health")]
    [SerializeField] float maxHealth = 100f;

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
        if (health <= 0)
        {
            deathHandler.HandleDeath();
        }
    }
}
