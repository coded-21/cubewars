using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletInfo : MonoBehaviour
{
    [HideInInspector] public PhotonView shooter;
    [HideInInspector] public int damage;
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public Vector3 direction;
    public GameObject hitVFX;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity = bulletSpeed * Time.deltaTime * direction;
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint _contactPoint = collision.contacts[0];
        ProcessHit(_contactPoint);
        Debug.Log("Player with view ID " + shooter.ViewID + " hit: " + collision.gameObject.name + " with a damage of " + damage);
    }

    private void ProcessHit(ContactPoint contactPoint)
    {
        PhotonNetwork.Instantiate(hitVFX.name, contactPoint.point, Quaternion.identity);
        Destroy(gameObject);
    }
}
