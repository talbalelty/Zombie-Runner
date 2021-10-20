using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeGrenade : MonoBehaviour
{
    [SerializeField] float grenadeTimer = 3f;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject grenadeBody;

    float destroyTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartExplosionSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator StartExplosionSequence()
    {
        yield return new WaitForSeconds(grenadeTimer);
        explosion.SetActive(true);
        grenadeBody.SetActive(false);
        Destroy(gameObject, destroyTimer);
    }
}
