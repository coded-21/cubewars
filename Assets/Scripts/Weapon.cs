using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
    public Gun currentGun;
    public Transform firePoint;

    public void Fire()
    {
        if (Time.time > currentGun.newFireTime)
        {
            GameObject spawnedBullet = PhotonNetwork.Instantiate(currentGun.bullet.name,
                        new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z),
                        Quaternion.identity);
            spawnedBullet.GetComponent<Rigidbody>().velocity += transform.forward * currentGun.bulletSpeed;
            Destroy(spawnedBullet, 4);
            currentGun.newFireTime = Time.time + currentGun.fireCooldown;
        }
    }
}
