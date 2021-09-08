using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject player;
    public Gun currentGun;
    public Transform firePoint;
    public GameObject[] gunGraphics;
    Vector3 direction;

    private void Start()
    {
        currentGun.newFireTime = 0;
        GunSwitch();
    }

    public void Fire(PhotonView PV_Shooter)
    {
        if (Time.time > currentGun.newFireTime)
        {
            Debug.Log("Well, you shot.");
            //spawn
            GameObject bullet = PhotonNetwork.Instantiate(currentGun.bullet.name,
                        new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z),
                        Quaternion.identity);
            //give bullet info
            BulletInfo b = bullet.GetComponent<BulletInfo>();
            direction = Vector3.Normalize(firePoint.position - transform.position);
            b.shooter = PV_Shooter;
            b.damage = currentGun.damage;
            b.direction = direction;
            b.bulletSpeed = currentGun.bulletSpeed;
            //cooldown
            currentGun.newFireTime = Time.time + currentGun.fireCooldown;
            //destroy
            Destroy(bullet, 4);
        }
    }

    private void GunSwitch()
    {
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
