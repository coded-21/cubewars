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
    PhotonView PlayerPV;

    private void Start()
    {
        PlayerPV = GetComponentInParent<PhotonView>();
        //if (PlayerPV.IsMine)
        //{
            currentGun.newFireTime = 0;
            GunSwitch();
        //}
    }

    public void Fire(PhotonView shooterPV)
    {
        if (Time.time > currentGun.newFireTime)
        {
            int shooterID = shooterPV.GetComponent<PlayerStatus>().playerManager.GetComponent<PhotonView>().ViewID;
            direction = Vector3.Normalize(firePoint.position - transform.position);
            ShootingManager.Instance.Shoot(currentGun.bulletPrefabName,
                firePoint.position,
                direction,
                currentGun.bulletSpeed,
                currentGun.damage,
                shooterID);
            
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
