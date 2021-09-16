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
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public int bulletSpeed;
    [HideInInspector] public int damage;
    [HideInInspector] public int shooterID;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //s_weapon = shooter.GetComponentInChildren<Weapon>();
    }

    private void FixedUpdate()
    {
        rb.velocity = bulletSpeed * Time.deltaTime * direction;
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<IDamagable>()?.TakeDamage(damage, shooterID);
        ContactPoint contactPoint = collision.contacts[0];
        ProcessHit(contactPoint);
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
