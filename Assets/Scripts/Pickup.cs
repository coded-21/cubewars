using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Gun pickupGun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Weapon w = other.GetComponentInChildren<Weapon>();
            w.currentGun = pickupGun;
            w.GunSwitch();
            Destroy(gameObject, 1);
        }
    }
}
