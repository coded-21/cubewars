using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ShootingManager : MonoBehaviour
{
    public static ShootingManager Instance;
    PhotonView PV;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Shoot(string bulletPrefabName, Vector3 firePoint, Vector3 direction, int bulletSpeed, int damage)
    {
        Debug.Log("Shot from ShootManager");
        PV.RPC("RPC_Shoot", RpcTarget.All, bulletPrefabName, firePoint, direction, bulletSpeed, damage);
    }
        
    [PunRPC]
    void RPC_Shoot(string bulletPrefabName, Vector3 firePoint, Vector3 direction, int bulletSpeed, int damage)
    {
        Debug.Log("RPC Working");
        GameObject bullet = Instantiate(Resources.Load<GameObject>("PhotonPrefabs/" + bulletPrefabName), firePoint, Quaternion.identity);
        BulletInfo b = bullet.GetComponent<BulletInfo>();
        b.direction = direction;
        b.bulletSpeed = bulletSpeed;
        b.damage = damage;
        //b.shooter = shooter;
    }
}
