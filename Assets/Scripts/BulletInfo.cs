using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.IO;

public class BulletInfo : MonoBehaviour
{
    public GameObject hitVFX;
    Rigidbody rb;
    [HideInInspector] public PhotonView shooter;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public int bulletSpeed;
    [HideInInspector] public int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //shooter = PhotonView.Find((int)PV.InstantiationData[0]);
        //s_weapon = shooter.GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        /*
        direction = s_weapon.direction;
        bulletSpeed = s_weapon.currentGun.bulletSpeed;
        damage = s_weapon.currentGun.damage;
        */
    }
    private void FixedUpdate()
    {
        rb.velocity = bulletSpeed * Time.deltaTime * direction;
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<IDamagable>()?.TakeDamage(damage);
        ContactPoint contactPoint = collision.contacts[0];
        ProcessHit(contactPoint);
        Debug.Log("Player hit: " + collision.gameObject.name + " with a damage of " + damage);
    }

    private void ProcessHit(ContactPoint contactPoint)
    {
        //vfx
        GameObject vfx = Instantiate(hitVFX, contactPoint.point, Quaternion.identity);
        //destroy
        gameObject.SetActive(false);
        Destroy(vfx, 1);
        Destroy(gameObject, 2);
    }
}
