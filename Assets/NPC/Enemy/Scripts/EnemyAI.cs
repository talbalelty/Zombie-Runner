using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [Header("Behaviour")]
    [Tooltip("Start chase if Player is inside the enemy's radius")]
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float attackRange = 20f;
    [SerializeField] float fireRate = 2f;
    [Header("Movement")]
    [Tooltip("Enemy's rotation speed when attacking")]
    [SerializeField] float turnSpeed = 4f;
    [SerializeField] GameObject enemyRifle;
    [SerializeField] TextMeshProUGUI chaseText;

    GameObject target;
    NavMeshAgent navMeshAgent;
    Animator animator;
    Weapon activeWeapon;
    float distanceToTarget = Mathf.Infinity;

    bool isProvoked = false;
    bool isHoldingRifle = false;
    bool isAlive = true;
    bool fired = false;

    // Start is called before the first frame update
    void Start()
    {
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
        navMeshAgent.SetDestination(transform.position);
        navMeshAgent.isStopped = true;
        isProvoked = false;
        isAlive = false;
        GetComponentInChildren<Weapon>(true).gameObject.SetActive(false);
        Destroy(gameObject, 4f);
    }

    void EngageTarget()
    {
        if (distanceToTarget > chaseRange)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= attackRange)
        {
            FaceTarget();
            if (!fired)
            {
                StartCoroutine(AttackTarget());
            }
        }
    }

    IEnumerator playPickUpWeapon()
    {
        navMeshAgent.isStopped = true;
        animator.SetInteger("state", 6);
        activeWeapon = GetComponentInChildren<Weapon>(true);
        yield return new WaitForSeconds(0.8f);
        activeWeapon.gameObject.SetActive(true);
        Destroy(enemyRifle);
        animator.SetInteger("state", 7);
        navMeshAgent.SetDestination(target.transform.position);
        isHoldingRifle = true;
        isProvoked = true;
        navMeshAgent.isStopped = false;
    }

    public void GetRifle()
    {
        distanceToTarget = Vector3.Distance(enemyRifle.transform.position, transform.position);
        if (distanceToTarget <= 3)
        {
            StartCoroutine(playPickUpWeapon());
            if (chaseText != null && !chaseText.gameObject.activeSelf)
            {
                chaseText.gameObject.SetActive(true);
                Destroy(chaseText.gameObject, 4f);
            }
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

    IEnumerator AttackTarget()
    {
        fired = true;
        animator.SetInteger("state", 8);
        activeWeapon.Shoot();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetInteger("state", 9);
        yield return new WaitForSeconds(fireRate);
        fired = false;
    }

    // NavMeshAgent no longer controls rotation therefore FaceTarget is called
    void FaceTarget()
    {
        navMeshAgent.isStopped = true;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }
}
