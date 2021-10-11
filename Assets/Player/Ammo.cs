using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;

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
}
