using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(menuName = "ScriptableObjects/Gun")]
public class Gun : ScriptableObject
{
    public string gunName;
    public int damage;
    public int bulletSpeed;
    public float fireCooldown;
    public GameObject bulletPrefab;
    public string bulletPrefabName;
    [HideInInspector] public float newFireTime;
}