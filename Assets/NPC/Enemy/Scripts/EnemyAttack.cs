using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Tooltip("The amount of damage with each attack")]
    [SerializeField] float damage = 25f;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackHitEvent()
    {
        if (target != null)
        {
            Debug.Log("AttackHitEvent" + damage);
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
