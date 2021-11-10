using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAI : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Enemy's rotation speed when attacking")]
    [SerializeField] float turnSpeed = 4f;
    [SerializeField] float fireRate = 2f;
    [Tooltip("Start chase if Player is inside the enemy's radius")]
    [SerializeField] float attackRange = 20f;
    [SerializeField] float distanceToPlayer = 7f;
    [SerializeField] List<GameObject> weapons;
    [SerializeField] List<GameObject> enemies;

    GameObject player;
    NavMeshAgent navMeshAgent;
    Animator animator;
    GameObject closestWeapon;
    Weapon activeWeapon;
    GameObject closestEnemy;
    float distanceToTarget = Mathf.Infinity;

    bool isProvoked = false;
    bool isHoldingRifle = false;
    bool isAlive = true;
    bool fired = false;


    // Start is called before the first frame update
    void Start()
    {
        closestWeapon = FindClosestObject(weapons);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(closestWeapon.transform.position);

        animator = GetComponent<Animator>();
        animator.SetInteger("state", 1);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingRifle)
        {
            navMeshAgent.SetDestination(player.transform.position);
            if (navMeshAgent.remainingDistance <= distanceToPlayer)
            {
                closestEnemy = FindClosestObject(enemies);
                if (closestEnemy != null && distanceToTarget <= attackRange)
                {
                    FaceTarget();
                    if (!fired)
                    {
                        StartCoroutine(AttackTarget());
                    }
                }
                else
                {
                    animator.SetInteger("state", 9);
                }
            }
            else
            {
                navMeshAgent.isStopped = false;
                animator.SetInteger("state", 7);
            }
        }
        else if (navMeshAgent.remainingDistance <= 3f && !isHoldingRifle)
        {
            StartCoroutine(playPickUpWeapon());
        }
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

    void FaceTarget()
    {
        navMeshAgent.isStopped = true;
        Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    IEnumerator playPickUpWeapon()
    {
        animator.SetInteger("state", 6);
        yield return new WaitForSeconds(0.8f);
        activeWeapon = GetComponentInChildren<Weapon>(true);
        activeWeapon.gameObject.SetActive(true);
        Destroy(closestWeapon);
        animator.SetInteger("state", 7);
        navMeshAgent.SetDestination(player.transform.position);
        navMeshAgent.stoppingDistance = distanceToPlayer;
        isHoldingRifle = true;
    }

    GameObject FindClosestObject(List<GameObject> objects)
    {
        distanceToTarget = Mathf.Infinity;
        GameObject closestObject = null;
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                float temp = Vector3.Distance(transform.position, obj.transform.position);
                if (temp < distanceToTarget)
                {
                    distanceToTarget = temp;
                    closestObject = obj;
                }
            }
        }
        return closestObject;
    }
}