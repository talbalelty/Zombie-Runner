using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    int currentWeaponIndex = -1;
    StarterAssetsInputs _input;
    Weapon[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponentInParent<StarterAssetsInputs>();
        weapons = GetComponentsInChildren<Weapon>(true);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessScroll();
        if (currentWeaponIndex != _input.selectedWeaponIndex)
        {
            SetActiveWeapon();
        }
    }

    void ProcessScroll()
    {
        if (_input.scroll != 0)
        {
            int loopSafeguard = 0;
            int cycleToWeaponIndex = NewIndexModuloWeapons(currentWeaponIndex);
            while (loopSafeguard < weapons.Length)
            {
                if (weapons[cycleToWeaponIndex].PickedUp)
                {
                    _input.selectedWeaponIndex = cycleToWeaponIndex;
                    return;
                }
                else if (currentWeaponIndex == cycleToWeaponIndex)
                {
                    return;
                }
                cycleToWeaponIndex = NewIndexModuloWeapons(cycleToWeaponIndex);
                loopSafeguard++;
            }
        }
    }

    int NewIndexModuloWeapons(int index)
    {
        int newIndex = (index + _input.scroll) % weapons.Length;
        if (newIndex < 0)
        {
            newIndex += weapons.Length;
        }
        return newIndex;
    }

    void SetActiveWeapon()
    {
        if (weapons[_input.selectedWeaponIndex].PickedUp)
        {
            if (0 <= currentWeaponIndex && currentWeaponIndex < weapons.Length)
            {
                weapons[currentWeaponIndex].gameObject.SetActive(false);
            }
            weapons[_input.selectedWeaponIndex].gameObject.SetActive(true);
            currentWeaponIndex = _input.selectedWeaponIndex;
        }
        else
        {
            _input.selectedWeaponIndex = currentWeaponIndex;
        }
    }
}
