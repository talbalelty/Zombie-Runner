using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{

    public class Weapon : MonoBehaviour
    {
        [SerializeField] Camera FPCamera;
        [SerializeField] ParticleSystem hitVFX;
        [SerializeField] float range = 100f;
        [SerializeField] float weaponDamage = 25f;

        StarterAssetsInputs _input;
        ParticleSystem muzzleFlash;

        // Start is called before the first frame update
        void Start()
        {
            _input = GetComponentInParent<StarterAssetsInputs>();
            muzzleFlash = GetComponentInChildren<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            Shoot();
        }

        void Shoot()
        {

            if (_input.fire1)
            {
                PlayMuzzleFlash();
                ProcessRaycast();
                _input.fire1 = false;
            }
        }

        void PlayMuzzleFlash()
        {
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
        }

        void ProcessRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
            {
                HitImpact(hit);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target != null)
                {
                    target.TakeDamage(weaponDamage);
                }
            }
        }

        void HitImpact(RaycastHit hit)
        {
            ParticleSystem impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact.gameObject, impact.main.duration);
        }
    }
}
