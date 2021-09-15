using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Weapon : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject player;
    public Gun currentGun;
    public Transform firePoint;
    public GameObject[] gunGraphics;
    [HideInInspector] public Vector3 direction;

    private void Start()
    {
        currentGun.newFireTime = 0;
        GunSwitch();
    }

    public void Fire(PhotonView shooter)
    {
        if (Time.time > currentGun.newFireTime)
        {
            Debug.Log("Well, you shot.");
            //spawn
            direction = Vector3.Normalize(firePoint.position - transform.position);
            ShootingManager.Instance.Shoot(currentGun.bulletPrefabName,
                firePoint.position,
                direction,
                currentGun.bulletSpeed,
                currentGun.damage);
            /*
            GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", currentGun.bulletPrefabName),
                        new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z),
                        Quaternion.identity, 0, new object[] { shooter.ViewID });
            */
            //cooldown
            currentGun.newFireTime = Time.time + currentGun.fireCooldown;
        }
    }

    public void GunSwitch()
    {
        currentGun.newFireTime = 0;
        for (int i = 0; i < gunGraphics.Length; i++)
        {
            if (gunGraphics[i].gameObject.name == currentGun.gunName)
            {
                if (!gunGraphics[i].activeInHierarchy)
                {
                    gunGraphics[i].SetActive(true);
                }      
            }
            else gunGraphics[i].SetActive(false);
        }
    }

}
