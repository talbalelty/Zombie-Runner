using StarterAssets;
using TMPro;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] TextMeshProUGUI pickupText;
    [SerializeField] float pickupRange = 2f;

    StarterAssetsInputs _input;
    int layerMask = 1 << 6;

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
        pickupText.enabled = false;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out RaycastHit hit, pickupRange))
        {
            if (hit.transform.gameObject.CompareTag("Weapon"))
            {
                pickupText.enabled = true;
                if (_input.weaponPickup)
                {
                    SetActiveWeapon(hit.transform.gameObject);
                }
            }
        }
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
