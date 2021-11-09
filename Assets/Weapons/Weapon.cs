using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [Tooltip("Weapon effective range")]
    [SerializeField] float range = 100f;
    [Tooltip("Weapon bullet damage")]
    [SerializeField] float weaponDamage = 25f;

    [Header("Weapon Assets")]
    [Tooltip("Partile effect on bullet impact")]
    [SerializeField] ParticleSystem hitVFX;
    [SerializeField] AudioClip shootSFX;

    [Header("Player Assets")]
    [Tooltip("Player Main Camera with CinemachineBrain")]
    [SerializeField] GameObject fireSource;
    [SerializeField] Ammo ammoSlot;

    StarterAssetsInputs _input;
    ParticleSystem muzzleFlash;
    AudioSource audioSource;
    bool pickedUp;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponentInParent<StarterAssetsInputs>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input != null && _input.fire)
        {
            Shoot();
            _input.fire = false;
        }
    }

    public void Shoot()
    {
        if (ammoSlot.GetAmmoAmount() > 0)
        {
            ammoSlot.ReduceAmmoAmount();
            audioSource.clip = shootSFX;
            audioSource.Play();
            PlayMuzzleFlash();
            ProcessRaycast();
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
        if (Physics.Raycast(fireSource.transform.position, fireSource.transform.forward, out hit, range))
        {
            HitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(weaponDamage);
            }
        }
    }

    // Play particle system impact
    void HitImpact(RaycastHit hit)
    {
        ParticleSystem impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact.gameObject, impact.main.duration);
    }

    public bool PickedUp
    {
        get
        {
            return pickedUp;
        }
        set
        {
            pickedUp = value;
        }
    }
}

