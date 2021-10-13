using UnityEngine;
using UnityEngine.AI;

// Control the enemy's behaviour
public class EnemyAI : MonoBehaviour
{
    [Header("Behaviour")]
    [Tooltip("Start chase if Player is inside the enemy's radius")]
    [SerializeField] float chaseRange = 15f;
    [Header("Movement")]
    [Tooltip("Enemy's rotation speed when attacking")]
    [SerializeField] float turnSpeed = 4f;

    GameObject target;
    NavMeshAgent navMeshAgent;
    Animator animator;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    // EnemyHealth broadcast function when health drops
    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }

    }
    void ChaseTarget()
    {
        animator.SetBool("attack", false);
        animator.SetTrigger("move");
        navMeshAgent.SetDestination(target.transform.position);
    }

    void AttackTarget()
    {
        animator.SetBool("attack", true);
        FaceTarget();
    }

    // NavMeshAgent no longer controls rotation therefore FaceTarget is called
    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
