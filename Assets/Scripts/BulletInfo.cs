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

    private void FixedUpdate()
    {
        GetComponent<Transform>().Translate(direction * Time.deltaTime * bulletSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player with view ID " + shooter.ViewID + " hit: " + other.gameObject.name + " with a damage of " + damage);
    }
}
