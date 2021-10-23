using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Behaviour")]
    [Tooltip("Start chase if Player is inside the enemy's radius")]
    [SerializeField] float chaseRange = 15f;
    [Header("Movement")]
    [Tooltip("Enemy's rotation speed when attacking")]
    [SerializeField] float turnSpeed = 4f;

    GameObject target;
    GameObject enemyRifle;
    NavMeshAgent navMeshAgent;
    Animator animator;
    float distanceToTarget = Mathf.Infinity;

    bool isProvoked = false;
    bool isHoldingRifle = false;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyRifle = GameObject.FindGameObjectWithTag("Enemy Rifle");
        target = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetInteger("state", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingRifle)
        {
            distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (isProvoked && isAlive)
            {
                EngageTarget();
            }
            /*else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
            }*/
        }
        else
        {
            distanceToTarget = Vector3.Distance(enemyRifle.transform.position, transform.position);
            GetRifle();
        }

    }

    // EnemyHealth broadcast function when health drops
    public void OnDamageTaken()
    {
        isProvoked = false;
        StartCoroutine(playDamageAnimation());
    }

    IEnumerator playDamageAnimation()
    {
        animator.SetInteger("state", 2);
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(0.5f);
        navMeshAgent.isStopped = false;
        animator.SetInteger("state", 7);
        isProvoked = true;

    }

    public void OnDeath()
    {
        animator.SetInteger("state", 4);
        navMeshAgent.isStopped = true;
        isProvoked = false;
        isAlive = false;
        GetComponentInChildren<Weapon>(true).gameObject.SetActive(false);
        Destroy(gameObject, 10f);
    }

    void EngageTarget()
    {
        if (distanceToTarget > 5)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= 5)
        {
            AttackTarget();
        }

    }

    IEnumerator playPickUpWeapon()
    {
        animator.SetInteger("state", 6);
        yield return new WaitForSeconds(0.8f);
        GetComponentInChildren<Weapon>(true).gameObject.SetActive(true);
        Destroy(enemyRifle);
        animator.SetInteger("state", 7);
        navMeshAgent.SetDestination(target.transform.position);
        isHoldingRifle = true;
        isProvoked = true;
    }
    public void GetRifle()
    {
        if (distanceToTarget <= 3)
        {
            StartCoroutine(playPickUpWeapon());
        }
        else
        {
            navMeshAgent.SetDestination(enemyRifle.transform.position);
        }


    }
    void ChaseTarget()
    {
        navMeshAgent.isStopped = false;
        animator.SetInteger("state", 7);
        navMeshAgent.SetDestination(target.transform.position);
    }

    void AttackTarget()
    {
        navMeshAgent.isStopped = true;
        animator.SetInteger("state", 8);
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
