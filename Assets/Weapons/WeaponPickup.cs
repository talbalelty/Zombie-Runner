using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] TextMeshProUGUI pickupText;
    [SerializeField] float pickupRange = 2f;

    StarterAssetsInputs _input;
    Weapon[] weapons;
    Ammo ammo;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponentInParent<StarterAssetsInputs>();
        weapons = GetComponentsInChildren<Weapon>(true);
        ammo = GetComponentInParent<Ammo>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPickup();
    }

    void ProcessPickup()
    {
        pickupText.enabled = false;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out RaycastHit hit, pickupRange))
        {
            if (hit.transform.gameObject.CompareTag("Weapon"))
            {
                pickupText.enabled = true;
                if (_input.weaponPickup)
                {
                    if (hit.transform.gameObject.TryGetComponent<ExplodeGrenade>(out ExplodeGrenade grenade))
                    {
                        ammo.IncreaseGrenadeAmmoAmount(grenade);
                    }
                    else
                    {
                        SetActiveWeapon(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    void SetActiveWeapon(GameObject newWeapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (newWeapon.name.Equals(weapons[i].name))
            {
                _input.selectedWeaponIndex = i;
                weapons[i].PickedUp = true;
                Destroy(newWeapon);
            }
        }
    }

}
