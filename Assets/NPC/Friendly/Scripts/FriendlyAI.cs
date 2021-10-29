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
    [Tooltip("Start chase if Player is inside the enemy's radius")]
    [SerializeField] float attackRange = 20f;
    [SerializeField] float distanceToPlayer = 7f;
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject[] enemies;

    GameObject player;
    NavMeshAgent navMeshAgent;
    Animator animator;
    GameObject closestWeapon;
    GameObject closestEnemy;
    float distanceToTarget = Mathf.Infinity;

    bool isProvoked = false;
    bool isHoldingRifle = false;
    bool isAlive = true;


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
            if (navMeshAgent.remainingDistance <= distanceToPlayer)
            {
                animator.SetInteger("state", 9);
            }
            else
            {
                navMeshAgent.stoppingDistance = distanceToPlayer;
                navMeshAgent.SetDestination(player.transform.position);
                animator.SetInteger("state", 7);
                navMeshAgent.isStopped = false;
            }

            closestEnemy = FindClosestObject(enemies);
            distanceToTarget = Vector3.Distance(transform.position, closestEnemy.transform.position);
            if (distanceToTarget <= attackRange)
            {
                navMeshAgent.isStopped = true;
                FaceTarget();
                animator.SetInteger("state", 8);
            }
        }
        else if (navMeshAgent.remainingDistance <= 3f && !isHoldingRifle)
        {
            StartCoroutine(playPickUpWeapon());
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    IEnumerator playPickUpWeapon()
    {
        animator.SetInteger("state", 6);
        yield return new WaitForSeconds(0.8f);
        GetComponentInChildren<Weapon>(true).gameObject.SetActive(true);
        Destroy(closestWeapon);
        animator.SetInteger("state", 7);
        closestEnemy = FindClosestObject(enemies);
        isHoldingRifle = true;
    }

    GameObject FindClosestObject(GameObject[] objects)
    {
        distanceToTarget = Mathf.Infinity;
        GameObject closestObject = null;
        foreach (GameObject weapon in objects)
        {
            float temp = Vector3.Distance(transform.position, weapon.transform.position);
            if (temp < distanceToTarget)
            {
                distanceToTarget = temp;
                closestObject = weapon;
            }
        }
        return closestObject;
    }
}
