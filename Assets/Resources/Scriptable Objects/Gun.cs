using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string gunName;
    public int damage;
    public int bulletSpeed;
    public float fireCooldown;
    public GameObject bullet;
    [HideInInspector] public float newFireTime;
}