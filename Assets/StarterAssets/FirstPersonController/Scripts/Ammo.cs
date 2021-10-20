using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    [SerializeField] int grenadeAmmoAmount = 1;
    [SerializeField] int maxGrenadeAmmoAmount = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetAmmoAmount()
    {
        return ammoAmount;
    }

    public void ReduceAmmoAmount()
    {
        ammoAmount--;
    }

    public int GetGrenadeAmmoAmount()
    {
        return grenadeAmmoAmount;
    }

    public void ReduceGrenadeAmmoAmount()
    {
        if (grenadeAmmoAmount > 0)
        {
            grenadeAmmoAmount--;
        }
    }

    public void IncreaseGrenadeAmmoAmount()
    {
        if (grenadeAmmoAmount < maxGrenadeAmmoAmount)
        {
            grenadeAmmoAmount++;
        }
    }
}
