using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeGrenade : MonoBehaviour
{
    [SerializeField] float grenadeTimer = 3f;
    [SerializeField] int grenadeDamage = 80;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject grenadeBody;

    SphereCollider bodyCollider;
    float colliderTimer = 0.1f;
    float destroyTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        bodyCollider = GetComponentInChildren<SphereCollider>();
        StartCoroutine(StartExplosionSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator StartExplosionSequence()
    {
        yield return new WaitForSeconds(colliderTimer);
        bodyCollider.enabled = true;
        yield return new WaitForSeconds(grenadeTimer);
        grenadeBody.SetActive(false);
        explosion.SetActive(true);
        Destroy(gameObject, destroyTimer);
    }

    public int GrenadeDamage
    {
        get
        {
            return grenadeDamage;
        }
    }

}
