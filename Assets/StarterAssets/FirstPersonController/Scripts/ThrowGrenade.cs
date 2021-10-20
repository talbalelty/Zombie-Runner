using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    [SerializeField] GameObject grenade;
    [SerializeField] float throwForce = 10f; 

    [SerializeField] Ammo ammo;
    [SerializeField] StarterAssetsInputs _input;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_input.throwGrenade)
        {
            StartGrenadeThrow();
            _input.throwGrenade = false;
        }
    }

    void StartGrenadeThrow()
    {
        if (ammo.GetGrenadeAmmoAmount() > 0)
        {
            ammo.ReduceGrenadeAmmoAmount();
            GameObject thrownGrenade = Instantiate(grenade, transform.position, Quaternion.identity);
            Rigidbody rb = thrownGrenade.GetComponent<Rigidbody>();
            rb.AddRelativeForce(transform.forward * throwForce, ForceMode.Impulse);
        }
    }
}
