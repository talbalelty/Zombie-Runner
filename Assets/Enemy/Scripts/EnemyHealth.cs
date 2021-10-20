using UnityEngine;


// Enemy Health system
public class EnemyHealth : MonoBehaviour
{
    [Tooltip("Maximum enemy health")]
    [SerializeField] float maxHealth = 100f;

    float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        // Enemy is provoked and start to chase target
        BroadcastMessage("OnDamageTaken");
        health -= Mathf.Abs(damage);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
