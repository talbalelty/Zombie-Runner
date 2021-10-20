using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    [SerializeField] GameObject grenade;
    [SerializeField] float throwForce = 2f; 

    Ammo ammo;
    StarterAssetsInputs _input;


    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponentInParent<Ammo>();
        _input = GetComponentInParent<StarterAssetsInputs>();
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
            GameObject thrownGrenade = Instantiate(grenade, transform.position, transform.rotation);
            Rigidbody rb = thrownGrenade.GetComponent<Rigidbody>();
            rb.AddRelativeForce(transform.forward * throwForce, ForceMode.Impulse);
        }
    }
}
