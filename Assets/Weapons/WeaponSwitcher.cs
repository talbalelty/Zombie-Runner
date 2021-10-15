using StarterAssets;
using TMPro;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] TextMeshProUGUI pickupText;
    [SerializeField] float pickupRange = 4f;

    StarterAssetsInputs _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponentInParent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPickup();
    }

    void ProcessPickup()
    {
        GameObject newWeapon = ProcessRaycast();
        if (newWeapon != null)
        {
            if (newWeapon.CompareTag("Weapon"))
            {
                pickupText.enabled = true;
                if (_input.weaponPickup)
                {
                    SetActiveWeapon(newWeapon);
                }
            }
            else
            {
                pickupText.enabled = false;
            }
        }
        else
        {
            pickupText.enabled = false;
        }
    }

    GameObject ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, pickupRange))
        {
            return hit.transform.gameObject;
        }

        return null;
    }

    void SetActiveWeapon(GameObject newWeapon)
    {
        foreach (Transform weapon in transform)
        {
            if (newWeapon.name.Equals(weapon.name))
            {
                weapon.gameObject.SetActive(true);
                Destroy(newWeapon);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

}
